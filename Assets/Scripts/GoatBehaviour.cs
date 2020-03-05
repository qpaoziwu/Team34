using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatBehaviour : MonoBehaviour
{
    private bool flipCharicter;
    private bool leftRight; // If false then moving left |||| if true than moving right
    public float yOffset;
    private float movementSpeed;
    private Vector3 startingPosition;
    public Animator animator;
    private bool isHit;


    // Start is called before the first frame update
    void Start()
    {
        isHit = false;
        startingPosition = transform.position;
        movementSpeed = 0.8f;
        leftRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        turnAround();
        moveGoat();
    }


    private void turnAround()
    {
        if(transform.position.x > startingPosition.x + 0.5f || transform.position.x < startingPosition.x - 0.5f)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
            if (leftRight == true)
            {
                leftRight = false;
            }
            else if (leftRight == false)
            {
                leftRight = true;
            }
        }
    }

    private void moveGoat()
    {
        
        if (leftRight == true)
        {
            transform.Translate(new Vector3(movementSpeed * Time.deltaTime, 0, 0));
        }
        else if (leftRight == false)
        {
            transform.Translate(new Vector3(movementSpeed * -1 * Time.deltaTime, 0, 0));
        }
        
        if(isHit == true)
        {
            print("FUCKEN U");
            animator.SetBool("isHeadbutting", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            isHit = true;
            if (collision.gameObject.transform.position.x > transform.position.x)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity
                    = (Vector2.right * 4 + new Vector2(Input.GetAxisRaw("Horizontal") * 0.5f, 0f) * Time.deltaTime);
                collision.gameObject.GetComponent<PlayerBehaviour>().isLosingLife = true;
            }
            else if(collision.gameObject.transform.position.x < transform.position.x)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity
                    = (Vector2.left * 4 + new Vector2(Input.GetAxisRaw("Horizontal") * 0.5f, 0f) * Time.deltaTime);
                collision.gameObject.GetComponent<PlayerBehaviour>().isLosingLife = true;
            }
            

        }
    }
}
