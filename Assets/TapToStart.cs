using UnityEngine;
using UnityEngine.SceneManagement;

public class TapToStart : MonoBehaviour
{
    private bool gameStarted = false;

    void Update()
    {
        // Check for tap or click input
        if (Input.GetMouseButtonDown(0) && !gameStarted)
        {
            // The screen is tapped, start the game
            StartGame();
        }
    }

    void StartGame()
    {
        // Set a flag to prevent multiple taps
        gameStarted = true;

        // Load the next scene (replace "GameScene" with the name of your actual game scene)
        SceneManager.LoadScene("game");
    }
}
