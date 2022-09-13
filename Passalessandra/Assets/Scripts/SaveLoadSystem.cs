using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// JSON utility
using System.IO;

public class SaveLoadSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ITALIAN ALPHABET
    public const int LTNUM = 21;

    [System.Serializable]
    public class CustomQA {
        public string question;
        public string answer;

        public CustomQA (string question, string answer)
        {
            this.question = question;
            this.answer = answer;
        }
    }
    [System.Serializable]
    public class QAList {
        public CustomQA[] list;
    }

    private static string savePath = "/savings/custom_qas.json";

    public static void SaveSession()
    {
        // Create an instance of the object we must serialize 
        QAList data = new QAList();
        data.list = new CustomQA [LTNUM] 
        {
            new CustomQA ("pA_question1", "pA_answer1"),
            new CustomQA ("pA_question2", "pA_answer2"),
            new CustomQA ("pA_question3", "pA_answer3"),
            new CustomQA ("pA_question4", "pA_answer4"),
            new CustomQA ("pA_question5", "pA_answer5"),
            new CustomQA ("pA_question6", "pA_answer6"),
            new CustomQA ("pA_question7", "pA_answer7"),
            new CustomQA ("pA_question8", "pA_answer8"),
            new CustomQA ("pA_question9", "pA_answer9"),
            new CustomQA ("pA_question10", "pA_answer10"),
            new CustomQA ("pA_question11", "pA_answer11"),
            new CustomQA ("pA_question12", "pA_answer12"),
            new CustomQA ("pA_question13", "pA_answer13"),
            new CustomQA ("pA_question14", "pA_answer14"),
            new CustomQA ("pA_question15", "pA_answer15"),
            new CustomQA ("pA_question16", "pA_answer16"),
            new CustomQA ("pA_question17", "pA_answer17"),
            new CustomQA ("pA_question18", "pA_answer18"),
            new CustomQA ("pA_question19", "pA_answer19"),
            new CustomQA ("pA_question20", "pA_answer20"),
            new CustomQA ("pA_question21", "pA_answer21")
        };

        // Serialize in JSON and save the string as a file
        string fileText = JsonUtility.ToJson(data);
        // Check if the path exist (create folders if not) then write file
        File.WriteAllText(Application.persistentDataPath + savePath, fileText);

        Debug.Log("Data saved!");
    }

    public static void LoadSession()
    {
        // Get the file from which we want to serialize and check it's existence
        string filePath = Application.persistentDataPath + savePath;
        if (File.Exists(filePath))
        {
            // Deserialize and transfer info to the manager
            string fileText = File.ReadAllText(filePath);
            QAList data = JsonUtility.FromJson<QAList>(fileText);

            Debug.Log("Data retrieved: ");
            foreach (CustomQA element in data.list)
            {
                Debug.Log(element.question + ", " + element.answer);
            }
        
            // Assign in player
        }

    }
}
