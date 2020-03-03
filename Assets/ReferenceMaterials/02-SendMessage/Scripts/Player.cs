using UnityEngine;

public class Player : MonoBehaviour {

    public Transform playerSprite;
    public Transform useArea;

    bool isFacingRight = true;

    float horizontalInput;
    public float speed;
    public float jumpForce;

    Rigidbody2D rigidbody2DComponent;
    Animator animatorComponent;
    SpriteRenderer spriteRendererComponent;

    void Awake() {
        // get references to components
        animatorComponent = playerSprite.GetComponent<Animator>();
        rigidbody2DComponent = GetComponent<Rigidbody2D>();
        spriteRendererComponent = playerSprite.GetComponent<SpriteRenderer>();
    }

    void Update() {

        // get input
        horizontalInput = Input.GetAxis("Horizontal");

        // jump
        if (Input.GetButtonDown("Jump")) {
            rigidbody2DComponent.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        // flip the sprite and the use area
        if (horizontalInput < 0 && isFacingRight) {
            spriteRendererComponent.flipX = true;
            useArea.localScale = new Vector3(-1, 1, 1);
            isFacingRight = false;
        } else if (horizontalInput > 0 && !isFacingRight) {
            spriteRendererComponent.flipX = false;
            useArea.localScale = new Vector3(1, 1, 1);
            isFacingRight = true;
        }

        // set the Animator paramaters
        animatorComponent.SetFloat("horizontalSpeed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
    }

    void FixedUpdate() {

        // Move the player horizontally based on input
        // NOTE THAT THIS IS A HACK.   Best practice is to use AddForce, but controlling
        // the velocity of the player controller would require a lot more code.
        // this scene is really about demonstrating how to use the Animator, not how to make a good player controller.
        rigidbody2DComponent.velocity = new Vector2(horizontalInput * speed, rigidbody2DComponent.velocity.y);

    }

}
