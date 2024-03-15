using System;
using System.Collections.Generic;
using System.IO;
<<<<<<< Updated upstream
using UnityEngine;
using UnityEngine.UI; // If you're using the regular UI
using TMPro; // If you're using TextMeshPro

public class DisplayLastFiveScores : MonoBehaviour
{
    public TextMeshProUGUI scoreTableText; // Reference to the TextMeshProUGUI component
=======
using System.Linq;
using UnityEngine;
using TMPro; 

public class DisplayLastFiveScores : MonoBehaviour
{
    public TextMeshProUGUI scoreTableText; 
>>>>>>> Stashed changes

    private void Start()
    {
        DisplayScores();
    }

    private void DisplayScores()
    {
        string filePath = Path.Combine(Application.dataPath, "userProgress.txt");

<<<<<<< Updated upstream
        // Check if the file exists
        if (File.Exists(filePath))
        {
            string[] allLines = File.ReadAllLines(filePath);
            int startLine = Mathf.Max(allLines.Length - 5, 0); // Get the starting index for the last 5 lines

            // Clear existing text
            scoreTableText.text = "";

            // Create a header for the "table"
            scoreTableText.text += "TotalQ,CorrectA,Accuracy,Rate\n";

            // Loop through the last 5 lines and add them to the text component
            for (int i = startLine; i < allLines.Length; i++)
            {
                scoreTableText.text += $"{allLines[i]}\n"; // Append each line as a new row
=======
        if (File.Exists(filePath))
        {
            string deviceID = SystemInfo.deviceUniqueIdentifier;
            var lines = File.ReadAllLines(filePath);

            var matchingData = lines.AsEnumerable() // Convert to IEnumerable for LINQ
                                    .Where(line => line.Split(',')[0] == deviceID)
                                    .Select(line => line.Split(','))
                                    .ToList(); 

            if (matchingData.Count > 0)
            {
                int totalQuestions = matchingData.Sum(record => int.Parse(record[1]));
                int totalCorrectAnswers = matchingData.Sum(record => int.Parse(record[2]));
                float totalAccuracy = (totalCorrectAnswers / (float)totalQuestions) * 100.0f;
                float totalRate = matchingData.Sum(record => float.Parse(record[4]));
                float averageRate = totalRate / matchingData.Count;

                scoreTableText.text = $@"Total Questions: {totalQuestions}
Correct Answers: {totalCorrectAnswers}
Accuracy: {totalAccuracy:F2}%
Average Rate: {averageRate:F2}/min";
            }
            else
            {
                Debug.LogWarning("No records found for the device ID in 'userProgress.txt'.");
                scoreTableText.text = "Error: No data found";
>>>>>>> Stashed changes
            }
        }
        else
        {
            Debug.LogWarning("File not found.");
<<<<<<< Updated upstream
        }
    }
}
=======
            scoreTableText.text = "Error: File not found";
        }
    }
}
>>>>>>> Stashed changes
