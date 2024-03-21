using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is to collect items
public class Collector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IItem item = collision.GetComponent<IItem>();
        if(item != null)
        {
            item.Collect();
        }
    }
}
