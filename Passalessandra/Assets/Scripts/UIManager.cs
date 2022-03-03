using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private const int LTNUM = 21;
    [Header("List of questions")]
    [SerializeField] string [] questionsA;
    [SerializeField] string [] questionsB;
    [Header("List of answers")]
    [SerializeField] string [] answersA;
    [SerializeField] string [] answersB;

    [Header("Game elements")]
    // Timer
    private int timerA, timerB;
    private bool timerGoing;
    private int min, sec;
    public TMP_Text timerTextA, timerTextB;
    // Scores
    public int scoreA, scoreB;
    public TMP_Text scoreTextA, scoreTextB;
    // Texts
    public TMP_Text questionText;
    public TMP_Text answerText;
    // Letters parent (for changing sprite)
    public Transform lettersA, lettersB;

    private bool isPlayerA = true;
    public int activeIndexA = 0, activeIndexB = 0;

    [Header("Player Panels")]
    public CanvasGroup PanelA;
    public CanvasGroup PanelB;
    

    
    private void Awake() {
        // TODO: load from a JSON file!!!

        // Player A
        questionsA = new string[LTNUM] 
        {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "Z"
        };
        answersA = new string[LTNUM] 
        {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "Z"
        };

        // Player B
        questionsB = new string[LTNUM] 
        {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "Z"
        };
        answersB = new string[LTNUM] 
        {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "Z"
        };
    
    }

    private void Start() {
        timerA = timerB = 3*60;
        scoreA = scoreB = 0;
        timerGoing = false;
        isPlayerA = false;
        SwitchPlayer();
    }

    private void FixedUpdate() {
        if (timerGoing)
        {
            if (isPlayerA)
            {
                timerA -= (int)Time.deltaTime;
                
                min = timerA/60;
                sec = timerA%60;
                timerTextA.text = min + ":" +sec;

                if (timerA <= 0.0f)
                {
                    Debug.Log("PlayerB wins, PlayerA loses");
                    EndGame();
                }
            }
            else
            {
                timerA -= (int)Time.deltaTime;
                
                min = timerA/60;
                sec = timerA%60;
                timerTextA.text = min + ":" +sec;

                if (timerB <= 0.0f)
                {
                    Debug.Log("PlayerA wins, PlayerB loses");
                    EndGame();
                }
            }
        }
    }

    private void EndGame()
    {
        Debug.Log("Game Over");
        StopTimer();
    }

    public void StartTimer()
    {
        timerGoing = true;
    }
    public void StopTimer()
    {
        timerGoing = false;
    }

    public void ShowActiveQuestion(int index)
    {
        if (isPlayerA)
        {
            questionText.text = questionsA[index];
            activeIndexA = index;
        }
        else
        {
            questionText.text = questionsB[index];
            activeIndexB = index;
        }
    }
    public void ShowActiveAnswer(int index)
    {

    }


    // Buttons for validation
    public void AnswerCorrect()
    {
        // Stop timer
        StopTimer();
        // Animate answer
        // Set status of letter
        if (isPlayerA)
        {
            lettersA.GetChild(activeIndexA).GetComponent<LetterButton>().SetStatus(Status.CORRECT);
            scoreA++;
            scoreTextA.text = scoreA.ToString();
        }
        else
        {
            lettersB.GetChild(activeIndexB).GetComponent<LetterButton>().SetStatus(Status.CORRECT);
            scoreB++;
            scoreTextB.text = scoreB.ToString();
        }
        
        // Switch to the next active index which is default or idle
        // DO IT MANUALLY (and wait as you wish)
    }
    public void AnswerWrong()
    {
        StopTimer();
     
        if (isPlayerA)
            lettersA.GetChild(activeIndexA).GetComponent<LetterButton>().SetStatus(Status.WRONG);
        else
            lettersB.GetChild(activeIndexB).GetComponent<LetterButton>().SetStatus(Status.WRONG);

        SwitchPlayer();
    }

    public void AnswerIdle()
    {
        StopTimer();
        
        if (isPlayerA)
            lettersA.GetChild(activeIndexA).GetComponent<LetterButton>().SetStatus(Status.IDLE);
        else
            lettersB.GetChild(activeIndexB).GetComponent<LetterButton>().SetStatus(Status.IDLE);
        
        SwitchPlayer();
    }

    public void ResetAnswer()
    {
        if (isPlayerA)
            lettersA.GetChild(activeIndexA).GetComponent<LetterButton>().SetStatus(Status.DEFAULT);
        else
            lettersB.GetChild(activeIndexB).GetComponent<LetterButton>().SetStatus(Status.DEFAULT);   
    }

    private void SwitchPlayer()
    {
        // TODO: wait a sec before switching
        
        Status s;
        // For each player, check pending letters
        bool hasPendingLettersA = false;
        for (int i = 0; i<lettersA.childCount; i++)
        {
            s = lettersA.GetChild(i).GetComponent<LetterButton>().status; 
            if (s == Status.IDLE || s == Status.DEFAULT)
            {
                hasPendingLettersA = true;
                break;
            }
        }
        bool hasPendingLettersB = false;
        for (int i = 0; i<lettersB.childCount; i++)
        {
            s = lettersB.GetChild(i).GetComponent<LetterButton>().status; 
            if (s == Status.IDLE || s == Status.DEFAULT)
            {
                hasPendingLettersB = true;
                break;
            }
        }

        if (isPlayerA)
        {
            // If the other has pending letters, pass control
            if (hasPendingLettersB)
            {
                // Change alpha to current and other
                PanelA.alpha = 0.2f;
                PanelA.interactable = false;
                PanelA.blocksRaycasts = false;

                PanelB.alpha = 1.0f;
                PanelB.interactable = true;
                PanelB.blocksRaycasts = true;
                // Show the last active letter of the opponent
                ShowActiveQuestion(activeIndexB);
                // Pass control
                isPlayerA = false;
            }
            // If the other player is done, continue
        }
        else
        {
            // If the other has pending letters, pass control
            if (hasPendingLettersA)
            {
                // Change alpha to current and the other
                PanelB.alpha = 0.2f;
                PanelB.interactable = false;
                PanelB.blocksRaycasts = false;

                PanelA.alpha = 1.0f;
                PanelA.interactable = true;
                PanelA.blocksRaycasts = true;
                // Show the last active letter of the opponent
                ShowActiveQuestion(activeIndexA);
                // Pass control
                isPlayerA = true;
            }
            // If the other player is done, continue
        }

        // Game is done if both are done
        if (!hasPendingLettersA && !hasPendingLettersB)
        {
            Debug.Log("Game finished");
            EndGame();
        }
    }


}
