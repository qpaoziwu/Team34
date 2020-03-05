using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lavaScript : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip hurtSFX;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {

            audioSource.PlayOneShot(hurtSFX, 0.75f);

            collision.gameObject.GetComponent<Rigidbody2D>().velocity
                = (Vector2.up * collision.gameObject.GetComponent<InputMovement>().jumpVelocity * 2 + new Vector2(Input.GetAxisRaw("Horizontal") * 0.5f, 0f) * Time.deltaTime);
            collision.gameObject.GetComponent<PlayerBehaviour>().isLosingLife = true;
        }
    }
}
