using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilSlick : MonoBehaviour
{
    private Ball ball;

    private void Start()
    {
        // Get the reference to the Ball script component
        ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Check if the colliding object is a ball
        if (other.CompareTag("Ball"))
        {
            // Get the ball's Rigidbody2D component
            Rigidbody2D ballRigidbody = other.GetComponent<Rigidbody2D>();

            if (ballRigidbody != null)
            {
                // Reduce the ball's speed
                ball.DecreaseSpeed();
                Debug.Log("Ball is in turbo mode!");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ball = collision.GetComponent<Ball>();
        if (ball != null)
        {
            ball.StopDecreasing();
        }
    }
}