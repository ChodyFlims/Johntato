using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> levels;
    public int currentLevelIndex = 0;

    private int score = 0;
    // Reference to the score label
    [SerializeField] TMP_Text scoreText;

    public GameObject gameOverScreen;
    public TMP_Text FinalScore;

    public static event Action OnReset;

    public GameObject WinObject;
    public GameObject winScreen;

    public TMP_Text WinnerText;

    private EnemySpawnpoint[] enemySpawnpoints;

    private ItemSpawner[] itemSpawners;

    public PolygonCollider2D polygonCollider;
    public PolygonCollider2D bossCollider;
    private Vector2[] originalPoints; // Stores the original points of the collider

    public Checkpoint checkpointObject;

    private bool checkpointReached = false;
    private bool youWin = false;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        GEM.OnGemCollect += IncreaseScore;
        PlayerHealth.OnPlayedDied += GameOverScreen;
        gameOverScreen.SetActive(false);
        WinCondition.OnPlayedWin += WinScreen;
        winScreen.SetActive(false);
        DoBoth();
    }

    void Update()
    {
        if (checkpointObject.checkpoint == true)
        {
            checkpointReached = true;
        }
        else
        {
            checkpointReached = false;
        }
    }

    // Display win screen
    void WinScreen()
    {
        youWin = true;
        winScreen.SetActive(true);
        WinnerText.text = "Final Score: " + score;
        MusicManager.PauseBackgroundMusic();
        // Hide score text
        scoreText.gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    // Display the game over screen
    void GameOverScreen()
    {
        gameOverScreen.SetActive(true);
        FinalScore.text = "Final Score: " + score;
        MusicManager.PauseBackgroundMusic();
        // Hide score text
        scoreText.gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    // Exit the game
    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    // Reset the game to its initial state
    public void ResetGame()
    {
        if (checkpointReached == true && youWin == false)
        {
            ResetGameCheckPoint();
        }
        else if (youWin == true || youWin == false)
        {
            // Deactivate all levels except the first one
            for (int i = 1; i < levels.Count; i++)
            {
                levels[i].SetActive(false);
            }
            MusicManager.SetBossBackgroundMusic(false);
            MusicManager.PlayBackgroundMusic(true);
            
            gameOverScreen.SetActive(false);
            winScreen.SetActive(false);
            score = 0;
            IncreaseScore(0);
            scoreText.gameObject.SetActive(true);

            KillEverybodyInTheWorld();
            Loadlevel(1);
            PlayerAssign();
            DoBoth();

            OnReset.Invoke();
            Time.timeScale = 1;
            youWin = false;
        }
    }

    // Reset the game at the checkpoint
    public void ResetGameCheckPoint()
    {
        // Deactivate all levels except the fourth one
        for (int i = 1; i < levels.Count; i++)
        {
            if (i != 4)
            {
                levels[i].SetActive(false);
            }
        }
        MusicManager.SetBossBackgroundMusic(false);
        MusicManager.PlayBackgroundMusic(true);
        
        gameOverScreen.SetActive(false);
        winScreen.SetActive(false);
        score = 0;
        IncreaseScore(0);
        scoreText.gameObject.SetActive(true);

        KillEverybodyInTheWorld();
        LoadlevelCheckpoint(4);
        PlayerAssign();
        DoBoth();

        OnReset.Invoke();
        Time.timeScale = 1;
    }

    // Reset the game components
    public void DoBoth()
    {
        FindAndAssignEverything();
        ReloadEverything();
    }

    // Assign player attributes
    void PlayerAssign()
    {
        PlayerShooting playerShooting = player.GetComponent<PlayerShooting>();
        playerShooting.shotsFired = 6;
        playerShooting.ammoUI.UpdateBullets(6);
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.maxJumps = 1;
        playerMovement.DoubleJumpActive(false);
    }

    // Find and assign enemy spawnpoints and item spawners
    public void FindAndAssignEverything()
    {
        // Find all GameObjects with the tag "EnemySpawnPoint" in the scene
        GameObject[] spawnpointObjects = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");

        // Initialize the array to hold EnemySpawnpoint scripts
        enemySpawnpoints = new EnemySpawnpoint[spawnpointObjects.Length];

        // Assign EnemySpawnpoint scripts from GameObjects
        for (int i = 0; i < spawnpointObjects.Length; i++)
        {
            enemySpawnpoints[i] = spawnpointObjects[i].GetComponent<EnemySpawnpoint>();
        }
        
        GameObject[] itemObjects = GameObject.FindGameObjectsWithTag("ItemSpawner");
        
        itemSpawners = new ItemSpawner[itemObjects.Length];
        
        for(int i = 0; i < itemObjects.Length; i++)
        {
            itemSpawners[i] = itemObjects[i].GetComponent<ItemSpawner>();
        }
    }

    // Function to reload everything
    public void ReloadEverything()
    {
        // Loop through each ItemSpawner and call the SpawnPrefab function
        foreach (ItemSpawner spawner in itemSpawners)
        {
            spawner.SpawnPrefab();
        }
        // Loop through each EnemySpawnpoint and call OnRestart function
        foreach (EnemySpawnpoint spawnpoint in enemySpawnpoints)
        {
            spawnpoint.OnRestart();
        }
    }

    // Function to destroy everything
    public void KillEverybodyInTheWorld()
    {
        // Find all GameObjects with the tag "Item"
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        // Find all GameObjects with the tag "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss");

        // Destroy each item
        foreach (GameObject item in items)
        {
            Destroy(item);
        }
        // Destroy each enemy
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        // Destroy each boss
        foreach (GameObject boss in bosses)
        {
            Destroy(boss);
        }
    }

    // Load a specific level by index
    public void Loadlevel(int level)
    {
        levels[currentLevelIndex].gameObject.SetActive(false);
        levels[level].gameObject.SetActive(true);

        player.transform.position = new Vector3(0,0,0);
        currentLevelIndex = level;
    }

    // Load level from checkpoint
    public void LoadlevelCheckpoint(int level)
    {
        levels[currentLevelIndex].gameObject.SetActive(false);
        levels[level].gameObject.SetActive(true);

        float xPos = 4.41f;
        float yPos = 27.95f;
        float zPos = 0f;

        player.transform.position = new Vector3(xPos,yPos,zPos);
        currentLevelIndex = level;
    }

    // Loads a specific level with a specific player position
    public void LoadSpecificLevel(int currentLevelIndex, int nextLevelIndex, Vector3 playerPosition)
    {
        levels[currentLevelIndex].gameObject.SetActive(false);
        levels[nextLevelIndex].gameObject.SetActive(true);

        player.transform.position = playerPosition;
        currentLevelIndex = nextLevelIndex;

        if (currentLevelIndex == 5)
        {
            ChangeConfiner();
            MusicManager.SetBossBackgroundMusic(true);
        }
    }

    // Increases the score
    void IncreaseScore(int amount)
    {
        score = score + amount;
        scoreText.text = $"Score: {score}";
    }

    void ChangeConfiner()
    {
        originalPoints = polygonCollider.points;
        Vector2[] targetPoints = bossCollider.points;
        polygonCollider.points = targetPoints;
    }
}