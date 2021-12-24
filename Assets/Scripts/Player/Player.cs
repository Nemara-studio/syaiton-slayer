using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Stats")]
    public int maxHealthPoint;
    public int currentHealthPoint;
    public int damage;
    public float moveSpeed = 10;
    public float jumpForce = 5;

    private Rigidbody2D rbCharacter;
    private float inputHorizontal;
    private bool jump = false;
    private bool m_FacingRight = true;

    [Header("Check Grounded")]
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private Transform m_GroundCheck;
    const float k_GroundedRadius = .2f;
    private bool m_Grounded;

    [Header("Player Attack")]
    public float startTimeBtwAttack;
    private float timeBtwAttack;
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRangeX;
    public float attackRangeY;

    [Header("Other")]
    [SerializeField] private Animator playerAnim;
    private AudioSource playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        rbCharacter = GetComponent<Rigidbody2D>();

        currentHealthPoint = maxHealthPoint;

        // set ui
        GameUIManager.singleton.UpdateHealthBar(currentHealthPoint, maxHealthPoint);
    }

    // Update is called once per frame
    void Update()
    {
        InputPlayer();
        UpdateAnimation();

        GameUIManager.singleton.UpdateHealthBar(currentHealthPoint, maxHealthPoint);
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
        CheckGrounded();
    }

    private void InputPlayer()
    {
        // MOVE
        inputHorizontal = Input.GetAxis("Horizontal");

        // JUMP
        if (Input.GetKeyDown(KeyCode.W) && m_Grounded)
        {
            jump = true;
        }

        // ATTACK
        if (timeBtwAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                Attack();
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    private void UpdateAnimation()
    {
        // Player walk animation
        if (inputHorizontal != 0)
        {
            playerAnim.SetBool("walk", true);
        }
        else
        {
            playerAnim.SetBool("walk", false);
        }
    }

    private void Move()
    {
        Vector2 targetVelocity= new Vector2(moveSpeed * inputHorizontal, rbCharacter.velocity.y);
        rbCharacter.velocity = targetVelocity;

        if (inputHorizontal > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (inputHorizontal < 0 && m_FacingRight)
        {
            Flip();
        }
    }

    private void Jump()
    {
        if (jump)
            rbCharacter.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

        jump = false;
    }

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void Attack()
    {
        playerAudio.Play();
        playerAnim.SetTrigger("attack");
        Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            Debug.Log($"{enemiesToDamage[i].name}");

            enemiesToDamage[i].gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }

        timeBtwAttack = startTimeBtwAttack;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector2(attackRangeX, attackRangeY));
    }

    public void TakeDamage(int damage)
    {
        currentHealthPoint -= damage;

        // Lose
        if (currentHealthPoint <= 0)
        {
            currentHealthPoint = 0;
            PlayerDie();
        }
    }

    private void PlayerDie()
    {
        // TODO: Lose
    }

    private void CheckGrounded()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
            }
        }
    }
}
