using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStayer : Entity
{
    [SerializeField] private float health = 10;
    private Rigidbody2D rb;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage(20);
            health -= 5;
        }
        if (health < 1)
        {
            Die();
        }

    }
    public override void GetDamage(int damage)
    {
        health -= damage;
        Debug.Log(health);
        if (health <= 0)
            Die();
    }
}
   
