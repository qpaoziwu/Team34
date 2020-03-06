using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public bool isLosingLife;
    public int lifes;
    public Vector3 startingPosition;
    // Start is called before the first frame update
    void Start()
    {
        lifes = 3;
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
