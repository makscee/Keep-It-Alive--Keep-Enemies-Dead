using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : Unit
{
    public static Player Instance;
    public bool fighting;
    SpriteRenderer _sr;

    void OnEnable()
    {
        Instance = this;
        _sr = GetComponent<SpriteRenderer>();
    }

    float timeToAttack = 4f, attackCounter = 1f, timeToNextEnemy = 10f;
    bool _enemyTurn;

    public override void Attack()
    {
        var enemy = Enemy.Instance;
        if (!enemy || enemy.hp <= 0) return;

        enemy.TakeDmg(Random.Range(damageFrom + nextHitBonus, damageTo + nextHitBonus));
        nextHitBonus = 0;
        Stats.dmgBoosted = 0;
        _sr.color = Color.white;
        Stats.Refresh();
        base.Attack();

        if (Random.Range(0f, 1f) > 0.5f || TetrisField.ShapesPlaced < 3)
        {
            var shape = ShapeConstructor.GetRandom();
            shape.transform.position = enemy.transform.position;
            shape.falling = true;
            shape.velocity = new Vector3(Random.Range(-8f, -3f), 9f);
        }
    }

    public void Heal(int amount = 1)
    {
        hp += amount;
        Stats.Refresh();
        ParticleEffects.CreateExplosion(transform.position);
    }

    int dmgCnt = 0;
    int nextHitBonus = 0;
    public void AddDmg(int amount)
    {
        dmgCnt += amount;
        nextHitBonus += amount;
        _sr.color = Color.yellow;
        Stats.dmgBoosted = nextHitBonus;
        
        ParticleEffects.CreateFlame(transform.position);
        if (dmgCnt >= 25)
        {
            dmgCnt -= 25;
            damageFrom++;
            damageTo++;
        }
        Stats.Refresh();
    }

    static bool EnemyDead => !Enemy.Instance || Enemy.Instance.hp <= 0;

    void Update()
    {
        if (EnemyDead)
        {
            timeToNextEnemy -= Time.deltaTime;
            if (timeToNextEnemy < 0f)
            {
                Enemy.Spawn();
                timeToNextEnemy = 5f;
            }
        }
        
        if (!fighting) return;
        
        attackCounter -= Time.deltaTime;
        if (attackCounter > 0f) return;
        
        attackCounter = timeToAttack;
        if (_enemyTurn)
        {
            Enemy.Instance.Attack();
        }
        else
        {
            Attack();
        }

        _enemyTurn = !_enemyTurn;
    }

    public override void Die()
    {
        base.Die();
        gameObject.SetActive(false);
        FieldedObjects.Instance.GameOverCanvas.gameObject.SetActive(true);
        FieldedObjects.Instance.GameOverText.text =
            $"Wowzers!\nYou defeated {Enemy.Kills} enemies\nand cleared {TetrisField.LinesCleared} lines!\n\nBut was it so hard to keep it alive?";
        Stats.gameObject.SetActive(false);
    }
}
