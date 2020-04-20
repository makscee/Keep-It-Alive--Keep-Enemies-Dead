using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : Unit
{
    public static Enemy Instance;

    void OnEnable()
    {
        Instance = this;
        damageFrom += addDmg;
        damageTo += addDmg;
        hp += addHp;
    }

    public void StartFight()
    {
        if (!Player.Instance) return;
        Player.Instance.fighting = true;
    }
    
    public override void Attack()
    {
        var player = Player.Instance;
        if (!player || player.hp <= 0) return;

        player.TakeDmg(Random.Range(damageFrom, damageTo));
        base.Attack();
    }

    public static int Kills;
    public override void Die()
    {
        Kills++;
        base.Die();
        Instance = null;
        Destroy(gameObject);
        for (var i = 0; i < 3; i++)
        {
            var shape = ShapeConstructor.GetRandom();
            shape.transform.position = transform.position;
            shape.falling = true;
            shape.velocity = new Vector3(Random.Range(-8f, -3f), 9f);
        }
    }

    public static int addHp, addDmg;
    public static void Spawn()
    {
        var r = Random.Range(0f, 1f);
        if (r > 0.6f)
            addHp++;
        if (r > 0.8f)
            addDmg++;
        
        Instantiate(FieldedObjects.Instance.Enemy);
    }
}