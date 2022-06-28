using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarFighter : MonoBehaviour
{
    [SerializeField] private Projectile ammoPrefab;
    [SerializeField] private CrewMember crewPrefab;

    private WarMonger commander;
    private PieStock ammo;
    private RatStock attackers;
    private List<CrewMember> crew = new List<CrewMember>();

    private void OnEnable()
    {
        attackers.OnStockValueChanged += ModifyCrew;
    }

    private void ModifyCrew(int newSize)
    {
        while(newSize > crew.Count)
        {
            CrewMember member = Instantiate(crewPrefab, transform.position, Quaternion.identity, transform);
            member.Init(ammoPrefab);
            member.OnDefeated += RemoveCrewMember;
            crew.Add(member);
        }
    }

    private void RemoveCrewMember(IDamageable member)
    {
        attackers.TryTake(1);
    }
}
