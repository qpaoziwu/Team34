using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiTextManager : MonoBehaviour
{
    public Text playerOneScore;
    public Text playerTwoScore;
    public Text score;
    public GameObject playerOne;
    private GameObject playerTwo;
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
