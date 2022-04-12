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

    [Header("Timer")]
    private int timerA, timerB;
    private bool timerGoing;
    private int min, sec;
    public TMP_Text timerTextA, timerTextB;
    [Header("Scores")]
    public int scoreA, scoreB;
    public TMP_Text scoreTextA, scoreTextB;
    [Header("Texts")]
    public TMP_Text questionText;
    public TMP_Text answerText;
    public Animator answerAnimator;

    private bool isPlayerA = true;
    public int activeIndexA = 0, activeIndexB = 0;

    [Header("Player Panels")]
    public CanvasGroup PanelA;
    public CanvasGroup PanelB;
    // Letters parent (for changing sprite)
    public Transform lettersA, lettersB;
    

    
    private void Awake() {
        // TODO: load from a JSON file!!!

        // Player A
        questionsA = new string[LTNUM] 
        {
            "PlayerA question A",
            "PlayerA question B",
            "PlayerA question C",
            "PlayerA question D",
            "PlayerA question E",
            "PlayerA question F",
            "PlayerA question G",
            "PlayerA question H",
            "PlayerA question I",
            "PlayerA question L",
            "PlayerA question M",
            "PlayerA question N",
            "PlayerA question O",
            "PlayerA question P",
            "PlayerA question Q",
            "PlayerA question R",
            "PlayerA question S",
            "PlayerA question T",
            "PlayerA question U",
            "PlayerA question V",
            "PlayerA question Z"
        };
        answersA = new string[LTNUM] 
        {
            "PlayerA answer A",
            "PlayerA answer B",
            "PlayerA answer C",
            "PlayerA answer D",
            "PlayerA answer E",
            "PlayerA answer F",
            "PlayerA answer G",
            "PlayerA answer H",
            "PlayerA answer I",
            "PlayerA answer L",
            "PlayerA answer M",
            "PlayerA answer N",
            "PlayerA answer O",
            "PlayerA answer P",
            "PlayerA answer Q",
            "PlayerA answer R",
            "PlayerA answer S",
            "PlayerA answer T",
            "PlayerA answer U",
            "PlayerA answer V",
            "PlayerA answer Z"
        };

        // Player B
        questionsB = new string[LTNUM] 
        {
            "PlayerB question A",
            "PlayerB question B",
            "PlayerB question C",
            "PlayerB question D",
            "PlayerB question E",
            "PlayerB question F",
            "PlayerB question G",
            "PlayerB question H",
            "PlayerB question I",
            "PlayerB question L",
            "PlayerB question M",
            "PlayerB question N",
            "PlayerB question O",
            "PlayerB question P",
            "PlayerB question Q",
            "PlayerB question R",
            "PlayerB question S",
            "PlayerB question T",
            "PlayerB question U",
            "PlayerB question V",
            "PlayerB question Z"
        };
        answersB = new string[LTNUM] 
        {
            "PlayerB answer A",
            "PlayerB answer B",
            "PlayerB answer C",
            "PlayerB answer D",
            "PlayerB answer E",
            "PlayerB answer F",
            "PlayerB answer G",
            "PlayerB answer H",
            "PlayerB answer I",
            "PlayerB answer L",
            "PlayerB answer M",
            "PlayerB answer N",
            "PlayerB answer O",
            "PlayerB answer P",
            "PlayerB answer Q",
            "PlayerB answer R",
            "PlayerB answer S",
            "PlayerB answer T",
            "PlayerB answer U",
            "PlayerB answer V",
            "PlayerB answer Z"
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
        Debug.Log("Answer is correct!");
        // Stop timer
        StopTimer();
        // Animate answer
        AnimateAnswer();
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


    private void AnimateAnswer()
    {
        // TODO adapt on which sprite to show
        answerAnimator.SetTrigger("Show");
    }
}
