using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public Animator enemyAnimation;
    public Transform player;
    public float movementRange;
    public float moveSpeed;
    bool isDead = false;
    Rigidbody2D rb;
    Robot robot;
    void Start()
    {
        currentHealth = maxHealth;

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float disTpPlayer = Vector2.Distance(transform.position, player.position);

       

            if (disTpPlayer < movementRange)
            {
                ChasePlayer();
            }
            else
            {
                StopChasePlayer();
            }

        
    }
    void ChasePlayer()
    {
        
            if (transform.position.x < player.position.x) // left
            {

                rb.velocity = new Vector2(moveSpeed, 0f);
            if(isDead == false)
                transform.localScale = new Vector2(-1f, 1f);

                enemyAnimation.SetTrigger("Chase");

            }
            else if (transform.position.x > player.position.x) //right
            {
                rb.velocity = new Vector2((-moveSpeed), 0f);
            if (isDead == false)
                transform.localScale = new Vector2(1f, 1f);

                enemyAnimation.SetTrigger("Chase");
            }
        

    }

    void StopChasePlayer()
    {
        rb.velocity = Vector2.zero;
        enemyAnimation.SetTrigger("noChase");

    }

    

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        enemyAnimation.SetTrigger("Hit");
        if (currentHealth <= 0)
        {  
            Die();     
        }
    }
    void Die()
    {
        // Debug.Log("Enemy Died");
        moveSpeed = 0f;
        isDead = true;
        enemyAnimation.SetBool("IsDead",true);

        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0f;
        //this.enabled = false;
        
        
        
    }

    
}
