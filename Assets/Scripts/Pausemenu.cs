using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausemenu : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused = false;

    private void Update()
    {
        // Check for the pause input (e.g., Escape key)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        isPaused = !isPaused;

        // Show or hide the pause menu
        pauseMenu.SetActive(isPaused);

        // Pause or resume the game
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void ResumeGame()
    {
        TogglePauseMenu();
    }

    public void GoToMainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene("Main Menu");
    }

    public void OpenOptionsMenu()
    {
        SceneManager.LoadScene("OptionsMenu");
    }
}
