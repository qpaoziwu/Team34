using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitileManager : MonoBehaviour
{
    private bool readyToGo;
    
    public GameObject[] collectables;

    public GameObject playerOne;
    public GameObject playerTwo;

    private bool spawnedPlayerOne;
    private bool spawnedPlayerTwo;

    GameObject[] players;
    // Start is called before the first frame update
    void Start()
    {
        spawnedPlayerOne = false;
        spawnedPlayerTwo = false;

        collectables = GameObject.FindGameObjectsWithTag("COL");
        players = GameObject.FindGameObjectsWithTag("Player");
        //for(int i = 0; i < collectables.Length; i++)
        //{
        //    collectables[i].GetComponent<Collectible>().startingPosition = collectables[i].transform.position;
        //}
        readyToGo = false;

    }

    // Update is called once per frame
    void Update()
    {
        startGame();
        restartPlayer();
        if (Input.GetKeyDown(KeyCode.Alpha1) && spawnedPlayerOne == false)
        {
            Instantiate(playerOne, new Vector3(-2, -3,0), playerOne.transform.rotation);
            playerOne.GetComponent<Rigidbody2D>().velocity
                = (Vector2.up * playerOne.gameObject.GetComponent<InputMovement>().jumpVelocity * 2 + new Vector2(Input.GetAxisRaw("Horizontal") * 0.5f, 0f) * Time.deltaTime);
            playerOne.gameObject.GetComponent<PlayerBehaviour>().isLosingLife = true;
            spawnedPlayerOne = true;
        }


        if (Input.GetKeyDown(KeyCode.Alpha2) && spawnedPlayerTwo == false)
        {
            Instantiate(playerTwo, new Vector3(2, -3, 0), playerTwo.transform.rotation);
            playerTwo.GetComponent<Rigidbody2D>().velocity
                = (Vector2.up * playerTwo.gameObject.GetComponent<InputMovement>().jumpVelocity * 2 + new Vector2(Input.GetAxisRaw("Horizontal") * 0.5f, 0f) * Time.deltaTime);
            playerTwo.gameObject.GetComponent<PlayerBehaviour>().isLosingLife = true;
            spawnedPlayerTwo = true;
        }
    }

    private void resetCollectables()
    {
        for(int i = 0; i < collectables.Length; i++)
        {
            if(collectables[i].transform.position.y < -5.2f)
            {
                collectables[i].transform.position = collectables[i].GetComponent<Collectible>().startingPosition;
            }
        }
    }

    public void restartPlayer()
    {

        players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length > 0)
        {
            for (int i = 0; i < players.Length; i++)
            {
                if(players[i].transform.position.y < -5.2)
                {
                    players[i].transform.position = players[i].GetComponent<PlayerBehaviour>().startingPosition;
                }
            }
        }
    }

    private void startGame()
    {

        //collectables = GameObject.FindGameObjectsWithTag("COL");
        //resetCollectables();
        if(collectables.Length <= 0)
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
    }
}
