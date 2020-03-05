using System.Collections;
using UnityEngine;

public class InputMovement : MonoBehaviour
{

    public int collectedItems;
    [Range(0,2)]
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

    AudioSource audioSource;


    bool isFacingRight = true;
    private bool dropping;

    [Header("GrapplingPoint Settings")]
    public float selfPullPower;
    public float collectiblePullPower;
    public float shotSpeed;
    public float minDistance;
    public float maxDistance;
    private LineRenderer rope;

    public float pullSpeed;

    KeyCode[] keyboardInput = new KeyCode[8];
    KeyCode[] p1Input = new KeyCode[8];
    KeyCode[] p2Input = new KeyCode[8];

    private ObjectPooler pool;

    private bool isRoping;

    //Audio clips
    public AudioClip jump;
    public AudioClip grappleConnect;
    public AudioClip grappleThrow;
    public AudioClip grappleError;
    public AudioClip gemGet;
    void SetInputs()
    {
        //aim,shoot,jump,drop
        keyboardInput[0] = KeyCode.J;
        keyboardInput[1] = KeyCode.K;
        keyboardInput[2] = KeyCode.N;
        keyboardInput[3] = KeyCode.M;
        keyboardInput[4] = KeyCode.W;
        keyboardInput[5] = KeyCode.S;
        keyboardInput[6] = KeyCode.A;
        keyboardInput[7] = KeyCode.D;

        p1Input[0] = KeyCode.LeftControl;
        p1Input[1] = KeyCode.LeftAlt;
        p1Input[2] = KeyCode.LeftShift;
        p1Input[3] = KeyCode.Z;
        p1Input[4] = KeyCode.UpArrow;
        p1Input[5] = KeyCode.DownArrow;
        p1Input[6] = KeyCode.LeftArrow;
        p1Input[7] = KeyCode.RightArrow;

        p2Input[0] = KeyCode.A;
        p2Input[1] = KeyCode.S;
        p2Input[2] = KeyCode.W;
        p2Input[3] = KeyCode.E;
        p2Input[4] = KeyCode.R;
        p2Input[5] = KeyCode.F;
        p2Input[6] = KeyCode.D;
        p2Input[7] = KeyCode.G;
    }
    void Awake()
    {
        // Get references to components
        animatorComponent = GetComponent<Animator>();
        spriteRendererComponent = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        playerLayer = LayerMask.GetMask("Player");
        groundLayer = LayerMask.GetMask("Ground");
        isFacingRight = true;
        SetInputs();
    }
    void Start()
    {
        pool = GameObject.FindGameObjectWithTag("ObjectPooler").GetComponent<ObjectPooler>();
        rope = GetComponent<LineRenderer>();

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
        //if(dropping)
        isCrossing = (Physics2D.Linecast(lineCastUpStart.position, lineCastUpEnd.position, groundLayer)) ? true : false;


        if (isGrounded)
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
            doubleJumped = !isGrounded;
            isJumping = !isGrounded;
        }

        ColliderCheck();

        InputHandler(InputSelect());
        // Sprite Animation Parameters
        AnimateSprite();
    }
    void FixedUpdate()
    {
        // move toward the target position using the interpolated speed
        transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.right * horizontalInput, LerpSpeed() * Time.deltaTime);

    }
    KeyCode[] InputSelect()
    {
        if (inputMode == 0)
        {
            return keyboardInput;
        }
        if (inputMode == 1)
        {
            return p1Input;
        }
        if (inputMode == 2)
        {
            return p2Input;
        }
        return keyboardInput;
    }
    private void OnCollisionEnter2D(Collision2D c)
    {
        if(c.gameObject.layer == 10)
        {
            audioSource.PlayOneShot(gemGet, 1.0f);

            c.gameObject.SetActive(false);
            collectedItems += 1;

        }

        if (c.transform.CompareTag("COL"))
        {
            collectedItems++;
            //Sound item_collect
            audioSource.PlayOneShot(gemGet, 1.0f);
            pool.Drown(c.gameObject);
            //collect gem
        }
    }
    void InputHandler(KeyCode[] k)
    {
        horizontalInput = Input.GetAxis("Horizontal");
        isAiming = Input.GetKey(k[0]);
        if (isAiming)
        {
            speed = Mathf.Clamp(horizontalInput, 0, slowedSpeed);
            if (Input.GetKeyDown(k[1]))
            {
                print("Hookshot!");
                Hookshot();
            }

            if (Input.GetKeyUp(k[1]))
            {
                isRoping = false;
            }
        }

        if (!isAiming)
        {
            // DropDown Interaction
            if (Input.GetKey(k[5]) && Input.GetKey(k[3]))
            {
                if (!dropping)
                {
                    dropping = true;
                }
            }

            if (isGrounded && !isJumping)
            {
                if (Input.GetKeyDown(k[2]))
                {
                    //Sound Jump
                    audioSource.PlayOneShot(jump, 1.0f);

                    isJumping = true;
                    //rb.AddRelativeForce(Vector2.up * jumpVelocity + new Vector2( Input.GetAxisRaw("Horizontal")*0.5f, 0f) * Time.deltaTime, ForceMode2D.Impulse);
                    rb.velocity = (Vector2.up * jumpVelocity + new Vector2(Input.GetAxisRaw("Horizontal") * 0.5f, 0f) * Time.deltaTime);
                }
            }

            if (!isGrounded && !doubleJumped)
            {
                if (Input.GetKeyDown(k[2]))
                {
                    float jumpPitch;

                    jumpPitch = Random.Range(0.8f, 1.2f);

                    audioSource.pitch = jumpPitch;

                    //Sound Jump
                    audioSource.PlayOneShot(jump, 1.0f);

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
                    print("Hitting " + box.TargetsInRange[0].name);
                    //TaggedLayers.Add(9); //Player Layer
                    //TaggedLayers.Add(10); //Collectible Layer
                    //TaggedLayers.Add(11); //Enemy Layer
                    //TaggedLayers.Add(12); //Terrian Layer
                    //1) shoot self at Terrian
                    if (box.TargetsInRange[0].gameObject.layer == 12)
                    {
                        Vector2 dirToTarget = box.TargetsInRange[0].position - gameObject.transform.position;
                        StartCoroutine(RopeItUp(box.TargetsInRange[0].transform,false));
                        rb.velocity = (dirToTarget.normalized * selfPullPower * 1.5f + new Vector2(Input.GetAxisRaw("Horizontal") * 0.5f, 0f) * Time.deltaTime);
                        //rb.AddRelativeForce(dirToTarget.normalized * jumpVelocity*1.5f + new Vector2(Input.GetAxisRaw("Horizontal") * 0.5f, 0f) * Time.deltaTime, ForceMode2D.Impulse);
                    }
                    //2) pull player to self
                    //3) pull collectiable to self
                    if (box.TargetsInRange[0].gameObject.layer == 9 ||
                        box.TargetsInRange[0].gameObject.layer == 10)
                    {
                        Vector2 dirToSelf = gameObject.transform.position - box.TargetsInRange[0].position;
                        StartCoroutine(RopeItUp(box.TargetsInRange[0].transform, true));
                        box.TargetsInRange[0].GetComponent<Rigidbody2D>().AddRelativeForce(dirToSelf.normalized *  collectiblePullPower + new Vector2(Input.GetAxisRaw("Horizontal") * 0.5f, 0f) * Time.deltaTime, ForceMode2D.Impulse);
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
        audioSource.PlayOneShot(grappleConnect, 1.0f);
        isRoping = true;
        while (isRoping){
            float _distance = Vector2.Distance(transform.position, _target.position);
            if (_distance<minDistance||_distance>maxDistance)
            {
                isRoping = false;
                if (_target.CompareTag("COL"))
                {
                    _target.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
                }
            }
            rope.SetPosition(0, transform.position);
            rope.SetPosition(1, _target.position);
            if (_pull)
            {
                if (_target.CompareTag("COL"))
                {
                    _target.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                }
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
