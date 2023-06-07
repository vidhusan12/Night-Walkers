using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStatus : PlayerStatus
{
    [SerializeField] private float damage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private bool canAttack;

    private void Start()
    {
        InitVariable();
    }

    public void DealDamage()
    {

    }

    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }

    public override void InitVariable()
    {
        maxHealth = 25;
        SetHealthTo(maxHealth);
        isDead = false;

        damage = 10;
        attackSpeed = 2f;
        canAttack = true;
    }

}
