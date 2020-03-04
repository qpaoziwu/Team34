using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lavaScript : MonoBehaviour
{ 
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity 
                = (Vector2.up * collision.gameObject.GetComponent<InputMovement>().jumpVelocity * 2 + new Vector2(Input.GetAxisRaw("Horizontal") * 0.5f, 0f) * Time.deltaTime);
            collision.gameObject.GetComponent<PlayerBehaviour>().isLosingLife = true;
        }
    }
}
