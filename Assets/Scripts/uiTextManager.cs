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
    public LevelController Level;
    public int playerOnecollectedItems;
    public int playerTwocollectedItems;
    public int levelScore;
    public int scoreAmmount;
    void Update()
    {
        scoreText();

        if (playerOne!=null)
        {
            playerOneScore.text = "x  " + playerOne.GetComponent<InputMovement>().collectedItems;
            playerOneLife.text = "Lives:  " + playerOne.GetComponent<PlayerBehaviour>().lifes;
            playerOnecollectedItems = playerOne.GetComponent<InputMovement>().collectedItems;
        }

        if (playerTwo != null)
        {
            playerTwoScore.text = "x  " + playerTwo.GetComponent<InputMovement>().collectedItems;
            playerTwoLife.text = "Lives:  " + playerTwo.GetComponent<PlayerBehaviour>().lifes;
            playerTwocollectedItems = playerTwo.GetComponent<InputMovement>().collectedItems;

        }
        if (Level != null)
        {
            levelScore = Level.height; 
        }
        //playerOneLives.text = "Player  one  lives:  " + playerOne.GetComponent<PlayerBehaviour>().lives;
    }

    private void scoreText()
    {
        scoreAmmount = (playerOnecollectedItems + playerTwocollectedItems) * 100 + levelScore*10;
        score.text = " SCORE: " + scoreAmmount;
    }
}
