using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartMenuController : MonoBehaviour
{
    // Reference to the start button GameObject
    public GameObject startButton;

    // Reference to the settings button GameObject
    public GameObject settingsButton;

    // Reference to the exit button GameObject
    public GameObject exitButton;

    // Reference to the volume slider GameObject
    public GameObject slider;

    // Reference to the exit settings GameObject
    public GameObject exitSettingsButton;

    // When clicking the start button load Johntato
    public void OnStartClick()
    {
        SceneManager.LoadScene("JohnTato");
    }

    public void OnClickSettings()
    {
        // Disable the start, settings, and exit buttons at the start
        startButton.SetActive(false);
        settingsButton.SetActive(false);
        exitButton.SetActive(false);
        slider.SetActive(true);
        exitSettingsButton.SetActive(true);
    }

    public void OnClickExitSettings()
    {
        // Disable the slider, and exit settings buttons that the start
        startButton.SetActive(true);
        settingsButton.SetActive(true);
        exitButton.SetActive(true);
        slider.SetActive(false);
        exitSettingsButton.SetActive(false);
    }

    // Exit the game
    public void OnExitClick()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
