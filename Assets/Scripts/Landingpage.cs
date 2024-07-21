using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Landingpage : MonoBehaviour
{
    public GameObject tutorialCanvas;
    public GameObject creditsCanvas;
    public GameObject mainMenuCanvas;

    public void StartGame()
    {
        // Load the game scene
        SceneManager.LoadScene("Game");
    }

    public void OpenTutorial()
    {
        // Show the tutorial canvas
        tutorialCanvas.SetActive(true);
    }
    

    public void OpenSettings()
    {
        // Load the settings scene
        SceneManager.LoadScene("OptionsMenu");
    }

    public void ShowCredits()
    {
        // Show the credits canvas
        creditsCanvas.SetActive(true);
    }

    public void BackToMainMenu()
    {
        // Hide the tutorial, settings, or credits canvas, show the main menu canvas
        tutorialCanvas.SetActive(false);
        creditsCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }

    public void QuitGame()
    {
        // Quit the application
        Debug.Log("Quitting the game...");
        Application.Quit();
    }
}
