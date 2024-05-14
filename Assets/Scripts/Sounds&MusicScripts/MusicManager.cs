using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    private AudioSource audioSource;
    public AudioClip backgroundMusic;
    public AudioClip backgroundMusic2;

    public AudioClip bossBackgroundMusic;
    [SerializeField] public Slider musicSlider;

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
            PlayBackgroundMusic(false, backgroundMusic2);
        }
        Instance.musicSlider.onValueChanged.AddListener(delegate { SetVolume(musicSlider.value); });
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Handle music for different scenes
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name;
        if (sceneName == "StartScene")
        {
            PlayBackgroundMusic(false, backgroundMusic2);
        }
        else if (sceneName == "JohnTato")
        {
            PlayBackgroundMusic(false, backgroundMusic);
        }
    }

    public static void SetVolume(float volume)
    {
        Instance.audioSource.volume = volume;
    }

    public static float GetVolume()
    {
        return Instance.audioSource.volume;
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
    public static void PauseBackgroundMusic()
    {
        Instance.audioSource.Pause();
    }

    public static void ResumeBackgroundMusic()
    {
        Instance.audioSource.Play();
    }

    public static void SetBossBackgroundMusic(bool isBossLevel)
    {
        if (isBossLevel)
        {
            PlayBackgroundMusic(false, Instance.bossBackgroundMusic);
        }
        else
        {
            PlayBackgroundMusic(false, Instance.backgroundMusic);
        }
    }
}
