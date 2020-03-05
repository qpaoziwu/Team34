using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public bool isLosingLife;
    private int lifes;
    public Vector3 startingPosition;
    private bool hitOnceWithoutGems;
    // Start is called before the first frame update
    void Start()
    {
        lifes = 3;
        hitOnceWithoutGems = false;
        startingPosition = transform.position;
        isLosingLife = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (lifes <= 0)
        {
            Destroy(this.gameObject);
        }
        if (isLosingLife == true)
        {
            lifes -= 1;
            isLosingLife = false;
            StartCoroutine(flickerSprite(this.gameObject));
            this.GetComponent<InputMovement>().collectedItems = 0;
            hitOnceWithoutGems = true;
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
