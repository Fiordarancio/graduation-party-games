using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// JSON utility
using System.IO;

/** 
 *  IMPORTANT NOTE: this game is built referring to 
 *                  the ITALIAN alphabet, which contains
 *                  21 letters (it has all english 
 *                  letters but KJXYZ)
 */

// A couple question-answer is the base element of the game
[System.Serializable]
public class CustomQA {
    public string question;
    public string answer;

    public CustomQA (string question, string answer)
    {
        this.question = question;
        this.answer = answer;
    }

    public void put (string question, string answer) 
    {
        this.question = question;
        this.answer = answer;
    }
};
