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

    private Button correctButton;
    private Color defaultButtonColor = Color.white; // The default color for buttons
    private Color correctButtonColor = Color.green; // The color for correct answers
    private Color incorrectButtonColor = Color.red; // The color for incorrect answers


    private int questionCounter = 0;
    private bool quizCompleted = false;
    private bool gamePaused = false; // Add variable to track game pause state
    private int correctAnswers = 0;
    private int totalQuestions = 3;
    private float rate;
    private float accuracy;
    private float startTime;
    private float endTime;
    private float totalTime;



    void Start()
    {

        correctAnswerPrompt.SetActive(false);
        wrongAnswerPrompt.SetActive(false);
        startTime = Time.time;
        StartCoroutine(DelayBeforeNextQuestion());
    }
    void InitializeGame()
    {
        // Reset all necessary game state variables here
        questionCounter = 0;
        quizCompleted = false;
        gamePaused = false;
        correctAnswers = 0;
        correctAnswerPrompt.SetActive(false);
        wrongAnswerPrompt.SetActive(false);
        pauseMenu.SetActive(false);
        startTime = Time.time;

        // Start the game loop
        StartCoroutine(DelayBeforeNextQuestion());
    }

    IEnumerator DelayBeforeNextQuestion()
    {
        yield return new WaitForSeconds(0.5f); // Adjust the delay time as needed
        // Generate the next question after the delay if the quiz is not completed and the game is not paused
        if (!quizCompleted && !gamePaused)
        {
            GenerateQuestion();
        }

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
                    correctButton = answerButtons[i];
                    answerButtons[i].onClick.AddListener(CorrectAnswer);
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
                    answerButtons[i].onClick.AddListener(WrongAnswer);
                }
            }

            if (questionCounter > totalQuestions)
            {
                quizCompleted = true;
                endTime = Time.time; // Record the end time when the quiz is completed
                totalTime = endTime - startTime; // Calculate the total time taken
                accuracy = ((float)correctAnswers / totalQuestions) * 100;
                accuracy = ((float)correctAnswers / totalQuestions) * 100;
                rate = (totalQuestions / totalTime) * 60f;
                questionText.text = $"Quiz is completed!\nYour score: {correctAnswers}/{totalQuestions}\nAccuracy: {accuracy:F2}%\nRate: {rate:F2}/min";
            }
        }
    }

    void CorrectAnswer()
    {
        Debug.Log("Correct!");
        correctAnswers++;
        HighlightButton(correctButton, correctButtonColor);
        StartCoroutine(ShowPrompt(correctAnswerPrompt));
    }

    void WrongAnswer()
    {
        Debug.Log("Wrong!");
        HighlightButton(correctButton, correctButtonColor);
        Button incorrectButton = (Button)UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        HighlightButton(incorrectButton, incorrectButtonColor);
        StartCoroutine(ShowPrompt(wrongAnswerPrompt));
    }

    void HighlightButton(Button button, Color color)
    {
        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage != null)
        {
            buttonImage.color = color;
            StartCoroutine(ResetButtonColor(buttonImage));
        }
    }

    IEnumerator ResetButtonColor(Image buttonImage)
    {
        yield return new WaitForSeconds(2.0f); // Adjust the delay time as needed
        buttonImage.color = defaultButtonColor;
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
        gamePaused = false;
        pauseMenu.SetActive(false); // Hide the pause menu panel
        Time.timeScale = 1f; // Restore normal time flow
    
        SceneManager.LoadScene(0); // Load the main menu scene
    }
    IEnumerator ShowPrompt(GameObject prompt)
    {
        prompt.SetActive(true);
        yield return new WaitForSeconds(2.0f); // Wait for 2 seconds
        prompt.SetActive(false);
        
    }
}