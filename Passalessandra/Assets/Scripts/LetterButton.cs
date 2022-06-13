using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Behavior of a letter

// TODO LATER: should be better that each letter has its own question/answer couple?
public class LetterButton : MonoBehaviour
{
    // private UIManager uiManager; // Manager to pass reset for timers ecc.
    private UXManager uxManager;

    public Status status = Status.DEFAULT;
    public int index = 0;

    [Header("Sprites")]
    public Sprite defaultSprite;
    public Sprite correctSprite;
    public Sprite wrongSprite;
    public Sprite idleSprite;


    private void Awake() 
    {
        // Try to get the UIManager and its UXManager component
        uxManager = GameObject.Find("UIManager").GetComponent<UXManager>();
    }


    // When the letter is clicked, it is activated
    public void SelectLetter()
    {
        if (uxManager == null)
        {
            Debug.LogError("Missing UX Manager in Scene!");
            return;
        }

        // Show the question of the letter
        uxManager.ShowCurrentQuestion(index, status);

        // Show the answer, the sprite ecc, depending on letter status
        switch(status)
        {
            case Status.DEFAULT: default:
                Debug.Log("Opening new letter");
                // Show default sprite
                SetSprite(defaultSprite);
                // Start the player
                uxManager.StartPlayer();
            break;
            case Status.CORRECT:
                SetSprite(correctSprite);
                uxManager.ShowGivenAnswer(index, status);
            break;
            case Status.WRONG:
                Debug.Log("Passing wrong");
                SetSprite(wrongSprite);
                uxManager.ShowGivenAnswer(index, status);
            break;
            case Status.IDLE:
                Debug.Log("Passing idle");
                SetSprite(idleSprite);
                // Start the player
                uxManager.StartPlayer();
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
