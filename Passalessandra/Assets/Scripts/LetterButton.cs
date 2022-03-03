using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Behavior of a letter
public class LetterButton : MonoBehaviour
{
    private UIManager uiManager; // Manager to pass reset for timers ecc.

    public Status status = Status.DEFAULT;
    public int index = 0;

    [Header("Sprites")]
    public Sprite defaultSprite;
    public Sprite correctSprite;
    public Sprite wrongSprite;
    public Sprite idleSprite;


    private void Awake() 
    {
        // Try to get the UIManager
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }


    // When the letter is clicked, it is activated
    public void SelectLetter()
    {
        if (uiManager == null)
        {
            Debug.LogError("Missing UIManager in Scene!");
            return;
        }

        // Show the question of the letter
        uiManager.ShowActiveQuestion(index);

        // Show the answer, the sprite ecc, depending on letter status
        switch(status)
        {
            case Status.DEFAULT: default:
                Debug.Log("Start new letter");
                // Show default sprite
                SetSprite(defaultSprite);
                // Start timer
                uiManager.StartTimer();
            break;
            case Status.CORRECT:
                SetSprite(correctSprite);
            break;
            case Status.WRONG:
                Debug.Log("Pass wrong");
                SetSprite(wrongSprite);
            break;
            case Status.IDLE:
                Debug.Log("Pass idle");
                SetSprite(idleSprite);
                uiManager.StartTimer();
            break;
        }
    }

    public void SetStatus (Status newStatus)
    {
        switch(newStatus)
        {
            case Status.DEFAULT: default:
                // Show default sprite
                SetSprite(defaultSprite);
            break;
            case Status.CORRECT:
                SetSprite(correctSprite);
            break;
            case Status.WRONG:
                SetSprite(wrongSprite);
            break;
            case Status.IDLE:
                SetSprite(idleSprite);
            break;
        }
        status = newStatus;
    }

    public void SetSprite (Sprite s)
    {
        GetComponent<Button>().GetComponent<Image>().sprite = s;
    }
}
