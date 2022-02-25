using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private const int LTNUM = 21;
    [Header("List of questions")]
    [SerializeField] string [] questionsA;
    [SerializeField] string [] questionsB;
    [Header("List of answers")]
    [SerializeField] string [] answersA;
    [SerializeField] string [] answersB;

    private bool isPlayerA = true;
    
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

    public string GetActivePlayerQuestion(int index)
    {
        return (isPlayerA)? questionsA[index] : questionsB[index];
    }
    public string GetActivePlayerAnswer(int index)
    {
        return (isPlayerA)? answersA[index] : answersB[index];
    }
}
