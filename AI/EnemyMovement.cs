using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D enemyRigidbody;
    CapsuleCollider2D enemyCapsuleCollider;
    BoxCollider2D enemyBoxCollider;
    private float scaleAtStart;

    [SerializeField] float moveSpeed = 1f;
    Health health;

    private void Start()
    {
        health = GetComponent<Health>();
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyCapsuleCollider = GetComponent<CapsuleCollider2D>();
        enemyBoxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if(!health.isDead) 
        {
            enemyRigidbody.velocity = new Vector2(moveSpeed, 0f);
        }
        else if(health.isDead) 
        {
            enemyRigidbody.velocity = new Vector2(0f, 0f);
        }
        
    }


    public void FlipEnemyFacing()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public void PeriscopeFunction(Collider2D other) 
    {
        if (other.gameObject.tag == "Environment")
        {
            moveSpeed = -moveSpeed;
            FlipEnemyFacing();
        }
    }

}
