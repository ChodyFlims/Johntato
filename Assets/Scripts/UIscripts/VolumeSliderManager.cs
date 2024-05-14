using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderManager : MonoBehaviour
{
    [SerializeField] private Slider musicSlider; // Reference to the slider in the Inspector
    private MusicManager musicManager; // Reference to the music manager script

    void Start()
    {
        musicManager = MusicManager.Instance;
        if (musicManager != null)
        {
            AttachSliderToMusicManager();
        }
        else
        {
            Debug.LogWarning("Music Manager instance not found!");
        }
    }

    void AttachSliderToMusicManager()
    {
        // Set the music slider reference to the found slider component
        musicManager.musicSlider = musicSlider;

        // Set up the slider properties (you may need to adjust this based on your setup)
        musicSlider.minValue = 0f;
        musicSlider.maxValue = 0.05f;
        musicSlider.value = MusicManager.GetVolume(); // Use the class name to access static method
        musicSlider.onValueChanged.AddListener(volume => MusicManager.SetVolume(volume)); // Use the class name to access static method
    }
}
