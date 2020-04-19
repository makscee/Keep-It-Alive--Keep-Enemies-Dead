using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int hp, damageFrom, damageTo;
    public Animator animator;

    protected UnitStats Stats;

    void Start()
    {
        Stats = UnitStats.Create(this, hp, damageFrom, damageTo);
    }

    public virtual void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public void TakeDmg(int amount)
    {
        animator.SetTrigger("TakeDmg");
        hp -= amount;
        DmgText.Create(amount, transform.position);
        if (hp <= 0) Die();
        Stats.Refresh();
    }

    public virtual void Die()
    {
        Player.Instance.fighting = false;
    }
}