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

    // Which is this player? (decide which QA list)
    [Range (1,2)] public int thisPlayerIndex = 1;

    // List of couples question/answers
    public QAList myQA;
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
        return myQA.list[currentQA].question;
    }
    public string GetCurrentAnswer()
    {
        return myQA.list[currentQA].answer;
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

    // Load from JSON file if existing
    void LoadPlayerA()
    {
        SaveLoadSystem.LoadSession(SaveLoadSystem.savePathPlayerA, "pA");
    }

    void LoadPlayerB()
    {
        SaveLoadSystem.LoadSession(SaveLoadSystem.savePathPlayerB, "pB");

        // First brute force initialization was the following
        // qaCouple = new QA[LTNUM]{
        //     new QA ("pB_question1", "pB_answer1"),
        //     new QA ("pB_question2", "pB_answer2"),
        //     new QA ("pB_question3", "pB_answer3"),
        //     new QA ("pB_question4", "pB_answer4"),
        //     new QA ("pB_question5", "pB_answer5"),
        //     new QA ("pB_question6", "pB_answer6"),
        //     new QA ("pB_question7", "pB_answer7"),
        //     new QA ("pB_question8", "pB_answer8"),
        //     new QA ("pB_question9", "pB_answer9"),
        //     new QA ("pB_question10", "pB_answer10"),
        //     new QA ("pB_question11", "pB_answer11"),
        //     new QA ("pB_question12", "pB_answer12"),
        //     new QA ("pB_question13", "pB_answer13"),
        //     new QA ("pB_question14", "pB_answer14"),
        //     new QA ("pB_question15", "pB_answer15"),
        //     new QA ("pB_question16", "pB_answer16"),
        //     new QA ("pB_question17", "pB_answer17"),
        //     new QA ("pB_question18", "pB_answer18"),
        //     new QA ("pB_question19", "pB_answer19"),
        //     new QA ("pB_question20", "pB_answer20"),
        //     new QA ("pB_question21", "pB_answer21")
        // };
    }
}
