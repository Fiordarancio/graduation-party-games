using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// The UI Manager controls the behavior of buttons and tells the player
// to switch between each other. Moreover, it updates the texts on the hud
public class UXManager : MonoBehaviour
{

    [Header("Players data")]
    public Player playerA;
    public Player playerB;
    private Player activePlayer = null;

    [Header("Heads-Up Display")]
    public TMP_Text questionText;
    public TMP_Text answerText;

    [Header("Answer animation")]
    private Image questionImage;
    private Image answerImage; 
    public Sprite questionPanelNormalSprite;
    public Sprite questionPanelCorrectSprite;
    public Sprite questionPanelIdleSprite;
    public Sprite questionPanelWrongSprite;
    public Animator answerPanelAnimator;
    public Sprite answerPanelCorrectSprite;
    public Sprite answerPanelWrongSprite;
    bool isAnswerShown = false;
    bool isInteractable = true; // false while waiting for switch

    private void Awake() 
    {
        questionImage = GameObject.Find("Question Panel").GetComponent<Image>();   
        answerImage = GameObject.Find("Answer Panel").GetComponent<Image>();

        SaveLoadSystem.SaveSession();
    }
    // Start is called before the first frame update
    void Start()
    {
        SwitchPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        // Control letter progression from keyboard here or
        // it will be fired once for every player in scene.
        // Avoided Arrows and WASD because Unity already uses
        // them to change button selection (TODO can be disabled?)
        // Moreover we use keys to control validation buttons
        if (isInteractable)
        {
            if (Input.GetKeyDown(KeyCode.Z)) // left
                SelectPreviousLetter();
            if (Input.GetKeyDown(KeyCode.C)) // right
                SelectNextLetter();
            
            if (Input.GetKeyDown(KeyCode.F))
                AnswerCorrect();
            if (Input.GetKeyDown(KeyCode.G))
                AnswerIdle();
            if (Input.GetKeyDown(KeyCode.H))
                AnswerWrong();
            if (Input.GetKeyDown(KeyCode.N))
                AnswerReset();
            if (Input.GetKeyDown(KeyCode.M))
                RecoverTime(2.00f);
        }
    }


    private void SwitchPlayer()
    {
        // If active player is not yet set, activate playerA and pause playerB
        if (!activePlayer)
        {
            playerA.ActivatePlayer();
            playerB.PausePlayer();
            activePlayer = playerA;
        }
        else 
        {
            // If there is already an active player, pause the active one ...
            int playerIndex = activePlayer.PausePlayer();
            // ... and activate the other 
            activePlayer = (playerIndex == 1)? playerB : playerA;
            activePlayer.ActivatePlayer();
        }

        // Clear the question panel while waiting for next letter to show
        questionText.text = "";
        answerText.text = "";
        if (isAnswerShown) 
            ResetAnswerPanel();

        isInteractable = true;
        // Debug.Log("Active player: "+ activePlayer.thisPlayerIndex);
        // Remember that the timer starts by manually selecting the next letter to show
    }
    public void StartPlayer()
    {
        activePlayer.StartPlayer();
    }


    public void ShowCurrentQuestion(int index, Status status)
    {
        activePlayer.currentQA = index;
        questionText.text = activePlayer.GetCurrentQuestion();
        
        if (status == Status.CORRECT)
            questionImage.sprite = questionPanelCorrectSprite;
        else if (status == Status.WRONG)
            questionImage.sprite = questionPanelWrongSprite;
        else
            questionImage.sprite = questionPanelNormalSprite;

        if (isAnswerShown)
            ResetAnswerPanel();
    }
    public void ShowAnimatedAnswer(Status status)
    {
        AnimateAnswerPanel(status);
    }
    private void ShowGivenAnswer(int index, Status status)
    {
        activePlayer.currentQA = index;
        answerText.text = activePlayer.GetCurrentAnswer();

        switch (status)
        {
            case Status.CORRECT:
                questionImage.sprite = questionPanelCorrectSprite;
                answerImage.sprite = answerPanelCorrectSprite;
                break;
            case Status.IDLE:
                questionImage.sprite = questionPanelIdleSprite;
                break;
            case Status.WRONG:
                questionImage.sprite = questionPanelWrongSprite;
                answerImage.sprite = answerPanelWrongSprite;
                break;
        }
    }
    private void SelectNextLetter()
    { 
        // Starting from the current index, scan the letters and find the 
        // first with state default or idle
        int next = activePlayer.currentQA;
        int ltnum = activePlayer.letters.childCount;
        for (int i = (activePlayer.currentQA+1)%ltnum; i != activePlayer.currentQA; i = (i+1)%ltnum)
        {
            Status status_i = activePlayer.letters.GetChild(i).GetComponent<LetterButton>().status;
            if (status_i == Status.IDLE || status_i == Status.DEFAULT)
            {
                // Debug.Log("Question "+i+ " not answered yet.");
                next = i;
                break;
            }
        }
        // If we did not find unanswered letters we have returned to activePlayer.currentQA, 
        // which could be also be the only one left. Else, let's "click" the letter
        if (next == activePlayer.currentQA)
            Debug.Log("This is the last active letter or the game is over");
        else
        {
            activePlayer.currentQA = next;
            activePlayer.letters.GetChild(next).GetComponent<Button>().onClick.Invoke();
            activePlayer.letters.GetChild(next).GetComponent<Button>().Select();
        }
    }
    private void SelectPreviousLetter()
    {  
        int previous = activePlayer.currentQA;
        int ltnum = activePlayer.letters.childCount;
        for (int i = ((activePlayer.currentQA-1+ltnum)%ltnum); i != activePlayer.currentQA; i = ((i-1+ltnum)%ltnum))
        {
            Status status_i = activePlayer.letters.GetChild(i).GetComponent<LetterButton>().status;
            if (status_i == Status.IDLE || status_i == Status.DEFAULT)
            {
                // Debug.Log("Question "+i+ " not answered yet.");
                previous = i;
                break;
            }
        }
        if (previous == activePlayer.currentQA)
            Debug.Log("This is the last active letter or the game is over");
        else
        {
            activePlayer.currentQA = previous;
            activePlayer.letters.GetChild(previous).GetComponent<Button>().onClick.Invoke();
            activePlayer.letters.GetChild(previous).GetComponent<Button>().Select();
        }
    }
    
    public void AnswerCorrect()
    {
        // Make the player register answer correct
        activePlayer.AnswerCorrect();
        // Animate answer
        AnimateAnswerPanel(Status.CORRECT);   
    }
    public void AnswerWrong()
    {
        // Block other input
        isInteractable = false;
        // Make the player register answer wrong
        activePlayer.AnswerWrong();
        // Animate answer
        AnimateAnswerPanel(Status.WRONG);
        // Wait a bit before passing control
        StartCoroutine(DelayPlayerSwitch(3.0f));
    }
    public void AnswerIdle()
    {
        // Block other input
        isInteractable = false;
        // Make the player register idle question
        activePlayer.AnswerIdle();
        // Wait a bit before passing control
        StartCoroutine(DelayPlayerSwitch(2.0f));
    }
    IEnumerator DelayPlayerSwitch(float delay)
    {
        yield return new WaitForSeconds(delay);
        // Switch control to the other player
        SwitchPlayer();
    }
    public void AnswerReset()
    {
        // Helper function to reset a letter
        activePlayer.AnswerReset();
        // Reset question and animation if it was issued
        questionImage.sprite = questionPanelNormalSprite;
        if (isAnswerShown)
            ResetAnswerPanel();
    }
    public void RecoverTime(float seconds)
    {
        // Helper function to restore some time to the timer
        activePlayer.RestoreTime(seconds);
    }
    

    private void AnimateAnswerPanel(Status status)
    {
        // Reset hide trigger in case it had been called previously
        answerPanelAnimator.ResetTrigger("Hide");
        answerPanelAnimator.SetTrigger("Show");
        ShowGivenAnswer(activePlayer.currentQA, status);
        StartCoroutine(DelayShowAnswer());

        isAnswerShown = true;
    }
    private void ResetAnswerPanel()
    {
        StopCoroutine(DelayShowAnswer());
        answerPanelAnimator.ResetTrigger("Show");
        answerPanelAnimator.SetTrigger("Hide");
        answerText.gameObject.SetActive(false);

        isAnswerShown = false;
    }
    IEnumerator DelayShowAnswer()
    {
        yield return new WaitForSeconds(0.5f);
        answerText.gameObject.SetActive(true);
    }
}
