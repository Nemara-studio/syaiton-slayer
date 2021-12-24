using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocong : Enemy
{
    private Rigidbody2D enemyRb;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpTime;
    private float currentJumpTime = 0;

    [Header("Check Grounded")]
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private Transform m_GroundCheck;
    const float k_GroundedRadius = .2f;
    private bool m_Grounded;

    private float direction = 0;

    protected override void Start()
    {
        base.Start();

        enemyAnim = GetComponent<Animator>();
        enemyRb = GetComponent<Rigidbody2D>();
        enemyTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        Move();
    }

    protected override void Move()
    {
        base.Move();

        float moveHorizontal = direction;
        Vector3 movement = new Vector3(moveHorizontal * currentStats.moveSpeed, 0, 0);

        if (enemyTarget.position.x > transform.position.x)
        {
            direction = 1;
        }
        else if (enemyTarget.position.x < transform.position.x)
        {
            direction = -1;
        }

        if (m_Grounded)
        {
            if (currentJumpTime <= jumpTime)
            {
                currentJumpTime += Time.deltaTime;
            }
            else
            {
                currentJumpTime = 0;
                enemyRb.AddForce(new Vector2(direction * currentStats.moveSpeed, jumpForce), ForceMode2D.Impulse);
            }
        }
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
