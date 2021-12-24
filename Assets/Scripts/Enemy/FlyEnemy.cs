using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : Enemy
{
    [SerializeField] private float distanceOffset;
    private float direction = 0;

    protected override void Start()
    {
        base.Start();

        enemyAnim = GetComponent<Animator>();
        enemyTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        Move();
    }

    protected override void Move()
    {
        base.Move();

        if (enemyTarget.position.x > transform.position.x)
        {
            direction = 1;
        }

        if (enemyTarget.position.x < transform.position.x)
        {
            direction = -1;
        }

        float moveHorizontal = direction;

        if (enemyTarget.position.x > transform.position.x + distanceOffset || enemyTarget.position.x < transform.position.x - distanceOffset)
        {
            Vector3 movement = new Vector3(moveHorizontal, 0, 0);
            transform.Translate(movement * currentStats.moveSpeed / 50);
        }
        else
        {
            Vector3 movement = new Vector3(0, -currentStats.moveSpeed / 10, 0);
            transform.Translate(movement);
        }
        
    }
}
