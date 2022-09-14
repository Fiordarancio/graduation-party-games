using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// JSON utility
using System.IO;

public class SaveLoadSystem : MonoBehaviour
{
    public const string savePathPlayerA = "/savings/custom_qas_playerA.json";
    public const string savePathPlayerB = "/savings/custom_qas_playerB.json";

    // Utility method to save and use a predefined question-answer
    // list in case it has not been prepared in advance
    public static void SaveSession()
    {
        // Create an instance of the object we must serialize 
        QAList pA_data = LoadStandardPlayer("pA");
        
        // Serialize in JSON and save the string as a file
        string fileText = JsonUtility.ToJson(pA_data);

        // Check if the path exist (create folders if not) then write file
        string filePath = Application.persistentDataPath + savePathPlayerA;
        if (File.Exists(filePath))
            File.WriteAllText(filePath, fileText);
        else 
        {
            FileInfo file = new FileInfo(filePath);
            file.Directory.Create(); // If the directory already exists, this method does nothing.
            File.WriteAllText(file.FullName, fileText);
        }

        // Just repeat for player B
        QAList pB_data = LoadStandardPlayer("pB");
        fileText = JsonUtility.ToJson(pB_data);
        filePath = Application.persistentDataPath + savePathPlayerB;
        if (File.Exists(filePath))
            File.WriteAllText(filePath, fileText);
        else 
        {
            FileInfo file = new FileInfo(filePath);
            file.Directory.Create(); // If the directory already exists, this method does nothing.
            File.WriteAllText(file.FullName, fileText);
        }

        Debug.Log("Data saved!");
    }

    public static QAList LoadSession(string savePath, string playerName)
    {
        // Get the file from which we want to serialize and check it's existence
        string filePath = Application.persistentDataPath + savePath;
        if (File.Exists(filePath))
        {
            // Deserialize and transfer info to the manager
            string fileText = File.ReadAllText(filePath);
            QAList data = JsonUtility.FromJson<QAList>(fileText);
        
            // Assign in player
            return data;
        }
        else 
        {
            Debug.Log("No data saved at: "+savePath);
            return LoadStandardPlayer(playerName);
        }

    }

    // In case anything isn't ready, load a placeholder list of questions
    public static QAList LoadStandardPlayer(string playerName)
    {
        QAList standard = new QAList();
        standard.list = new CustomQA [QAList.LTNUM];
        for (int i = 0; i < QAList.LTNUM; i++)
            standard.list[i] = 
                new CustomQA (playerName+"_question"+(i+1), playerName+"_answer"+(i+1));
        return standard;
    }
}
