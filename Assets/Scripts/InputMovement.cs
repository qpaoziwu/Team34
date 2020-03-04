using System.Collections;
using UnityEngine;

public class InputMovement : MonoBehaviour
{
    //Lerping Movement Speed
    public float speed;
    public float slowedSpeed;
    public float jumpVelocity;
    public float percent;
    public float changeSpeedInterval;
    private float startTimer = 0;
    private float dropTimer = 0;

    public float initialSpeed;
    public float targetSpeed;

    public AnimationCurve animationCurve;

    //Movement Checks
    public float horizontalInput;
    [Range (1, 10)]
    public float jumpForce;
    public float hookForce;
    public float dropElapsed;
    public bool isCrossing;
    public bool isGrounded;
    public bool isAiming;
    public bool isJumping;
    public bool doubleJumped;
    public bool checkingGround;

    public int playerLayer;
    public int groundLayer;
    public Transform lineCastStart;
    public Transform lineCastEnd;
    public Transform lineCastUpStart;
    public Transform lineCastUpEnd;

    public BoxTarget2D box;
    Animator animatorComponent;
    SpriteRenderer spriteRendererComponent;
    public Rigidbody2D rb;


    bool isFacingRight = true;
    private bool dropping;

    void Awake()
    {
        // Get references to components
        animatorComponent = GetComponent<Animator>();
        spriteRendererComponent = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        playerLayer = LayerMask.GetMask("Player");
        groundLayer = LayerMask.GetMask("Ground");
        isFacingRight = true;
    }

    void Start()
    {
        // reset the current speed and initial speed, in case they've been changed in the Inspector
        initialSpeed = 0;
        speed = 0;
        // reset the timer
        ResetTimer();
    }

    void Update()
    {
        Debug.DrawLine(lineCastStart.position, lineCastEnd.position, Color.green);
        Debug.DrawLine(lineCastUpStart.position, lineCastUpEnd.position, Color.blue);
        playerLayer = LayerMask.GetMask("Player");
        groundLayer = LayerMask.GetMask("Ground");


        // Check if grounded
        isGrounded = (Physics2D.Linecast(lineCastStart.position, lineCastEnd.position, groundLayer)) ? true : false;
        isCrossing = (Physics2D.Linecast(lineCastUpStart.position, lineCastUpEnd.position, groundLayer)) ? true : false;
        isAiming = Input.GetKey(KeyCode.J);
        if (isGrounded)
        {
            doubleJumped = !isGrounded;
            isJumping = !isGrounded;
        }

        ColliderCheck();

        InputHandler();
        // Sprite Animation Parameters
        AnimateSprite();
    }

    void FixedUpdate()
    {
        // move toward the target position using the interpolated speed
        transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.right * horizontalInput, LerpSpeed() * Time.deltaTime);

    }

    void InputHandler()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (isAiming)
        {
            speed = Mathf.Clamp(horizontalInput, slowedSpeed, slowedSpeed);
            if (Input.GetKeyDown(KeyCode.K))
            {
                print("Hookshot!");
                Hookshot();
            }
        }

        if (!isAiming)
        {
            // DropDown Interaction
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (!dropping)
                {
                    dropping = true;
                }
            }

            if (isGrounded && !isJumping)
            {
                if (Input.GetKeyDown(KeyCode.N))
                {
                    isJumping = true;
                    rb.AddRelativeForce(Vector2.up * jumpVelocity + new Vector2( Input.GetAxisRaw("Horizontal")*0.5f, 0f) * Time.deltaTime, ForceMode2D.Impulse);
                }
            }

            if (!isGrounded && !doubleJumped)
            {
                if (Input.GetKeyDown(KeyCode.N))
                {
                    doubleJumped = true;
                    rb.AddRelativeForce(Vector2.up * jumpVelocity*0.9f + new Vector2(Input.GetAxisRaw("Horizontal") * 0.5f, 0f) * Time.deltaTime, ForceMode2D.Impulse);
                    
                }

            }
            
        }

    }

    void Hookshot()
    {
        if (box.TargetsInRange.Count >0)
        {
            if (box.ClosestTarget(gameObject.transform) != gameObject.transform)
            {
                if (HitDirectionCheck(box.TargetsInRange[0]) >= 0.4f)
                {
                    print("Hitting " + box.TargetsInRange[0].name);
                    //TaggedLayers.Add(9); //Player Layer
                    //TaggedLayers.Add(10); //Collectible Layer
                    //TaggedLayers.Add(11); //Enemy Layer
                    //TaggedLayers.Add(12); //Terrian Layer

                    //1) shoot self at Terrian
                    if (box.TargetsInRange[0].gameObject.layer == 12)
                    {
                        Vector2 dirToTarget = box.TargetsInRange[0].position - gameObject.transform.position;
                        rb.AddRelativeForce(dirToTarget.normalized * jumpVelocity*1.5f + new Vector2(Input.GetAxisRaw("Horizontal") * 0.5f, 0f) * Time.deltaTime, ForceMode2D.Impulse);
                    }

                    //2) pull player to self
                    //3) pull collectiable to self
                    if (box.TargetsInRange[0].gameObject.layer == 9 ||
                        box.TargetsInRange[0].gameObject.layer == 10)
                    {
                        Vector2 dirToSelf = gameObject.transform.position - box.TargetsInRange[0].position;
                        box.TargetsInRange[0].GetComponent<Rigidbody2D>().AddRelativeForce(dirToSelf.normalized * jumpVelocity * hookForce + new Vector2(Input.GetAxisRaw("Horizontal") * 0.5f, 0f) * Time.deltaTime, ForceMode2D.Impulse);
                    }
                }
            }
        }
        else
        {
            print("No target in range!");
            //shoot at axis(h,v);
        }
    }

    public float HitDirectionCheck(Transform t)
    {
        //float angleToTarget = Vector2.Dot(box.TargetsByRange[0].position.normalized, gameObject.transform.position.normalized);

        Vector3 dirToTarget = t.position - gameObject.transform.position;

        float angleToAimpoint= Vector2.Dot(dirToTarget.normalized, new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));

        return angleToAimpoint;

    }

    void ColliderCheck()
    {
        if (dropping)
        {
            checkingGround = false;
            gameObject.GetComponent<Collider2D>().enabled = false;

            dropElapsed += Time.deltaTime;
            if (dropElapsed > 0.4f)
            {
                checkingGround = true;

                gameObject.GetComponent<Collider2D>().enabled = true;
                dropElapsed = 0f;
                dropping = false;
            }
        }
        if (isCrossing)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
        if (isGrounded&& checkingGround)
        {
            gameObject.GetComponent<Collider2D>().enabled = true;

        }
    }

    float LerpSpeed()
    {
        if (!isAiming)
        {
            if (Input.GetButton("Horizontal"))
            {
                // the time elapsed since the timer was started
                float timeElapsed = Time.time - startTimer;
                // the percent of time elapsed in relation to the speed change interval
                float percent = timeElapsed / changeSpeedInterval;
                if (percent > 1)
                {
                    ResetTimer();
                }
                // calculate the current speed, using percent and the animation curve
                return speed = Mathf.Lerp(initialSpeed, targetSpeed, animationCurve.Evaluate(percent));
            }
            if (Input.GetButton("Horizontal") && !isGrounded)
            {
                startTimer = 10f;
            }
            if (Input.GetButtonUp("Horizontal") && speed != 0f&& !isJumping)
            {
                return speed = 0.1f;
            }

        }
        if (!isGrounded)
        {
            speed *= 0.5f;
        }
        return speed = Mathf.Lerp(speed, 0f, animationCurve.Evaluate(percent));
    }

    void ResetTimer()
    {
        startTimer += Time.time;
    }

    void AnimateSprite()
    {
        // Flip sprite
        if (horizontalInput < 0 && isFacingRight)
        {
            spriteRendererComponent.flipX = true;
            isFacingRight = false;
        }
        else if (horizontalInput > 0 && !isFacingRight)
        {
            spriteRendererComponent.flipX = false;
            isFacingRight = true;
        }

        // Set Animator variables
        animatorComponent.SetFloat("horizontalSpeed", Mathf.Abs(horizontalInput));
        animatorComponent.SetBool("isGrounded", isGrounded);
        animatorComponent.SetBool("isAiming", isAiming);

    }
}
