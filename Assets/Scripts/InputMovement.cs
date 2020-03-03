using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMovement : MonoBehaviour
{
    //Lerping Movement Speed
    public float speed;
    public float percent;
    public float changeSpeedInterval;
    float startTimer;

    public float initialSpeed;
    public float targetSpeed;

    public AnimationCurve animationCurve;

    //Movement Checks
    public float horizontalInput;
    public float jumpForce;

    public bool isGrounded;

    public Transform lineCastStart;
    public Transform lineCastEnd;

    Animator animatorComponent;
    SpriteRenderer spriteRendererComponent;

    bool isFacingRight = true;

    void Awake()
    {
        // Get references to components
        animatorComponent = GetComponent<Animator>();
        spriteRendererComponent = GetComponent<SpriteRenderer>();

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

    // Update is called once per frame
    void Update()
    {

        Debug.DrawLine(lineCastStart.position, lineCastEnd.position, Color.green);

        // Check if grounded
        int groundLayerMask = LayerMask.GetMask("Ground");
        isGrounded = (Physics2D.Linecast(lineCastStart.position, lineCastEnd.position, groundLayerMask)) ? true : false;


        // Input
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //ShootHook
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

    void FixedUpdate()
    {
        // move toward the target position using the interpolated speed
        transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.right * horizontalInput, LerpSpeed() * Time.deltaTime);

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
                ResetTimer();
            }
            // calculate the current speed, using percent and the animation curve
            return speed = Mathf.Lerp(initialSpeed, targetSpeed, animationCurve.Evaluate(percent));
        }

        //if (Input.GetButtonUp("Horizontal")&& speed!=0f)
        //{
        //    return speed = Mathf.Lerp(speed, 0f, animationCurve.Evaluate(percent));
        //}

            return speed = Mathf.Lerp(speed, 0f , animationCurve.Evaluate(percent));

    }

    void ResetTimer()
    {
        startTimer = Time.time;
    }

}
