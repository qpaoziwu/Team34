using UnityEngine;

public class Rabbit : MonoBehaviour {

    public GameObject gameManager;
    SpriteRenderer spriteRendererComponent;
   
    void Awake() {
        // get references to components
        spriteRendererComponent = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        // if rabbit is hit by the crate   
        if (collision.gameObject.name == "Crate") {
            // set color to black 
            spriteRendererComponent.color = Color.black;
            // send a message to the game manager that the player has died
            gameManager.SendMessage("OnPlayerDied");
        }
    }
}
