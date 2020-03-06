using TMPro;
using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

[Serializable]
public struct Score
{
    public string initials;
    public int score;
}

public class HighScores : MonoBehaviour
{
    public int score;
    public int winningPlayer;
    public Score[] scores;

    public TextMeshProUGUI newHighScoretext;
    public TextMeshProUGUI playerInitials;
    public TextMeshProUGUI topArrow;
    public TextMeshProUGUI bottomArrow;
    public TextMeshProUGUI message;
    public TextMeshProUGUI[] initials;
    public TextMeshProUGUI[] highScores;
    public int gameSceneBuildIndex;

    private Animator animator;

    private int initialColumn;
    private int newHighScoreIndex;
    private bool inputIsEnabled;
    private string[] alphabet;
    private int[] initial;
    private string[] lettersOfInitial;

    private void Start()
    {
        initial = new int[3];
        lettersOfInitial = new string[3];
        alphabet = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", " " };
        winningPlayer = PlayerPrefs.GetInt("WinningPlayer");
        score = PlayerPrefs.GetInt("Score");
        animator = GetComponent<Animator>();
        GetHighScores();
        UpdateTable();
        CheckScore(score);
    }

    private IEnumerator GetInput()
    {
        while (inputIsEnabled)
        {
            switch (winningPlayer)
            {
                case 0:
                    if (Input.GetAxisRaw("p1_h") > 0)
                    {
                        IncreaseColumn();
                    }
                    else if (Input.GetAxisRaw("p1_h") < 0)
                    {
                        DecreaseColumn();
                    }

                    if (Input.GetAxisRaw("p1_v") > 0)
                    {
                        IncreaseLetter(initialColumn);
                    }
                    else if (Input.GetAxisRaw("p1_v") < 0)
                    {
                        DecreaseLetter(initialColumn);
                    }

                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        InsertNewHighScore(newHighScoreIndex, playerInitials.text, score);
                    }
                    break;

                case 1:
                    if (Input.GetAxisRaw("p2_v") > 0)
                    {
                        IncreaseColumn();
                    }
                    else if (Input.GetAxisRaw("p2_v") < 0)
                    {
                        DecreaseColumn();
                    }

                    if (Input.GetAxisRaw("p2_v") > 0)
                    {
                        IncreaseLetter(initialColumn);
                    }
                    else if (Input.GetAxisRaw("p2_v") < 0)
                    {
                        DecreaseLetter(initialColumn);
                    }
                    if (Input.GetKeyDown(KeyCode.LeftControl))
                    {
                        InsertNewHighScore(newHighScoreIndex, playerInitials.text, score);
                    }
                    break;
            }
            UpdateInitial();
            yield return new WaitForSeconds(.1f);
        }
        while (!inputIsEnabled)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(gameSceneBuildIndex);
            }
            yield return new WaitForSeconds(.1f);
        }
    }

    private void DecreaseLetter(int _column)
    {
        initial[_column] = Mathf.Clamp(initial[_column] - 1, 0, 26);
    }

    private void IncreaseLetter(int _column)
    {
        initial[_column] = Mathf.Clamp(initial[_column] + 1, 0, 26);
    }

    private void DecreaseColumn()
    {
        initialColumn = Mathf.Clamp(initialColumn - 1, 0, 2);
        UpdateColumnPointer();
    }

    private void IncreaseColumn()
    {
        initialColumn = Mathf.Clamp(initialColumn + 1, 0, 2);
        UpdateColumnPointer();
    }

    private void UpdateColumnPointer()
    {
        switch (initialColumn)
        {
            case 0:
                topArrow.alignment = TextAlignmentOptions.Left;
                bottomArrow.alignment = TextAlignmentOptions.Left;
                break;
            case 1:
                topArrow.alignment = TextAlignmentOptions.Center;
                bottomArrow.alignment = TextAlignmentOptions.Center;
                break;
            case 2:
                topArrow.alignment = TextAlignmentOptions.Right;
                bottomArrow.alignment = TextAlignmentOptions.Right;
                break;
            default:
                break;
        }
    }

    private void UpdateInitial()
    {
        string _combinedInitial = "";
        for (int i = 0; i < initial.Length; i++)
        {
            _combinedInitial += alphabet[initial[i]];
        }
        playerInitials.text = _combinedInitial;
        newHighScoretext.text = score.ToString();
        //Debug.Log(playerInitials.text);
    }

    private void UpdateTable()
    {
        for (int i = 0; i < scores.Length; i++)
        {
            initials[i].text = scores[i].initials;
            highScores[i].text = scores[i].score.ToString();
        }
    }

    private void GetHighScores()
    {
        for (int i = 0; i < scores.Length; i++)
        {
            scores[i].initials = PlayerPrefs.GetString(i.ToString(), "AAA");
            scores[i].score = PlayerPrefs.GetInt(i.ToString());
        }
    }

    private void CheckScore(int _score)
    {
        bool newHighScore = false;
        for (int i = 0; i < scores.Length; i++)
        {
            if (_score > scores[i].score)
            {
                newHighScore = true;
                newHighScoreIndex = i;
                inputIsEnabled = true;
                message.text = "High Score #" + (i + 1);
                break;
            }
        }
        if (!newHighScore)
        {
            RemoveInput();
        }
        else
        {
            StartCoroutine(GetInput());
        }
    }

    private void RemoveInput()
    {
        animator.SetTrigger("NoInput");
        inputIsEnabled = false;
    }

    private void InsertNewHighScore(int _index, string _initial, int _score)
    {
        for (int i = scores.Length; i < _index; i++)
        {
            if (i - 1 >= 0)
            {
                scores[i].initials = scores[i - 1].initials;
                scores[i].score = scores[i - 1].score;
            }
        }
        scores[_index].initials = _initial;
        scores[_index].score = _score;
        UpdateTable();
        SaveScoreTable();
        RemoveInput();
    }

    public void SaveScoreTable()
    {
        for (int i = 0; i < scores.Length; i++)
        {
            PlayerPrefs.SetString(i.ToString(), scores[i].initials);
            PlayerPrefs.SetInt(i.ToString(), scores[i].score);
        }
    }
}
