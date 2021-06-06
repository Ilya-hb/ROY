using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hero : Entity
{
    #region Variables
    [SerializeField] private float speed = 3f;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float jumpForce = 50f;
    [SerializeField] private GameObject deathScreen;

    public int currentHealth;
    public HP_Script healthBar;
    public int damage;
    public ParticleSystem dust;
   

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    public bool isGrounded = false;
    /* private Arrow arrow;*/


       public Transform AttackPoint;
       public LayerMask enemy;
       public int Damage;
       public float AttackRange;
       public float TimeAttack;
       private float Timer;

    public float HeavyTimeAttack;

    public float Impulse = 30000f;
    private bool LockCharge = false;
    private bool LockBlock = false;


    
    public static Hero Instance { get; set; }

    public Animator animator;
    private string currentAnimation;
    #endregion
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        Instance = this;
        animator = GetComponent<Animator>();
        /*       arrow = Resources.Load<Arrow>("Arrow");*/

    }
    public void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

    }
    private void Update()
    {
        

        if (Input.GetButton("Horizontal"))
        {
            animator.SetBool("isRunning", true);
            Run();

        }
        else animator.SetBool("isRunning", false);


        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
            
           
            /*            ChangeAnimation("Hero_LightAttack");*/
        }
        if (Input.GetButtonDown("Fire2"))
            HeavyAttack();
        Charge();
        Block();
        if (currentHealth <= 1)
            deathScreen.SetActive(true);

    }

    private void FixedUpdate()
    {

        if (isGrounded && Input.GetButton("Jump"))
        {
            /*if (Input.GetButton("Jump"))
                CreateDust();*/
            Jump();
         /*   if(Input.GetButton("Jump"))
            animator.SetBool("isJumping", true);*/
        }
        if (isGrounded == true)
            animator.SetBool("isJumping", false);
        if (Input.GetButton("Jump"))
            animator.SetBool("isJumping", true);
    }
    void ChangeAnimation(string animation)
    {
        if (currentAnimation == animation) return;

        animator.Play(animation);
        currentAnimation = animation;
    }
    private void Run()
    {
        CreateDust();
        Vector3 vector = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + vector, speed * Time.deltaTime);
        sprite.flipX = vector.x < 0;
    }

    private void Jump()
    {

        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }


    public override void GetDamage(int damage)
    {
        if (LockBlock == true)
        {
            currentHealth = currentHealth - (damage/3);
            healthBar.SetHealth(currentHealth);
        }
        /*else*/
        //animator.SetBool("getDamage", true);
        else
        {
            currentHealth = currentHealth - damage;
            healthBar.SetHealth(currentHealth);
            animator.SetTrigger("getDamage");
        }
        Debug.Log(currentHealth);
    
   
    }
    public override void GetShootDamage(int damage)
    {
        if (LockBlock == true)
        {
            currentHealth = currentHealth - (damage/5);
            healthBar.SetHealth(currentHealth);
        }
        else
            currentHealth = currentHealth - damage;
        Debug.Log(currentHealth);
        if (currentHealth < 1)
        {
            Die();
        }
    }
        
    
    void CreateDust()
    {
        dust.Play();
    }
    #region Attack
    private void Attack()
    {
        if (Timer <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Collider2D[] enemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, enemy);
                animator.SetTrigger("attack");
                if (enemies.Length != 0)
                {

                    for (int i = 0; i < enemies.Length; i++)
                    {
                        enemies[i].GetComponent<Entity>().GetDamage(Damage);
                    }
                }
                Timer = TimeAttack;
            }

        }
        else
        {
            Timer -= Time.deltaTime;
        }
    }
    #endregion
    #region Coins
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Coin"))
        {
            ChangeAnimation("CoinsCollectAnim");
            
            CoinsCollect.coinsCount += 1;
            Destroy(collision.gameObject);
        }
    }
    #endregion
    #region Block
    private void Block()
    {
        if (Input.GetKeyDown(KeyCode.S) && !LockBlock)
        {
            LockBlock = true;
            Invoke("BlockLock", 5f);
            Damage = 0;
            if (Input.GetButtonDown("Fire1"))
            {
                Collider2D[] enemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, enemy);
                if (enemies.Length != 0)
                {
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        enemies[i].GetComponent<Entity>().GetDamage(0);
                    }
                }
                Timer = TimeAttack;
            }
            if (Input.GetButtonDown("Fire2"))
            {
                Collider2D[] enemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, enemy);
                if (enemies.Length != 0)
                {
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        enemies[i].GetComponent<Entity>().GetDamage(Damage*2);
                    }
                }
                Timer = HeavyTimeAttack;
            }


        }
    }

    private void BlockLock()
    {
        LockBlock = false;
    }
    #endregion
    #region Charge
    private void Charge()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !LockCharge)
        {
            Vector3 vector = transform.right * Input.GetAxis("Horizontal");
            LockCharge = true;
            Invoke("ChargeLock", 2f);
            animator.SetTrigger("charge");
            rb.velocity = new Vector2(0, 0);
            if (vector.x < 0)
            {
                rb.AddForce(Vector2.left * Impulse);
            }
            else
                rb.AddForce(Vector2.right * Impulse);
        }
    }
    private void ChargeLock()
    {
        LockCharge = false;
    }
    #endregion
    #region HeavyAttack
    private void HeavyAttack()
    {
        if (Timer <= 0)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                Collider2D[] enemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, enemy);
                if (enemies.Length != 0)
                {
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        enemies[i].GetComponent<Entity>().GetDamage(Damage * 2);
                    }
                }
                Timer = HeavyTimeAttack;
            }

        }
        else
        {
            Timer -= Time.deltaTime;
        }
    }
}
#endregion
