using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// What does a player do:
// - activate and stop timer
// - control letters (make them reset, passed, correct or wrong)
// - keep a list of questions and answers to display

public class Player : MonoBehaviour
{
    public class QA {
        public string question;
        public string answer;

        public QA (string question, string answer)
        {
            this.question = question;
            this.answer = answer;
        }
    };

    // Which is this player? (decide which QA list)
    [Range (1,2)] public int thisPlayerIndex = 1;

    // List of couples question/answers
    private const short LTNUM = 21;
    public QA[] qaCouple;
    public int currentQA = 0;
    // Letters
    public Transform letters;

    // Time
    private const int MAXTIME = 3*60*10;
    private bool playerActive = false;
    public int tenthsLeft = 0;
    public TMP_Text timerText;

    // Score
    private int score = 0;
    public TMP_Text scoreText; 

    // Canvas group to make transition
    private CanvasGroup canvasGroup;

    private void Awake() 
    {
        if (thisPlayerIndex == 1)
            LoadPlayerA();
        else 
            LoadPlayerB();

        canvasGroup = GetComponent<CanvasGroup>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        currentQA = 0; // Starting from A
        playerActive = false;
        tenthsLeft = MAXTIME;
        score = 0;
        scoreText.text = score.ToString();
        timerText.text = "3:00";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivatePlayer ()
    {
        // When a player is active, make it fully visible
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        playerActive = true;
    }
    public int PausePlayer ()
    {
        // When a player is paused, it is only partially visible
        canvasGroup.alpha = 0.2f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        playerActive = false;  
        // Return your index so that the UX Manager can easily tell 
        // which player to active next 
        return thisPlayerIndex;
    }
    public void StartPlayer()
    {
        playerActive = true;
        StopAllCoroutines();
        StartCoroutine(GoTimer());
    }

    // Coroutine going on while player is active
    IEnumerator GoTimer ()
    {
        while (playerActive && tenthsLeft > 0)
        {
            // Show time on HUD
            timerText.text = (tenthsLeft/600) +":"+ ((tenthsLeft/10)%60).ToString("D2");

            yield return new WaitForSeconds(0.1f);
            tenthsLeft--;
        }
    }


    public string GetCurrentQuestion()
    {
        return qaCouple[currentQA].question;
    }
    public string GetCurrentAnswer()
    {
        return qaCouple[currentQA].answer;
    }

    public void AnswerCorrect()
    {
        // Stop timer
        StopAllCoroutines();
        // Set status of letter
        letters.GetChild(currentQA).GetComponent<LetterButton>().SetStatus(Status.CORRECT);
        score++;
        scoreText.text = score.ToString();
        
        // Switch to the next letter manually as you wish
    }
    public void AnswerWrong()
    {
        // Stop timer
        StopAllCoroutines();
        // Set status of letter
        letters.GetChild(currentQA).GetComponent<LetterButton>().SetStatus(Status.WRONG);
    }
    public void AnswerIdle()
    {
        // Stop timer
        StopAllCoroutines();
        // Set status of letter
        letters.GetChild(currentQA).GetComponent<LetterButton>().SetStatus(Status.IDLE);
    }
    public void AnswerReset()
    {
        // Restore the current letter to default and recover some time,
        // but the timer is not stopped. Plus, reset score
        tenthsLeft += 85; // On average, we have 8.57 seconds per answer
        if (tenthsLeft > MAXTIME) tenthsLeft = MAXTIME;
        LetterButton lb = letters.GetChild(currentQA).GetComponent<LetterButton>();
        if (lb.status == Status.CORRECT)
            score--;
        lb.SetStatus(Status.DEFAULT);
    }
    public void RestoreTime(float seconds)
    {
        tenthsLeft += (int)(seconds*10); 
        if (tenthsLeft > MAXTIME) tenthsLeft = MAXTIME;
    }

    // TODO: Load from some text file when the game is installed
    void LoadPlayerA()
    {
        qaCouple = new QA[LTNUM] 
        {
            new QA ("pA_question1", "pA_answer1"),
            new QA ("pA_question2", "pA_answer2"),
            new QA ("pA_question3", "pA_answer3"),
            new QA ("pA_question4", "pA_answer4"),
            new QA ("pA_question5", "pA_answer5"),
            new QA ("pA_question6", "pA_answer6"),
            new QA ("pA_question7", "pA_answer7"),
            new QA ("pA_question8", "pA_answer8"),
            new QA ("pA_question9", "pA_answer9"),
            new QA ("pA_question10", "pA_answer10"),
            new QA ("pA_question11", "pA_answer11"),
            new QA ("pA_question12", "pA_answer12"),
            new QA ("pA_question13", "pA_answer13"),
            new QA ("pA_question14", "pA_answer14"),
            new QA ("pA_question15", "pA_answer15"),
            new QA ("pA_question16", "pA_answer16"),
            new QA ("pA_question17", "pA_answer17"),
            new QA ("pA_question18", "pA_answer18"),
            new QA ("pA_question19", "pA_answer19"),
            new QA ("pA_question20", "pA_answer20"),
            new QA ("pA_question21", "pA_answer21")
        };
    }
    void LoadPlayerB()
    {
        qaCouple = new QA[LTNUM]{
            new QA ("pB_question1", "pB_answer1"),
            new QA ("pB_question2", "pB_answer2"),
            new QA ("pB_question3", "pB_answer3"),
            new QA ("pB_question4", "pB_answer4"),
            new QA ("pB_question5", "pB_answer5"),
            new QA ("pB_question6", "pB_answer6"),
            new QA ("pB_question7", "pB_answer7"),
            new QA ("pB_question8", "pB_answer8"),
            new QA ("pB_question9", "pB_answer9"),
            new QA ("pB_question10", "pB_answer10"),
            new QA ("pB_question11", "pB_answer11"),
            new QA ("pB_question12", "pB_answer12"),
            new QA ("pB_question13", "pB_answer13"),
            new QA ("pB_question14", "pB_answer14"),
            new QA ("pB_question15", "pB_answer15"),
            new QA ("pB_question16", "pB_answer16"),
            new QA ("pB_question17", "pB_answer17"),
            new QA ("pB_question18", "pB_answer18"),
            new QA ("pB_question19", "pB_answer19"),
            new QA ("pB_question20", "pB_answer20"),
            new QA ("pB_question21", "pB_answer21")
        };
    }
}
