using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiTextManager : MonoBehaviour
{
    public Text playerOneScore;
    public Text playerTwoScore;
    public Text playerOneLives;
    public Text playerTwoLives;
    public Text score;
    public GameObject playerOne;
    private GameObject playerTwo;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (playerOne!=null)
        {
            playerOneScore.text = "x  " + playerOne.GetComponent<InputMovement>().collectedItems;
        }

        if (playerTwo != null)
        {
            playerTwoScore.text = "x  " + playerTwo.GetComponent<InputMovement>().collectedItems;
        }
        //playerOneLives.text = "Player  one  lives:  " + playerOne.GetComponent<PlayerBehaviour>().lives;
    }
}
