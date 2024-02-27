using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public ScoreManager scoreManager;
    public Text additionProblemText;
    public Button[] answerButtons;

    private int operand1;
    private int operand2;
    private int correctAnswer;

    void Start()
    {
        GenerateAdditionProblem();
    }

    void GenerateAdditionProblem()
    {
        operand1 = UnityEngine.Random.Range(1, 10);
        operand2 = UnityEngine.Random.Range(1, 10);
        correctAnswer = operand1 + operand2;

        additionProblemText.text = operand1 + " + " + operand2;

        // Shuffle the answers
        int[] answers = { correctAnswer, correctAnswer + 1, correctAnswer - 1 };
        Shuffle(answers);

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<Text>().text = answers[i].ToString();
        }
    }


    void Shuffle<T>(T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            T value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
    }

    public void CheckAnswer(Button button)
    {
        int selectedAnswer = int.Parse(button.GetComponentInChildren<Text>().text);
        if (selectedAnswer == correctAnswer)
        {
            scoreManager.AddScore();
            Debug.Log("Correct answer! Score incremented.");
        }
        else
        {
            Debug.Log("Incorrect answer.");
        }

        GenerateAdditionProblem();
    }
}
