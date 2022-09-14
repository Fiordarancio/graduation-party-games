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


// Data we load from JSON are a list of <num_letters_in_alphabets>
// couples question-answers
[System.Serializable]
public class QAList {

    public const int LTNUM = 21;
    public CustomQA[] list;

}