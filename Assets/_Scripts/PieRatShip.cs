using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PieStock))]
[RequireComponent(typeof(RatStock))]
[RequireComponent(typeof(Traveler))]
[RequireComponent(typeof(WarFighter))]
public class PieRatShip : MonoBehaviour
{
    [SerializeField] private Projectile ammoPrefab;
    [SerializeField] private CrewMember crewPrefab;

    private RatStock crew;
    private PieStock stock;
    private Traveler traveler;
    private WarFighter fighter;
    private List<CrewMember> members = new List<CrewMember>();

    private void Awake()
    {
        TryGetComponent<RatStock>(out crew);
        TryGetComponent<PieStock>(out stock);
        TryGetComponent<Traveler>(out traveler);
        TryGetComponent<WarFighter>(out fighter);
    }

    private void OnEnable()
    {
        crew.OnStockValueChanged += ModifyCrew;
        crew.OnStockEmpty += Lose;
        fighter.OnJoinedWar += StartWar;
    }

    private void OnDisable()
    {
        crew.OnStockValueChanged -= ModifyCrew;
        crew.OnStockEmpty -= Lose;
        fighter.OnJoinedWar -= StartWar;
    }

    private void StartWar()
    {
        traveler.Halt();
    }

    internal void ModifyCrew(int newSize)
    {
        while (newSize > members.Count)
        {
            Vector3 offset = new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));

            CrewMember member = Instantiate(crewPrefab, 
                                            transform.position + offset, 
                                            Quaternion.identity, 
                                            transform);
            member.Init(ammoPrefab);
            member.OnDefeated += RemoveCrewMember;
            members.Add(member);
        }
    }

    private void RemoveCrewMember(GameObject member)
    {
        if (crew.TryTake(1) == 1)
        {
            members.Remove(member.GetComponent<CrewMember>());
        }
    }
    private void Lose()
    {
        foreach (CrewMember member in members)
        {
            members.Remove(member);
            Destroy(member.gameObject);
        }

        Destroy(gameObject);
    }
}
