using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject crate;
    public Text countdownText;
    public float countdownDuration;
    float startTime;

    public float gameOverDelay;

    // Use this for initialization
    void Start() {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update() {

        float timeElapsed = Time.time - startTime;
        // if the time has not yet elapsed, then update the UI text
        if (timeElapsed < countdownDuration) {
            countdownText.text = Mathf.Ceil(countdownDuration - timeElapsed).ToString();
        } else {
            // if time is up, tell the crate that the timer has finished
            countdownText.text = "";
            crate.SendMessage("OnTimerFinished");
            // disable this script
            enabled = false;
        }
    }

    void OnPlayerDied() {
        // upon receiving a message that the player has died, invoke the game over screen after a delay
        Invoke("GameOver", gameOverDelay);
    }


    void GameOver() {
        // load the game over scene
        SceneManager.LoadScene("GameOver");
    }
}
