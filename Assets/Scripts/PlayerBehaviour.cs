using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public int lives;
    public bool isLosingLife;
    public Vector3 startingPosition;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        lives = 3;
        isLosingLife = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isLosingLife == true)
        {
            lives -= 1;
            isLosingLife = false;
            StartCoroutine(flickerSprite(this.gameObject));
            
        }
        //if the player has it the bottom of the screen

        if(lives <= 0)
        {
            //This is where one player will be eleminated 
            //If the ammount of objects witht he player tag is less or equal to zero then the game is reset
            Destroy(this.gameObject);
        }
        
    }



    private IEnumerator flickerSprite(GameObject player)
    {
        player.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.15f);
        player.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.15f);

        player.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.15f);
        player.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.15f);

        player.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.15f);
        player.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.15f);

        player.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.15f);
        player.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.15f);

        player.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.15f);
        player.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.15f);
    }

    
}
