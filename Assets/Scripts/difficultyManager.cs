using UnityEngine;
using System;
using System.Collections;

public class DifficultyManager : MonoBehaviour
{
    [Serializable]
    public struct DifficultyPair
    {
        public int threshold;
        public float newSpeed;
    }
    private LevelController controller;
    public float startSpeed;
    public DifficultyPair[] difficulty;

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("LevelScroller").GetComponent<LevelController>();
        if (difficulty!=null)
        {
            StartCoroutine(CheckDifficulty());
        }
    }

    private IEnumerator CheckDifficulty()
    {
        while (true)
        {
            foreach (DifficultyPair _diff in difficulty)
            {
                if (controller.height == _diff.threshold)
                {
                    controller.scrollSpeed = _diff.newSpeed;
                }
            }
            yield return new WaitForSeconds(3f);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            controller.scrollSpeed = startSpeed;
        }
    }

}
