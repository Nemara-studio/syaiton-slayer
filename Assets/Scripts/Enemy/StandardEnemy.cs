using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardEnemy : Enemy
{
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
        else if (enemyTarget.position.x < transform.position.x)
        {
            direction = -1;
        }

        float moveHorizontal = direction;
        Vector3 movement = new Vector3(moveHorizontal, 0, 0);
        transform.Translate(movement * currentStats.moveSpeed / 50);

        
    }
}
