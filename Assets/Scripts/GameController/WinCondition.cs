using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    // Setting up an effect when the player wins
    public static event Action OnPlayedWin;

    // Check for player entering the trigger area
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayedWin.Invoke();
        }
    }

}
