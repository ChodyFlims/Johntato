using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    // Reference to the resume button GameObject
    public GameObject resumeButton;

    // Reference to the settings button GameObject
    public GameObject settingsButton;

    // Reference to the exit button GameObject
    public GameObject exitButton;

    // Reference to the volume slider GameObject
    public GameObject slider;

    // Reference to the exit settings GameObject
    public GameObject exitSettingsButton;


    private bool isPaused = false;

    void Update()
    {
        // Check for input to toggle pause
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        // Freeze time
        Time.timeScale = 0f;
        
        // Activate the pause menu GameObject if it's assigned
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
        }
        MusicManager.PauseBackgroundMusic();

        isPaused = true;
    }

    public void ResumeGame()
    {
        // Unfreeze time
        Time.timeScale = 1f;
        
        // Deactivate the pause menu GameObject if it's assigned
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
        MusicManager.ResumeBackgroundMusic();
        isPaused = false;
    }

    public void OnClickSettings()
    {
        // Disable the start, settings, and exit buttons at the start
        resumeButton.SetActive(false);
        settingsButton.SetActive(false);
        exitButton.SetActive(false);
        slider.SetActive(true);
        exitSettingsButton.SetActive(true);
    }

    public void OnClickExitSettings()
    {
        // Disable the slider, and exit settings buttons that the start
        resumeButton.SetActive(true);
        settingsButton.SetActive(true);
        exitButton.SetActive(true);
        slider.SetActive(false);
        exitSettingsButton.SetActive(false);
    }
}
