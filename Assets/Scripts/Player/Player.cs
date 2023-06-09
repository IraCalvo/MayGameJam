using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    Vector2 moveInput;
    public LayerMask groundLayer;
    SpriteRenderer sr;
    private float currentHorizontalSpeed;
    Camera camera;
    // private float currentVerticalSpeed;

    [Header("Jumping")]
    private bool isGrounded;
    public float groundCheckDistance;
    public float jumpForce;
    public float airJumpForce;
    private float apexPoint;
    private float jumpApexThreshold = 50f;
    public float apexBonus;
    public int jumpAmount;
    int currentJumpAmount;
    public float peakJumpGravity;
    public float groundCheckDistanceBuffer;

    private float coyoteTime = 0.25f;
    private float coyoteTimeCounter;

    //jump checkers
    private bool jumped = false;
    private bool airJumped = false;
    private bool coyoteJumping = false;

    [Header("Running")]
    public float acceleration;
    public float decceleration;
    public float movementClamp;

    // [Header("Dashing")]
    // public float dashForce;

    [Header("SFX")]
    public int groundJumpSFX;

    [Header("Shoot")]
    public GameObject banana;
    private bool bananaAvailable;

    //TODO:
    //Squash and stretch done sorta
    //detect block above head to push player
    //fix coyote triple jump

    void Awake()
    {
        camera = Camera.main;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        currentJumpAmount = jumpAmount;
        bananaAvailable = true;
    }

    void Update()
    {
        FlipSprite();
    }

    void FixedUpdate()
    {
        Move();
        CoyoteTiming();
        CalculateJumpApex();
        GravityAtApex();
        CheckIfGrounded();
    }

    void FlipSprite()
    {
        if(moveInput.x < 0)
        {
            sr.flipX = true;
        }
        if(moveInput.x > 0)
        {
            sr.flipX = false;
        }

        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if(playerHasHorizontalSpeed)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }


    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Move()
    {
        if(moveInput.x != 0)
        {
            currentHorizontalSpeed += moveInput.x * acceleration * Time.deltaTime;
            currentHorizontalSpeed = Mathf.Clamp(currentHorizontalSpeed, -movementClamp, movementClamp);

            //apply bonus speed at apex of jump here
            var apexBonusAtPeak = Mathf.Sign(moveInput.x) * apexBonus * apexPoint;
            currentHorizontalSpeed += apexBonus * Time.deltaTime;
        }
        else
        {
            currentHorizontalSpeed = Mathf.MoveTowards(currentHorizontalSpeed, 0, decceleration * Time.deltaTime);
        }

        rb.velocity = new Vector2(currentHorizontalSpeed, rb.velocity.y);        
    }

    void OnJump(InputValue value)
    {
        if(isGrounded)
        {
            Debug.Log("Grounded jump");
            Jump();
        }

        //coyote grace period checker
        else if(coyoteTimeCounter > 0 && currentJumpAmount == jumpAmount)
        {
            Debug.Log("Coyote jump!");

            coyoteJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            Jump();
        }
        //regular air jumping
        else if((currentJumpAmount > 0 && !isGrounded))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistanceBuffer, groundLayer);
            if(hit)
            {
                Debug.Log("Close enough to the ground");
                StartCoroutine(JumpWhenGroundedFrame1());
            }
            else
            {
                Debug.Log("Air jumped");
                currentJumpAmount--;
                AirJump();
            }

        }
        
        //check if they are trying to buffer a jump
        else if(!isGrounded)
        {
            JumpBuffer();
        }
    }

    void Jump()
    {
        //QoL
        anim.SetTrigger("jump");
        AudioManager.instance.PlaySFX(groundJumpSFX);

        //actual code to jump
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumped = true;
        currentJumpAmount--;
    }

    void AirJump()
    {
        //QoL
        anim.SetTrigger("jump");
        AudioManager.instance.PlaySFX(groundJumpSFX);

        // Reset y velocity so that the double jump is consistent with regular jump
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * airJumpForce, ForceMode2D.Impulse);
        airJumped = true;
    }

    void JumpBuffer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistanceBuffer, groundLayer);

        //ground detected
        if(hit)
        {
            Debug.Log("Buffered Correctly");
            StartCoroutine(JumpWhenGroundedFrame1());
        }
        else
        {
            Debug.Log("Player tried to buffer to early");
        }
    }

    void CoyoteTiming()
    {
        if(isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    void CalculateJumpApex()
    {
        if(!isGrounded && jumped)
        {
            apexPoint = Mathf.InverseLerp(jumpApexThreshold, 0, Mathf.Abs(rb.velocity.y));
        }
        else
        {
            apexPoint = 0;
        }
    }

    void GravityAtApex()
    {
        if(rb.velocity.y < 0)
        {
            rb.gravityScale = peakJumpGravity;
        }
        else
        {
            rb.gravityScale = 2.5f;
        }
    }


    void CheckIfGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        if(hit.collider != null)
        {
            Debug.Log("HIT!");
            //TODO: fix when to play the ladning animation, currently it also plays when the player jumps
            // if(jumped || airJumped || !isGrounded && currentJumpAmount == jumpAmount)
            // {
            //     Debug.Log("play squish animation");
            //     Debug.Log("jumped is:" + jumped);
            //     Debug.Log("airJumped is:" + airJumped);
            //     Debug.Log("isGrounded is:" + isGrounded);
            //     Debug.Log("currentJumpAmountIs:" + currentJumpAmount);
            // }
            isGrounded = true;
            currentJumpAmount = jumpAmount;
            if(jumped)
            {
                Debug.Log("jumped turned false");
                jumped = false;
            }
            if(airJumped)
            {
                airJumped = false;
            }
            if(coyoteJumping)
            {
                coyoteJumping = false;
            }

        }
        else
        {
            isGrounded = false;
        }
    }

    IEnumerator JumpWhenGroundedFrame1()
    {
        while(!isGrounded)
        {
            yield return null;
        }

        Jump();
        Debug.Log("Launched");
    }

    // void OnDash(InputValue value)
    // {
    //     Vector2 dashDirection = new Vector2(moveInput.x, moveInput.y);
    //     rb.velocity = dashDirection * dashForce;
    //     Debug.Log("called!");
    // }

    void OnFire(InputValue value)
    {
        if(bananaAvailable)
        {
            GameObject newBoomerang = Instantiate(banana, transform.position, Quaternion.identity);

            Vector3 shootLocation = Mouse.current.position.ReadValue();
            Vector3 worldMousePosition = camera.ScreenToWorldPoint(shootLocation);
            Vector3 direction = (worldMousePosition - transform.position).normalized;

            Boomerang boomerang = newBoomerang.GetComponent<Boomerang>();
            boomerang.Fire(direction);

            bananaAvailable = false;
        }

    }

}
