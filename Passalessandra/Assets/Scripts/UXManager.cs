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
    public Sprite questionPanelWrongSprite;
    public Animator answerPanelAnimator;
    public Sprite answerPanelCorrectSprite;
    public Sprite answerPanelWrongSprite;

    private void Awake() 
    {
        questionImage = GameObject.Find("Question Panel").GetComponent<Image>();   
        answerImage = GameObject.Find("Answer Panel").GetComponent<Image>();
    }
    // Start is called before the first frame update
    void Start()
    {
        SwitchPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
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

        Debug.Log("Active player: "+ activePlayer.thisPlayerIndex);
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

        ResetAnswerPanel();
    }
    public void ShowGivenAnswer(int index, Status status)
    {
        activePlayer.currentQA = index;
        answerText.text = activePlayer.GetCurrentAnswer();

        if (status == Status.CORRECT)
            answerImage.sprite = answerPanelCorrectSprite;
        else
            answerImage.sprite = answerPanelWrongSprite;
    }

    public void AnswerCorrect()
    {
        Debug.Log("Player"+activePlayer.thisPlayerIndex+" answers correclty!");
        
        // Make the player register answer correct
        activePlayer.AnswerCorrect();
        // Animate answer
        AnimateAnswerPanel(Status.CORRECT);   
    }
    public void AnswerWrong()
    {
        Debug.Log("Player"+activePlayer.thisPlayerIndex+" got mistaken...");

        // Make the player register answer wrong
        activePlayer.AnswerWrong();
        // Animate answer
        AnimateAnswerPanel(Status.WRONG);
        // Wait a bit before passing control
        StartCoroutine(DelayPlayerSwitch());
    }
    public void AnswerIdle()
    {
        Debug.Log("Player"+activePlayer.thisPlayerIndex+" passed the turn.");

        // Make the player register idle question
        activePlayer.AnswerIdle();
        // Wait a bit before passing control
    }
    IEnumerator DelayPlayerSwitch()
    {
        yield return new WaitForSeconds(5.00f);
        // Switch control to the other player
        SwitchPlayer();
    }
    

    private void AnimateAnswerPanel(Status status)
    {
        Debug.Log("Show answer");

        // Reset hide trigger in case it had been called previously
        answerPanelAnimator.ResetTrigger("Hide");
        answerPanelAnimator.SetTrigger("Show");
        ShowGivenAnswer(activePlayer.currentQA, status);
        StartCoroutine(DelayShowAnswer());
    }
    private void ResetAnswerPanel()
    {
        Debug.Log("Hide answer");

        StopCoroutine(DelayShowAnswer());
        answerPanelAnimator.SetTrigger("Hide");
        answerText.gameObject.SetActive(false);
    }
    IEnumerator DelayShowAnswer()
    {
        yield return new WaitForSeconds(0.5f);
        answerText.gameObject.SetActive(true);
    }
}
