using System.Collections;
using UnityEngine;

public class InputMovement : MonoBehaviour
{
    //Lerping Movement Speed
    public float speed;
    public float slowedSpeed;
    public float percent;
    public float changeSpeedInterval;
    private float startTimer = 0;
    private float dropTimer = 0;

    public float initialSpeed;
    public float targetSpeed;

    public AnimationCurve animationCurve;

    //Movement Checks
    public float horizontalInput;
    public float jumpForce;
    public float dropElapsed;
    public bool isGrounded;
    public bool isAiming;

    public int playerLayer;
    public int groundLayer;
    public Transform lineCastStart;
    public Transform lineCastEnd;
    //public BoxCollider2D box;
    Animator animatorComponent;
    SpriteRenderer spriteRendererComponent;

    bool isFacingRight = true;
    private bool dropping;

    void Awake()
    {
        // Get references to components
        animatorComponent = GetComponent<Animator>();
        spriteRendererComponent = GetComponent<SpriteRenderer>();

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

        // Check if grounded
        isGrounded = (Physics2D.Linecast(lineCastStart.position, lineCastEnd.position, groundLayer)) ? true : false;
        isAiming = Input.GetKey(KeyCode.J);

        DropDownCheck();




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
            if (Input.GetKeyDown(KeyCode.K) && isGrounded)
            {
                Hookshot();
                print("Hookshot!");
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
        }

    }

    void Hookshot()
    {

    }

    void DropDownCheck()
    {
        if (dropping)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;

            dropElapsed += Time.deltaTime;
            if (dropElapsed > 0.4f)
            {
                gameObject.GetComponent<Collider2D>().enabled = true;
                dropElapsed = 0f;
                dropping = false;
            }
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
            if (Input.GetButtonUp("Horizontal") && speed != 0f)
            {
                return speed = 0f;
            }
        }
        return speed = Mathf.Lerp(speed, 0f, animationCurve.Evaluate(percent));
    }

    void ResetTimer()
    {
        startTimer = Time.time;
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
