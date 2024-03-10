using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Import SceneManager to handle scene changes
using System.Collections.Generic;
using System.Collections;

public class MathGame : MonoBehaviour
{
    public Text questionText;
    public Button[] answerButtons;
    public Text questionCounterText;
    public GameObject pauseMenu; // Add reference to the pause menu panel
    public GameObject correctAnswerPrompt;
    public GameObject wrongAnswerPrompt;


    private int questionCounter = 0;
    private bool quizCompleted = false;
    private bool gamePaused = false; // Add variable to track game pause state
    private int correctAnswers = 0;
    private int totalQuestions = 3;
    private float accuracy;
    private float startTime;
    private float endTime;
    private float totalTime;

    void Start()
    {
        correctAnswerPrompt.SetActive(false);
        wrongAnswerPrompt.SetActive(false);
        startTime = Time.time;
        GenerateQuestion();
    }

    void GenerateQuestion()
    {

        if (!quizCompleted && !gamePaused) // Check if the quiz is not completed and the game is not paused
        {
            // Increment question counter
            questionCounter++;
            if (questionCounter <= totalQuestions)
            {
                questionCounterText.text = $"{questionCounter} / {totalQuestions}";
            }
            else
            {
                questionCounterText.text = ""; // Hide question counter after 3 questions
            }

            // Generate random numbers for the addition question
            int num1 = Random.Range(1, 11); // Change the range as per your requirement
            int num2 = Random.Range(1, 11);

            int answer = num1 + num2;

            // Display the question
            questionText.text = num1 + " + " + num2 + "= ?";

            // List to store wrong answers
            List<int> wrongAnswers = new List<int>();

            // Generate random answer options
            int correctButtonIndex = Random.Range(0, answerButtons.Length);

            for (int i = 0; i < answerButtons.Length; i++)
            {
                if (questionCounter <= totalQuestions)
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
                    int wrongAnswer = Random.Range(answer / 2, answer + 3); // Change the range as per your requirement
                    while (wrongAnswers.Contains(wrongAnswer) || wrongAnswer == answer)
                    {
                        wrongAnswer = Random.Range(answer / 2, answer + 3);
                    }
                    wrongAnswers.Add(wrongAnswer); // Add wrong answer to the list
                    answerButtons[i].GetComponentInChildren<Text>().text = wrongAnswer.ToString();
                    answerButtons[i].onClick.RemoveAllListeners(); // Remove previous listeners
                    answerButtons[i].onClick.AddListener(() => WrongAnswer());
                }
            }

            if (questionCounter > totalQuestions)
            {
                quizCompleted = true;
                endTime = Time.time; // Record the end time when the quiz is completed
                totalTime = endTime - startTime; // Calculate the total time taken
                accuracy = ((float)correctAnswers / totalQuestions) * 100;
                accuracy = ((float)correctAnswers / totalQuestions) * 100;
                questionText.text = $"Quiz is completed!\nYour score: {correctAnswers}/{totalQuestions}\nAccuracy: {accuracy:F2}%\nTotal time: {totalTime:F2} seconds";
            }
        }
    }

    void CorrectAnswer()
    {
        Debug.Log("Correct!");
        correctAnswers++;
        StartCoroutine(ShowPrompt(correctAnswerPrompt));
    }

    void WrongAnswer()
    {
        Debug.Log("Wrong!");
        StartCoroutine(ShowPrompt(wrongAnswerPrompt));
    }

    public void PauseGame()
    {
        gamePaused = true;
        pauseMenu.SetActive(true); // Show the pause menu panel
        Time.timeScale = 0f; // Pause the game
    }

    public void ResumeGame()
    {
        gamePaused = false;
        pauseMenu.SetActive(false); // Hide the pause menu panel
        Time.timeScale = 1f; // Resume the game
    }

    public void RestartGame()
    {
        questionCounter = 0; // Reset question counter
        quizCompleted = false; // Reset quiz completion status
        gamePaused = false; // Reset game pause status
        pauseMenu.SetActive(false); // Hide the pause menu panel
        Time.timeScale = 1f; // Resume the game
        GenerateQuestion(); // Start generating questions again
    }

    public void Quit()
    {
        SceneManager.LoadScene("main"); // Load the main menu scene
    }
    IEnumerator ShowPrompt(GameObject prompt)
    {
        prompt.SetActive(true);
        yield return new WaitForSeconds(2); // Wait for 2 seconds
        prompt.SetActive(false);
        GenerateQuestion(); // Continue to the next question or end the quiz
    }
}