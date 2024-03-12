using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI; // If you're using the regular UI
using TMPro; // If you're using TextMeshPro

public class DisplayLastFiveScores : MonoBehaviour
{
    public TextMeshProUGUI scoreTableText; // Reference to the TextMeshProUGUI component

    private void Start()
    {
        DisplayScores();
    }

    private void DisplayScores()
    {
        string filePath = Path.Combine(Application.dataPath, "userProgress.txt");

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
            }
        }
        else
        {
            Debug.LogWarning("File not found.");
        }
    }
}