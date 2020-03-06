using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public int playerID;
    private InputMovement _main;
    private LevelController _level;
    public bool isLosingLife;
    public int lifes;
    public Vector3 startingPosition;
    // Start is called before the first frame update
    void Start()
    {
        lifes = 3;
        startingPosition = transform.position;
        isLosingLife = false;
        _main = GetComponent<InputMovement>();
        _level = GameObject.FindGameObjectWithTag("LevelScroller").GetComponent<LevelController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (lifes <= 0)
        {
            Die();
        }
        if (isLosingLife == true)
        {
            lifes -= 1;
            isLosingLife = false;
            StartCoroutine(flickerSprite(this.gameObject));
        }
    }

    private void Die()
    {
        int _score = _main.collectedItems + (_level.height * 10);

        PlayerPrefs.SetInt("Score", _score);
        PlayerPrefs.SetInt("WinningPlayer", playerID);

        SceneManager.LoadScene(2);
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
