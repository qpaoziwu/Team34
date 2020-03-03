using System.Collections;
using UnityEngine;

public class InputMovement : MonoBehaviour
{
    //Lerping Movement Speed
    public float speed;
    public float percent;
    public float changeSpeedInterval;
    float startTimer;
    float dropTimer;

    public float initialSpeed;
    public float targetSpeed;

    public AnimationCurve animationCurve;

    //Movement Checks
    public float horizontalInput;
    public float jumpForce;
    public float dropElapsed;
    public bool isGrounded;

    public int playerLayer;
    public int groundLayer;
    public Transform lineCastStart;
    public Transform lineCastEnd;
    public BoxCollider2D box;
    Animator animatorComponent;
    SpriteRenderer spriteRendererComponent;

    bool isFacingRight = true;
    private bool dropping;

    void Awake()
    {
        // Get references to components
        animatorComponent = GetComponent<Animator>();
        spriteRendererComponent = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
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
        ResetTimer(startTimer);
        ResetTimer(dropTimer);

    }
    void FixedUpdate()
    {
        // move toward the target position using the interpolated speed
        transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.right * horizontalInput, LerpSpeed() * Time.deltaTime);

    }
    // Update is called once per frame
    void Update()
    {
        DropDownCheck();
        Debug.DrawLine(lineCastStart.position, lineCastEnd.position, Color.green);

        // Check if grounded
        isGrounded = (Physics2D.Linecast(lineCastStart.position, lineCastEnd.position, groundLayer)) ? true : false;


        // Input
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //ShootHook
        }
        ResetTimer(dropTimer);
        if (Input.GetButtonDown("Jump"))
        {
            if (!dropping)
            {
                ResetTimer(dropTimer);
                dropping = true;
            }
            //print("Jump!");
            //transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.up * Input.GetAxis("Vertical"), 10 * Time.deltaTime);
            
            //StartCoroutine("JumpOff");
            //print("Jumped!");

        }
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

    }
    //public IEnumerable JumpOff()
    //{
    //    print("Jump!");
    //    gameObject.GetComponent<Collider2D>().enabled = false;
    //    //Physics2D.IgnoreLayerCollision(playerLayer, groundLayer, true);
    //    yield return new WaitForSeconds(0.4f);
    //    gameObject.GetComponent<Collider2D>().enabled = true;
    //    //Physics2D.IgnoreLayerCollision(playerLayer, groundLayer, false);
    //}
    void DropDownCheck()
    {
        if (dropping)
        {
            print("Drop!");
            gameObject.GetComponent<Collider2D>().enabled = false;

            dropElapsed += Time.deltaTime;
            if (dropElapsed > 0.4f)
            {
                print("Dropped!");

                gameObject.GetComponent<Collider2D>().enabled = true;
                dropElapsed = 0f;
                dropping = false;

            }
        }
    }


    float LerpSpeed()
    {

        if (Input.GetButton("Horizontal"))
        {
            
            // the time elapsed since the timer was started
            float timeElapsed = Time.time - startTimer;
            // the percent of time elapsed in relation to the speed change interval
            float percent = timeElapsed / changeSpeedInterval;
            if (percent > 1)
            {
                ResetTimer(startTimer);
            }
            // calculate the current speed, using percent and the animation curve
            return speed = Mathf.Lerp(initialSpeed, targetSpeed, animationCurve.Evaluate(percent));
        }

        if (Input.GetButtonUp("Horizontal") && speed != 0f)
        {
            return speed = 0f;
        }

        return speed = Mathf.Lerp(speed, 0f , animationCurve.Evaluate(percent));

    }

    void ResetTimer(float t)
    {
        t = Time.time;
    }

}
