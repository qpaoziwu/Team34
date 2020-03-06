using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeleteMe : MonoBehaviour
{

public GameObject playerOne;
public GameObject playerTwo;

public int playerOnecollectedItems;
public int playerTwocollectedItems;
public int levelScore;
public int scoreAmmount;

void Update()
{
    scoreText();

    if (playerOne != null)
    {
        playerOnecollectedItems = playerOne.GetComponent<InputMovement>().collectedItems;
    }

    if (playerTwo != null)
    {
        playerTwocollectedItems = playerTwo.GetComponent<InputMovement>().collectedItems;
    }

}

private void scoreText()
{
        levelScore = (playerOnecollectedItems + playerTwocollectedItems);
        if (levelScore>=3)
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }

}
}
