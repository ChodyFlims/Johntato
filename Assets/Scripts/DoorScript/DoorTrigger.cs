using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorTrigger : MonoBehaviour
{
    public int levelToLoadIndex;
    private int currentLevelIndex;
    public Vector3 playerPosition;
    private bool playerInRange; // Flag to track if the player is in range
    private GameController gameController;

    // Fetch the level properties from each of the levels, which is just the level index
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        LevelProperties levelProperties = GetComponentInParent<LevelProperties>();
        if (levelProperties != null)
        {
            currentLevelIndex = levelProperties.levelIndex;
        }
        else
        {
            Debug.LogWarning("LevelProperties not found on the parent object.");
        }
    }

    // Method to handle the interaction input
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && playerInRange && gameController != null)
        {
            gameController.KillEverybodyInTheWorld();
            // Call the method to load the specific level in the GameController
            gameController.LoadSpecificLevel(currentLevelIndex, levelToLoadIndex, playerPosition);
            gameController.DoBoth();
            SoundEffectManager.Play("PlayerWalkThroughDoor");
        }
    }

    // When the player enters the trigger area
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Set player in range to true
            playerInRange = true;
        }
    }

    // When the player exits the trigger area
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Set player in range to false
            playerInRange = false;
        }
    }
}