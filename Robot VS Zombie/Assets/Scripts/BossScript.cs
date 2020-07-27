using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public Animator enemyAnimation;
    public Transform player;
    public float movementRange;
    public float moveSpeed;
    public static bool isDead = false;
    Rigidbody2D rb;
    public GameManager gameManager;
   // public Robot robot;



    public Transform attackCentre;
    public float attackRange = 0.5f;
    public LayerMask RobotLears;
    public int attackDamage = 30;


    

    void Start()
    {
        currentHealth = maxHealth;

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Robot.isDead == false && isDead == false)
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
        else if(Robot.isDead == true)
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
                transform.localScale = new Vector2(1f, 1f);

                enemyAnimation.SetTrigger("Chase");

            }
            else if (transform.position.x > player.position.x) //right
            {
                rb.velocity = new Vector2((-moveSpeed), 0f);
            if (isDead == false)
                transform.localScale = new Vector2(-1f, 1f);

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
        gameManager.BossDied();
        
        moveSpeed = 0f;
        isDead = true;
        rb.velocity = Vector2.zero;
        enemyAnimation.SetBool("IsDead",true);

        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0f;
        //this.enabled = false;
        
        
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead == false)
        {
            if (collision.CompareTag("Robot"))
            {

                enemyAnimation.SetTrigger("Attack");
                rb.velocity = Vector2.zero;
                Attack();

            }

        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead == false)
        {
            Attack();
        }
    }
    void Attack()
    {


        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackCentre.position, attackRange, RobotLears);
        foreach (Collider2D enemy in hitEnemy)
        {
            //Debug.Log("we hit " + enemy.name);
            // enemy.GetComponent<BossScript>().TakeDamage(attackDamage);
            enemy.GetComponent<Robot>().TakeDamage(attackDamage);
        }

       

    }

}
