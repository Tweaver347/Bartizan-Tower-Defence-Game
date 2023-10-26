using System;
using UnityEngine;

public class ArcherTowerType : MonoBehaviour, ITower
{
    public bool Active { get; set; }
    public int[] Location { get; set; }
    public int Damage { get; set; }
    public float AttackRange { get; set; }
    public float AttackCooldown { get; set; }
    public GameObject Target { get; set; }
    public Transform FirePoint { get; set; }

    void Start()
    {
        Active = false;
        Location = null;
        Damage = 1;
        AttackRange = 10f;
        AttackCooldown = 1f;
    }

    void Update()
    {
        if (Active)
        {
            Attack();
        }
        else
        {
            return;
        }
    }
    // TODO: Implement Attack Method
    public void Attack()
    {
        throw new NotImplementedException("The Attack Method is not implemented yet.");
    }
    // TODO: Implement Upgrade Method
    public void Upgrade()
    {
        throw new NotImplementedException("The Upgrade Method is not implemented yet.");
    }
    // TODO: Implement Sell Method
    public int[] Sell()
    {
        throw new NotImplementedException("The Sell Method is not implemented yet.");
    }

}
