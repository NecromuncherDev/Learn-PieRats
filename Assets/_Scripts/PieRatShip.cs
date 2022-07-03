using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PieStock))]
[RequireComponent(typeof(RatStock))]
[RequireComponent(typeof(Traveler))]
[RequireComponent(typeof(WarFighter))]
public class PieRatShip : MonoBehaviour
{
    public event Action OnLose;
    public event Action<uint> OnCrewChanged;
    public event Action<uint> OnAmmoChanged;

    [Header("Ammunition")]
    [SerializeField] protected uint startingAmmo;
    [SerializeField] private Projectile ammoPrefab;

    [Header("Crew")]
    [SerializeField] protected uint startingCrew;
    [SerializeField] protected uint crewMemberHP;
    [SerializeField] private CrewMember crewPrefab;
    [SerializeField] private float barrageInterval;

    private RatStock crew;
    private PieStock ammo;
    private Traveler traveler;
    private WarFighter fighter;
    private List<CrewMember> members = new List<CrewMember>();
    private bool attacking = false;
    private Coroutine attackCoroutine;

    private void Awake()
    {
        TryGetComponent<RatStock>(out crew);
        TryGetComponent<PieStock>(out ammo);
        TryGetComponent<Traveler>(out traveler);
        TryGetComponent<WarFighter>(out fighter);
    }

    private void Start()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        ammo.Add(startingAmmo);
        crew.Add(startingCrew);
    }

    private void OnEnable()
    {
        crew.OnStockValueChanged += ModifyCrew;
        crew.OnStockEmpty += Lose;
        ammo.OnStockValueChanged += ModifyAmmo;
        fighter.OnJoinedWar += StartWar;
    }


    private void OnDisable()
    {
        crew.OnStockValueChanged -= ModifyCrew;
        crew.OnStockEmpty -= Lose;
        fighter.OnJoinedWar -= StartWar;
    }

    private void StartWar(WarMonger monger)
    {
        traveler.Halt();

        PieRatShip enemy;
        if (monger.gameObject.TryGetComponent<PieRatShip>(out enemy))
        {
            enemy.OnLose += StopWar;
            StartAttacking(enemy);
        }
        else
        {
            StopWar();
        }
    }

    private void StopWar()
    {
        traveler.Resume();
        StopAttacking();
    }

    private void StartAttacking(PieRatShip enemy)
    {
        attacking = true;

        if (attackCoroutine != null)
            attackCoroutine = null;

        attackCoroutine = StartCoroutine(Barrage(enemy, ammo));
    }

    private IEnumerator Barrage(PieRatShip enemy, Stock ammoStock)
    {
        while (attacking)
        {
            AttackOnce(enemy);
            yield return new WaitForSeconds(barrageInterval);
        }
    }

    private void AttackOnce(PieRatShip enemy)
    {
        foreach (CrewMember member in members)
            member.LaunchProjectile(enemy.GetRandomCrewMember(), ammo);
    }

    private void StopAttacking()
    {
        attacking = false;
        if (attackCoroutine != null)
            StopCoroutine(attackCoroutine);
    }

    internal Vector2 GetRandomCrewMember()
    {
        return members[UnityEngine.Random.Range(0, members.Count)].transform.position;
    }

    internal void ModifyCrew(int newSize)
    {
        while (newSize > members.Count)
        {
            Vector3 offset = new Vector2(UnityEngine.Random.Range(-0.2f, 0.2f), UnityEngine.Random.Range(-0.2f, 0.2f));

            CrewMember member = Instantiate(crewPrefab, 
                                            transform.position + offset, 
                                            Quaternion.identity, 
                                            transform);
            member.Init(ammoPrefab, crewMemberHP);
            member.OnDefeated += RemoveCrewMember;
            member.gameObject.layer = gameObject.layer;
            members.Add(member);
        }

        OnCrewChanged?.Invoke((uint)members.Count);
    }


    private void RemoveCrewMember(GameObject member)
    {
        if (crew.TryTake(1) == 1)
        {
            member.GetComponent<CrewMember>().OnDefeated -= RemoveCrewMember;
            members.Remove(member.GetComponent<CrewMember>());
            Destroy(member);
        }

        OnCrewChanged?.Invoke((uint)members.Count);
    }

    private void ModifyAmmo(int newAmount)
    {
        OnAmmoChanged?.Invoke((uint)newAmount);
    }

    private void Lose()
    {
        OnLose?.Invoke();
        Destroy(gameObject, 2f);
    }
}
