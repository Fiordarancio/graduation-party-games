using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Behavior of a letter
public class LetterButton : MonoBehaviour
{
    public UIManager uIManager; // Manager to pass reset for timers ecc.
    public TMP_Text questionText;
    public TMP_Text answerText;
    // public Panel QuestionPanel; // Modify the panel?
    public enum Status
    {
        DEFAULT, CORRECT, INCORRECT, SKIPPED
    };
    private Status status = Status.DEFAULT;


    // When the letter is clicked, it is activated
    public void SelectLetter(int index)
    {
        // Show the question of the letter
        questionText.text = uIManager.GetActivePlayerQuestion(index);
        // Show the answer, the sprite ecc, depending on letter status
        switch(status)
        {
            case Status.DEFAULT: default:
                // Show default sprite
            break;
            case Status.CORRECT:
            break;
            case Status.INCORRECT:
            break;
            case Status.SKIPPED:
            break;
        }
    }
}
