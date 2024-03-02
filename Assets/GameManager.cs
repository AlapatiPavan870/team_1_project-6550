using UnityEngine;
using UnityEngine.UI;

public class MathGame : MonoBehaviour
{
    public Text questionText;
    public Button[] answerButtons;
    public Text questionCounterText;

    private int questionCounter = 0;
    private bool quizCompleted = false;

    void Start()
    {
        GenerateQuestion();
    }

    void GenerateQuestion()
    {
        if (!quizCompleted)
        {
            // Increment question counter
            questionCounter++;
            if (questionCounter <= 3)
            {
                questionCounterText.text = "" + questionCounter +" / 3";
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
            questionText.text = "" + num1 + " + " + num2 + " ?";

            // Generate random answer options
            int correctButtonIndex = Random.Range(0, answerButtons.Length);

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

    void CorrectAnswer()
    {
        Debug.Log("Correct!");
        GenerateQuestion();
    }

    void WrongAnswer()
    {
        Debug.Log("Wrong!");
        GenerateQuestion();
    }
}
