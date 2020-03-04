﻿using System.Collections;
using UnityEngine;

public class InputMovement : MonoBehaviour
{
<<<<<<< HEAD
    [Range(0, 2)]
=======
    public int collectedItems;
    [Range(0,2)]
>>>>>>> 5ac026a668c9eaef8a773ce6f34eb6771a40c489
    public int inputMode;
    //Lerping Movement Speed
    public float speed;
    public float slowedSpeed;
    public float jumpVelocity;
    public float percent;
    public float changeSpeedInterval;
    private float startTimer = 0;


    public float initialSpeed;
    public float targetSpeed;
    public float velLimit = 2.6f;
    public AnimationCurve animationCurve;

    //Movement Checks
    public float horizontalInput;
    [Range(1, 10)]
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
<<<<<<< HEAD

=======
    private LineRenderer rope;
    public float ropeCancelDistance;
    public float pullSpeed;
>>>>>>> 5ac026a668c9eaef8a773ce6f34eb6771a40c489

    string[] keyboardInput = new string[8];
    string[] p1Intput = new string[8];
    string[] p2Input = new string[8];

    private ObjectPooler pool;

    private bool isRoping;

    void SetInputs()
    {
        keyboardInput[0] = "";
        keyboardInput[1] = "";
        keyboardInput[2] = "";
        keyboardInput[3] = "";
        keyboardInput[4] = "";
        keyboardInput[5] = "";
        keyboardInput[6] = "";
        keyboardInput[7] = "";

        p1Intput[0] = "";
        p1Intput[1] = "";
        p1Intput[2] = "";
        p1Intput[3] = "";
        p1Intput[4] = "";
        p1Intput[5] = "";
        p1Intput[6] = "";
        p1Intput[7] = "";

        p2Input[0] = "";
        p2Input[1] = "";
        p2Input[2] = "";
        p2Input[3] = "";
        p2Input[4] = "";
        p2Input[5] = "";
        p2Input[6] = "";
        p2Input[7] = "";
    }

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
<<<<<<< HEAD

=======
        pool = GameObject.FindGameObjectWithTag("ObjectPooler").GetComponent<ObjectPooler>();
        rope = GetComponent<LineRenderer>();
>>>>>>> 5ac026a668c9eaef8a773ce6f34eb6771a40c489
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
        if (Mathf.Abs(rb.velocity.x) < velLimit)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        // Check if grounded
        isGrounded = (Physics2D.Linecast(lineCastStart.position, lineCastEnd.position, groundLayer)) ? true : false;
        if(dropping)
        isCrossing = (Physics2D.Linecast(lineCastUpStart.position, lineCastUpEnd.position, groundLayer)) ? true : false;
<<<<<<< HEAD
        isAiming = Input.GetKey(KeyCode.LeftControl);

=======
        isAiming = Input.GetKey(KeyCode.J);
>>>>>>> 5ac026a668c9eaef8a773ce6f34eb6771a40c489
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
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                print("Hookshot!");
                Hookshot();
            }

            if (Input.GetKeyUp(KeyCode.K))
            {
                isRoping = false;
            }
        }

        if (!isAiming)
        {
            // DropDown Interaction
            if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.Z))
            {
                if (!dropping)
                {
                    dropping = true;
                }
            }

            if (isGrounded && !isJumping)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    //Sound Jump

                    isJumping = true;
                    //rb.AddRelativeForce(Vector2.up * jumpVelocity + new Vector2( Input.GetAxisRaw("Horizontal")*0.5f, 0f) * Time.deltaTime, ForceMode2D.Impulse);
                    rb.velocity = (Vector2.up * jumpVelocity + new Vector2(Input.GetAxisRaw("Horizontal") * 0.5f, 0f) * Time.deltaTime);
                }
            }

            if (!isGrounded && !doubleJumped)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    //Sound Jump

                    doubleJumped = true;
                    // rb.AddRelativeForce(Vector2.up * jumpVelocity*0.9f + new Vector2(Input.GetAxisRaw("Horizontal") * 0.5f, 0f) * Time.deltaTime, ForceMode2D.Impulse);
                    rb.velocity = (Vector2.up * jumpVelocity + new Vector2(Input.GetAxisRaw("Horizontal") * 0.5f, 0f) * Time.deltaTime);

                }

            }

        }

    }

    void Hookshot()
    {
        if (box.TargetsInRange.Count > 0)
        {
            if (box.ClosestTarget(gameObject.transform) != gameObject.transform)
            {
                if (HitDirectionCheck(box.TargetsInRange[0]) >= 0.4f)
                {
<<<<<<< HEAD

=======
>>>>>>> 5ac026a668c9eaef8a773ce6f34eb6771a40c489
                    print("Hitting " + box.TargetsInRange[0].name);
                    //TaggedLayers.Add(9); //Player Layer
                    //TaggedLayers.Add(10); //Collectible Layer
                    //TaggedLayers.Add(11); //Enemy Layer
                    //TaggedLayers.Add(12); //Terrian Layer

                    //1) shoot self at Terrian
                    if (box.TargetsInRange[0].gameObject.layer == 12)
                    {
                        Vector2 dirToTarget = box.TargetsInRange[0].position - gameObject.transform.position;
<<<<<<< HEAD
                        rb.AddRelativeForce(dirToTarget.normalized * jumpVelocity * 1.5f + new Vector2(Input.GetAxisRaw("Horizontal") * 0.5f, 0f) * Time.deltaTime, ForceMode2D.Impulse);
=======
                        StartCoroutine(RopeItUp(box.TargetsInRange[0].transform,false));
                        rb.AddRelativeForce(dirToTarget.normalized * jumpVelocity*1.5f + new Vector2(Input.GetAxisRaw("Horizontal") * 0.5f, 0f) * Time.deltaTime, ForceMode2D.Impulse);
>>>>>>> 5ac026a668c9eaef8a773ce6f34eb6771a40c489
                    }

                    //2) pull player to self
                    //3) pull collectiable to self
                    if (box.TargetsInRange[0].gameObject.layer == 9 ||
                        box.TargetsInRange[0].gameObject.layer == 10)
                    {
                        Vector2 dirToSelf = gameObject.transform.position - box.TargetsInRange[0].position;
                        StartCoroutine(RopeItUp(box.TargetsInRange[0].transform, true));
                        box.TargetsInRange[0].GetComponent<Rigidbody2D>().AddRelativeForce(dirToSelf.normalized * jumpVelocity * hookForce + new Vector2(Input.GetAxisRaw("Horizontal") * 0.5f, 0f) * Time.deltaTime, ForceMode2D.Impulse);
                    }
                }
            }
        }
        else
        {
            print("No target in range!");
            //Sound Grapple_error

            //shoot at axis(h,v);
        }
    }

    private IEnumerator RopeItUp(Transform _target, bool _pull){
        rope.enabled = true;
        //Sound Grapple_Connect
        isRoping = true;
        bool _iscollectible = _target.CompareTag("COL");
        if (_iscollectible)
        {
            _target.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }

        while (isRoping){
            if (Vector2.Distance(transform.position, _target.position)<ropeCancelDistance)
            {
                isRoping = false;
                if (_iscollectible)
                {
                    collectedItems++;
                    //Sound item_collect

                    pool.Drown(_target.gameObject);
                    //collect gem
                }
            }
            rope.SetPosition(0, transform.position);
            rope.SetPosition(1, _target.position);
            if (_pull)
            {
                _target.position = Vector2.MoveTowards(_target.position, transform.position, pullSpeed * Time.deltaTime);
            }
            yield return new WaitForEndOfFrame();
        }
        rope.enabled = false;
    }
    
    public float HitDirectionCheck(Transform t)
    {
        //float angleToTarget = Vector2.Dot(box.TargetsByRange[0].position.normalized, gameObject.transform.position.normalized);

        Vector3 dirToTarget = t.position - gameObject.transform.position;

        float angleToAimpoint = Vector2.Dot(dirToTarget.normalized, new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));

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
        if (isGrounded && checkingGround)
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
                float _percent = timeElapsed / changeSpeedInterval;
                if (_percent > 1)
                {
                    ResetTimer();
                }
                // calculate the current speed, using percent and the animation curve
                return speed = Mathf.Lerp(initialSpeed, targetSpeed, animationCurve.Evaluate(_percent));
            }
            if (Input.GetButton("Horizontal") && !isGrounded)
            {
                startTimer = 10f;
            }
            if (Input.GetButtonUp("Horizontal") && speed != 0f && !isJumping)
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
