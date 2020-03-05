using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitileManager : MonoBehaviour
{
    private bool readyToGo;

    public Text playerOneText;
    public Text playerTwoText;
    private GameObject[] collectables;

    public GameObject playerOne;
    //public GameObject playerTwo;
    // Start is called before the first frame update
    void Start()
    {
        readyToGo = false;

    }

    // Update is called once per frame
    void Update()
    {
        startGame();
        restartPlayer();
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(flickeringText(playerOneText));
            Instantiate(playerOne, new Vector3(-2, -3,0), Quaternion.identity);
            playerOne.GetComponent<Rigidbody2D>().velocity
                = (Vector2.up * playerOne.gameObject.GetComponent<InputMovement>().jumpVelocity * 2 + new Vector2(Input.GetAxisRaw("Horizontal") * 0.5f, 0f) * Time.deltaTime);
            playerOne.gameObject.GetComponent<PlayerBehaviour>().isLosingLife = true;
        }


        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartCoroutine(flickeringText(playerTwoText));
        }
    }

    public void restartPlayer()
    {
        GameObject playerOne = GameObject.FindGameObjectWithTag("Player");
        if (playerOne != null)
        {
            if (playerOne.transform.position.y < -5.2f)
            {
                playerOne.transform.position = new Vector3(-2, -3, 0);
            }
        }
    }

    private void startGame()
    {

        collectables = GameObject.FindGameObjectsWithTag("COL");
        print(collectables.Length);
        if(collectables.Length <= 0)
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
    }


    public IEnumerator flickeringText(Text text)
    {
        text.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        text.color = Color.white;
        yield return new WaitForSeconds(0.15f);
        text.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        text.color = Color.white;
        yield return new WaitForSeconds(0.15f);
        text.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        text.color = Color.white;
        yield return new WaitForSeconds(0.15f);
        text.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        text.color = Color.white;
        yield return new WaitForSeconds(0.15f);
        text.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        text.color = Color.white;
        yield return new WaitForSeconds(0.15f);
        text.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        text.color = Color.white;
    }
}
