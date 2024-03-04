using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
public class MathGame : MonoBehaviour
{
    public ScoreManager scoreManager;
    public Text additionProblemText;
    public Text questionText;
    public Button[] answerButtons;
    public Text questionCounterText;

    private int operand1;
    private int operand2;
    private int correctAnswer;
    private int questionCounter = 0;
    private bool quizCompleted = false;

    void Start()
    {
        GenerateAdditionProblem();
        GenerateQuestion();
    }

    void GenerateAdditionProblem()
    void GenerateQuestion()
    {
        operand1 = UnityEngine.Random.Range(1, 10);
        operand2 = UnityEngine.Random.Range(1, 10);
        correctAnswer = operand1 + operand2;
        if (!quizCompleted)
        {
            // Increment question counter
            questionCounter++;
            if (questionCounter <= 3)
            {
                questionCounterText.text = "" + questionCounter + " / 3";
            }
            else
            {
                questionCounterText.text = ""; // Hide question counter after 3 questions
            }

            additionProblemText.text = operand1 + " + " + operand2;
            // Generate random numbers for the addition question
            int num1 = Random.Range(1, 11); // Change the range as per your requirement
            int num2 = Random.Range(1, 11);

            // Shuffle the answers
            int[] answers = { correctAnswer, correctAnswer + 1, correctAnswer - 1 };
            Shuffle(answers);
            int answer = num1 + num2;

            for (int i = 0; i < answerButtons.Length; i++)
            {
                answerButtons[i].GetComponentInChildren<Text>().text = answers[i].ToString();
            }
        }
        // Display the question
        questionText.text = "" + num1 + " + " + num2 + " ?";

        // Generate random answer options
        int correctButtonIndex = Random.Range(0, answerButtons.Length);

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
                for (int i = 0; i < answerButtons.Length; i++)
                {
                    if (questionCounter <= 3)
                    {
                        answerButtons[i].gameObject.SetActive(true); // Show answer buttons for first 3 questions
                    }
                    else
                    {
                        answerButtons[i].gameObject.SetActive(false); // Hide answer buttons after 3 questions
                    }

                    if (i == correctButtonIndex)
                    {
                        answerButtons[i].GetComponentInChildren<Text>().text = answer.ToString();
                        answerButtons[i].onClick.RemoveAllListeners(); // Remove previous listeners
                        answerButtons[i].onClick.AddListener(() => CorrectAnswer());
                    }
                    else
                    {
                        int wrongAnswer = Random.Range(answer + 1, answer + 5); // Change the range as per your requirement
                        while (wrongAnswer == answer)
                        {
                            wrongAnswer = Random.Range(answer + 1, answer + 5);
                        }
                        answerButtons[i].GetComponentInChildren<Text>().text = wrongAnswer.ToString();
                        answerButtons[i].onClick.RemoveAllListeners(); // Remove previous listeners
                        answerButtons[i].onClick.AddListener(() => WrongAnswer());
                    }
                }

                if (questionCounter > 3)
                {
                    quizCompleted = true;
                    questionText.text = "Quiz is completed!";
                }
            }
        }

        public void CheckAnswer(Button button)
    void CorrectAnswer()
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
            Debug.Log("Correct!");
            GenerateQuestion();
        }

        GenerateAdditionProblem();
        void WrongAnswer()
        {
            Debug.Log("Wrong!");
            GenerateQuestion();
        }
    }