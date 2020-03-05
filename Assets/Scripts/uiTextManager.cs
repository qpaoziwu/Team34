using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiTextManager : MonoBehaviour
{
    public Text playerOneScore;
    public Text playerTwoScore;
    public Text playerOneLife;
    public Text playerTwoLife;
    public Text score;
    public GameObject playerOne;
    public GameObject playerTwo;
    public int scoreAmmount;
    void Update()
    {
        scoreText();
        if (playerOne!=null)
        {
            playerOneScore.text = "x  " + playerOne.GetComponent<InputMovement>().collectedItems;
            playerOneLife.text = "Lives:  " + playerOne.GetComponent<PlayerBehaviour>().lifes;
        }

        if (playerTwo != null)
        {
            playerTwoScore.text = "x  " + playerTwo.GetComponent<InputMovement>().collectedItems;

            playerTwoLife.text = "Lives:  " + playerTwo.GetComponent<PlayerBehaviour>().lifes;
        }
        //playerOneLives.text = "Player  one  lives:  " + playerOne.GetComponent<PlayerBehaviour>().lives;
    }

    private void scoreText()
    {
        scoreAmmount = playerOne.GetComponent<InputMovement>().collectedItems + playerTwo.GetComponent<InputMovement>().collectedItems * 1000;
        score.text = " SCORE" + scoreAmmount;
    }
}
