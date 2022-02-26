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

    // Timer
    private float timer;
    public TMP_Text timerTextA, timerTextB;
    // Texts
    public TMP_Text questionText;
    public TMP_Text answerText;
    // Letters parent (for changing sprite)
    public Transform lettersA, lettersB;

    private bool isPlayerA = true;
    public int activeIndexA = 0, activeIndexB = 0;
    
    private void Awake() {
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

    private void FixedUpdate() {
        
    }
    
    public string GetActivePlayerQuestion(int index)
    {
        return (isPlayerA)? questionsA[index] : questionsB[index];
    }
    public string GetActivePlayerAnswer(int index)
    {
        return (isPlayerA)? answersA[index] : answersB[index];
    }
    public void ShowActiveQuestion(int index)
    {
        if (isPlayerA)
        {
            questionText.text = questionsA[index];
            activeIndexA = index;
        }
    }
    public void ShowActiveAnswer(int index)
    {

    }


    // Buttons for validation
    public void AnswerCorrect()
    {
        // Stop timer
        // Animate answer
        // Set status of letter
        if (isPlayerA)
            lettersA.GetChild(activeIndexA).GetComponent<LetterButton>().SetStatus(Status.CORRECT);
        else
            lettersB.GetChild(activeIndexB).GetComponent<LetterButton>().SetStatus(Status.CORRECT);
        
        // Switch to the next active index which is default or idle
        // DO IT MANUALLY (and wait as you wish)
    }
    public void AnswerWrong()
    {
        // Set status of letter
        if (isPlayerA)
            lettersA.GetChild(activeIndexA).GetComponent<LetterButton>().SetStatus(Status.WRONG);
        else
            lettersB.GetChild(activeIndexB).GetComponent<LetterButton>().SetStatus(Status.WRONG);
    }
    public void AnswerIdle()
    {
        // Set the status and increment index
        if (isPlayerA)
            lettersA.GetChild(activeIndexA).GetComponent<LetterButton>().SetStatus(Status.IDLE);
        else
            lettersB.GetChild(activeIndexB).GetComponent<LetterButton>().SetStatus(Status.IDLE);
    }
    public void ResetAnswer()
    {
        if (isPlayerA)
            lettersA.GetChild(activeIndexA).GetComponent<LetterButton>().SetStatus(Status.IDLE);
        else
            lettersB.GetChild(activeIndexB).GetComponent<LetterButton>().SetStatus(Status.IDLE);
            
    }


}
