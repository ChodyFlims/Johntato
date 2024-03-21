using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoubleJumpUI : MonoBehaviour
{
    public Image UIDoubleJumpPrefab;
    public Sprite fullDoubleJump;
    public Sprite emptyDoubleJump;
    private List<Image> jumps = new List<Image>();


    // Method to set the double jum,p UI based on availablility
    public void SetDoubleJump(bool hasDoubleJump)
    {
        // Clear existing jumps
        foreach (Image jump in jumps)
        {
            Destroy(jump.gameObject);
        }
        jumps.Clear();

        // Create new jump UI based on double jump availability
        Image newJump = Instantiate(UIDoubleJumpPrefab, transform);
        newJump.sprite = hasDoubleJump ? fullDoubleJump : emptyDoubleJump;
        jumps.Add(newJump);
    }
}
