using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    // Game objects representing the start and finish positions
    public GameObject startPoint;
    public GameObject endPoint;

    // How fast the platform will move
    public float speed = 0.5f;

    // Percentage of completion (from 0.0 to 1.0) from start to finish
    private float trackPercent = 0;

    // Current movement direction
    private int direction = 1;

    // Start is called before the first frame update
    private void Start()
    {
        // Ensure start and finish points are set
        if (startPoint == null || endPoint == null)
        {
            Debug.LogError("Start or end point is not set!");
            return;
        }

        // Set platform's starting position to startPoint
        transform.position = startPoint.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (startPoint == null || endPoint == null)
            return;

        trackPercent += direction * speed * Time.deltaTime;
        float x = Mathf.Lerp(startPoint.transform.position.x, endPoint.transform.position.x, trackPercent);
        float y = Mathf.Lerp(startPoint.transform.position.y, endPoint.transform.position.y, trackPercent);
        transform.position = new Vector3(x, y, transform.position.z);

        // Reverse direction at start/finish positions
        if ((direction == 1 && trackPercent > 0.9f) || (direction == -1 && trackPercent < 0.1f))
        {
            direction *= -1;
        }
    }

    // Player sticks to plaform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = transform;
        }
    }

    // Player can leave the platform
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }

}
