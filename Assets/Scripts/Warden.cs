using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warden : Entity
{
    public float speed;
    public float distance;
    public Animator animator;
    private bool movingRight = true;
    public Transform groundDetection;
    private Rigidbody2D rb;
    private float health = 100;
    private void Awake()
    {

        animator = GetComponent<Animator>();


    }
    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        
        if (groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }



    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            animator.SetTrigger("New Trigger");
            Hero.Instance.GetDamage(20);
        }
    }
    public override void GetDamage(int damage)
    {
        health -= damage;
        animator.SetTrigger("HitTrigger");
        Debug.Log(health);
        if (health <= 0)
            Die();
    }
}

