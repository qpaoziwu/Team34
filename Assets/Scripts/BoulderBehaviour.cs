using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderBehaviour : MonoBehaviour
{

    private void Update()
    {
        if(transform.position.y < -10)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            print("Player is hit");
            collision.gameObject.GetComponent<PlayerBehaviour>().isLosingLife = true;
        }
    }
}
