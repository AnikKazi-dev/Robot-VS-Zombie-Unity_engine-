using UnityEngine;

public class Robot : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    bool isGrounded;
    public float runSpeed;
    public float jumpSpeed;
    public Transform groundCheck;
    public Transform groundCheckL;
    public Transform groundCheckR;

    public Transform attackCentre;
    public float attackRange = 0.5f;
    public LayerMask enemyLears;
    public LayerMask BossLears;
    public int attackDamage = 40;

    public GameManager gameManager;

    int currentHealth = 100;
    public static bool isDead = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void FixedUpdate()
    {
        if (isDead == false) {
            if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")) ||
                Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Ground")) ||
                Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground"))
                )
            {
                isGrounded = true;
                animator.SetBool("IsGround", true);
            }
            else
            {
                isGrounded = false;
                animator.SetBool("IsGround", false);
                animator.SetTrigger("Jump");
            }

            if (Input.GetKey("right"))
            {
                attackCentre.position = new Vector3(this.transform.position.x + 0.32f, attackCentre.position.y, attackCentre.position.z);
                rb.velocity = new Vector2(runSpeed, rb.velocity.y);
                if (isGrounded)
                    animator.SetTrigger("Run");

                spriteRenderer.flipX = false;
            }
            else if (Input.GetKey("left"))
            {
                attackCentre.position = new Vector3(this.transform.position.x - 0.40f, attackCentre.position.y, attackCentre.position.z);
                rb.velocity = new Vector2(-runSpeed, rb.velocity.y);
                if (isGrounded)
                    animator.SetTrigger("Run");
                spriteRenderer.flipX = true;
            }
            else if (Input.GetKeyDown("d") && isGrounded)
            {
                animator.SetTrigger("Attack");
                Attack();


            }
            else
            {
                //if (isGrounded && flagForAttack == true)
                //animator.Play("Robot Idle");
                animator.SetTrigger("StopRunning");
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            if (Input.GetKey("space") && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                animator.Play("Robot Jump");
            }


        }

    }

    void Attack()
    {
       

        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackCentre.position, attackRange, enemyLears);
        foreach (Collider2D enemy in hitEnemy)
        {
            //Debug.Log("we hit " + enemy.name);
           // enemy.GetComponent<BossScript>().TakeDamage(attackDamage);
            enemy.GetComponent<EnemyScript>().TakeDamage(attackDamage);
        }

        Collider2D[] hitBoss = Physics2D.OverlapCircleAll(attackCentre.position, attackRange, BossLears);
        foreach (Collider2D boss in hitBoss)
        {
            //Debug.Log("we hit " + enemy.name);
            boss.GetComponent<BossScript>().TakeDamage(attackDamage);
            // enemy.GetComponent<EnemyScript>().TakeDamage(attackDamage);
        }

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
       // animator.SetTrigger("Hit");
        if (currentHealth <= 0)
        {


           Die();

        }
    }
    void Die()
    {
        // Debug.Log("Enemy Died");
        gameManager.PlayerDied();
        isDead = true;
        animator.SetTrigger("IsDead");
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;
        this.GetComponent<CapsuleCollider2D>().enabled = false;
        
        

        // GetComponent<Collider2D>().enabled = false;
        // GetComponent<Rigidbody2D>().gravityScale = 0f;
        //this.enabled = false;

        Debug.Log("Dead");
        


    }


}
