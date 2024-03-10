using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public Text questionText;
    public Button[] answerButtons;
    public Text questionCounterText;
    public GameObject pauseMenu;
    public GameObject correctAnswerCanvas;
    public GameObject wrongAnswerCanvas;

    private int questionCounter = 0;
    private bool quizCompleted = false;
    private bool gamePaused = false;
    private int correctAnswers = 0;
    private int totalQuestions = 3;
    private float accuracy;
    private float startTime;
    private float endTime;
    private float totalTime;

    void Start()
    {
        startTime = Time.time;
        GenerateQuestion();
        correctAnswerCanvas.SetActive(false);
        wrongAnswerCanvas.SetActive(false);
    }

    void GenerateQuestion()
    {
        if (!quizCompleted && !gamePaused)
        {
            questionCounter++;
            if (questionCounter <= totalQuestions)
            {
                questionCounterText.text = $"{questionCounter} / {totalQuestions}";
                int num1 = Random.Range(1, 11);
                int num2 = Random.Range(1, 11);
                int answer = num1 + num2;

                questionText.text = $"{num1} + {num2} = ?";
                SetupAnswerButtons(answer);
            }
            else
            {
                EndQuiz();
            }
        }
    }

    void SetupAnswerButtons(int correctAnswer)
    {
        List<int> wrongAnswers = new List<int>();
        int correctButtonIndex = Random.Range(0, answerButtons.Length);
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].gameObject.SetActive(true);
            answerButtons[i].onClick.RemoveAllListeners();
            int answerValue;
            if (i == correctButtonIndex)
            {
                answerValue = correctAnswer;
                answerButtons[i].onClick.AddListener(() => CorrectAnswer());
            }
            else
            {
                do
                {
                    answerValue = Random.Range(correctAnswer - 10, correctAnswer + 10);
                } while (wrongAnswers.Contains(answerValue) || answerValue == correctAnswer);
                wrongAnswers.Add(answerValue);
                answerButtons[i].onClick.AddListener(() => WrongAnswer());
            }
            answerButtons[i].GetComponentInChildren<Text>().text = answerValue.ToString();
        }
    }

    IEnumerator ShowFeedback(bool isCorrect)
    {
        GameObject feedbackCanvas = isCorrect ? correctAnswerCanvas : wrongAnswerCanvas;
        feedbackCanvas.SetActive(true);
        yield return new WaitForSeconds(2);
        feedbackCanvas.SetActive(false);

        if (questionCounter < totalQuestions)
        {
            GenerateQuestion();
        }
        else
        {
            EndQuiz();
        }
    }

    void CorrectAnswer()
    {
        correctAnswers++;
        StartCoroutine(ShowFeedback(true));
    }

    void WrongAnswer()
    {
        StartCoroutine(ShowFeedback(false));
    }

    public void PauseGame()
    {
        gamePaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        gamePaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void EndQuiz()
    {
        quizCompleted = true;
        endTime = Time.time;
        totalTime = endTime - startTime;
        accuracy = ((float)correctAnswers / totalQuestions) * 100;
        questionText.text = $"Quiz Completed!\nYour score: {correctAnswers}/{totalQuestions}\nAccuracy: {accuracy:F2}%\nTotal time: {totalTime:F2} seconds";
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
