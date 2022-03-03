using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Just arrange the letter in a symmetrical way whenever the game starts.
 * Assumes the letters we want have been already placed on the scene.
 */
public class ArrangeLetters : MonoBehaviour
{
    public Transform lettersParent;
    public float radius = 125f;


    void Start()
    {
        Debug.Log("Player has "+lettersParent.childCount+" letters to guess");
        // Step angle for each point
        float step = 2*Mathf.PI/lettersParent.childCount;
        float angle;

        // A is first at pi/2, and we proceed clockwise
        for (int i = 0; i < lettersParent.childCount; i++)
        {
            RectTransform letter = lettersParent.GetChild(i).GetComponent<RectTransform>(); 
            angle = 2.5f*Mathf.PI - (step * i); // (2*Mathf.PI - (step * i)) + Mathf.PI/2 = 5/2 PI - step*i
            Vector2 letterPosition =  
                new Vector2(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle));
            // Debug.Log("Letter "+ (char)((char)'A' + i) + " at: ");
            letter.anchoredPosition = letterPosition;
        }
    }
}
