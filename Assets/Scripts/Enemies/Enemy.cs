using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Type")]
    public bool existsInReality;
    public bool existsInImagination;
    public bool movesHorizontally;
    public bool movesVertically;

    public float moveSpeed;
    Rigidbody2D rb;
    BoxCollider2D detectorCollider;
    CapsuleCollider2D bodyCollider;
    SpriteRenderer sr;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        if(existsInReality && GameManager.instance.isReality)
        {
            bodyCollider.enabled = true;
            sr.enabled = true;
            rb.isKinematic = true;
            if(movesHorizontally)
            {
                MoveEnemyHorizontally();
            }
            if(movesVertically)
            {
                MoveEnemyVertically();
            }
        }
        else if(existsInReality && GameManager.instance.isImagination)
        {
            bodyCollider.enabled = false;
            sr.enabled = false;
            rb.isKinematic = false;
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
        }

        if(existsInImagination && GameManager.instance.isImagination)
        {
            bodyCollider.enabled = true;
            sr.enabled = true;
            rb.isKinematic = true;
            if(movesHorizontally)
            {
                MoveEnemyHorizontally();
            }
            if(movesVertically)
            {
                MoveEnemyVertically();
            }
        }
        else if(existsInImagination && GameManager.instance.isReality)
        {
            bodyCollider.enabled = false;
            sr.enabled = false;
            rb.isKinematic = false;
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
        }

    }

    void MoveEnemyHorizontally()
    {
        rb.velocity = new Vector2(moveSpeed, 0f);
    }

    void MoveEnemyVertically()
    {
        rb.velocity = new Vector2(0f, moveSpeed);
    }

    void OnTriggerExit2D(Collider2D otherCollision)
    {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rb.velocity.x)), 1f);
    }
}
