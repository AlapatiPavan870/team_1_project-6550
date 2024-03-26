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

            var matchingData = lines
                .Where(line => line.Split(',')[0] == deviceID)
                .Select(line => line.Split(','))
                .Reverse() // Reverse to get the last entries first
                .Take(5) // Take only the last 5 entries
                .Reverse() // Reverse again to display them in the original order
                .ToList();

            if (matchingData.Any())
            {
                foreach (var record in matchingData)
                {
                    scoreTableText.text += $"{record[2]} | {record[3]}% | {record[4]}/min\n";
                }
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
