using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using TMPro; 

public class DisplayLastFiveScores : MonoBehaviour
{
    public TextMeshProUGUI scoreTableText; 

    private void Start()
    {
        DisplayScores();
    }

    private void DisplayScores()
    {
        string filePath = Path.Combine(Application.dataPath, "userProgress.txt");

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
            }
        }
        else
        {
            Debug.LogWarning("File not found.");
            scoreTableText.text = "Error: File not found";
        }
    }
}
