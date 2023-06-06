using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public CameraManager followCam;
    Animator anim;
    Rigidbody2D rb;
    public float playerMoveSpeed;
    Vector2 moveInput;

    [Header("Jumping")]
    private bool isGrounded;
    public float jumpSpeed;
    public int jumpAmount;
    private int currentJumpAmount;
    public List<GameObject> npcCollisions = new List<GameObject>();


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentJumpAmount = jumpAmount;
    }

    void Update()
    {
        Move();
        FlipSprite();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Move()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * playerMoveSpeed, rb.velocity.y);
        rb.velocity =  playerVelocity;
    }
    
    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if(playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    void OnJump(InputValue value)
    {
        if(value.isPressed && currentJumpAmount > 0)
        {
            currentJumpAmount--;
            rb.velocity += new Vector2(0f, jumpSpeed);
            Debug.Log("Jumped!");
            Debug.Log(currentJumpAmount);
        }
    }

    void OnCollisionEnter2D(Collision2D otherCollision)
    {
        if(otherCollision.gameObject.layer == LayerMask.NameToLayer("Ground") && currentJumpAmount < jumpAmount)
        {
            currentJumpAmount = jumpAmount;
        }
    }

    void OnCollisionExit2D(Collision2D otherCollision)
    {
    }

    void OnCam(InputValue value) {
        Debug.Log("Switch camera");
        followCam.switchCamera();
    }

    void OnInteract(InputValue value) {
        Debug.Log("Searching Interactions");
        foreach (GameObject npcGameObject in npcCollisions)
        {
            Charles charles = (Charles) npcGameObject.GetComponent<Charles>();
            charles.showDialogue();
        }
    }

}
