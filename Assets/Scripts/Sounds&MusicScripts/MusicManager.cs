using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager Instance;
    private AudioSource audioSource;
    public AudioClip backgroundMusic;

    // Awake is called when the script instance is being loaded
    // It ensures that only one instance of the MusicManager exists
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    // Play background music if provided
    void Start()
    {
        if(backgroundMusic != null)
        {
            PlayBackgroundMusic(false, backgroundMusic);
        }
    }

    // Method to play background music
    public static void PlayBackgroundMusic(bool resetSong, AudioClip audioClip = null)
    {
        if(audioClip != null)
        {
            Instance.audioSource.clip = audioClip;
        }
        
        if(Instance.audioSource.clip != null)
        {
            if (resetSong)
            {
                Instance.audioSource.Stop();
            }
            Instance.audioSource.Play();
        }
    }

    // Pauses the background music
    public static void PauseBackgorundMusic()
    {
        Instance.audioSource.Pause();
    }
}
