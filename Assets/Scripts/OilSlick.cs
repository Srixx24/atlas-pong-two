using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilSlick : MonoBehaviour
{
    public float speedReduction = 200f; // Amount of speed to reduce from the ball

    private void OnTriggerStay2D(Collider2D other)
    {
        // Check if the colliding object is a ball
        if (other.CompareTag("Ball"))
        {
            // Get the ball's Rigidbody2D component
            Rigidbody2D ballRigidbody = other.GetComponent<Rigidbody2D>();

            if (ballRigidbody != null)
            {
                // Get the ball's script component
                Ball ballScript = other.GetComponent<Ball>();

                if (ballScript != null)
                {
                    // Reduce the ball's speed
                    ballScript.ReduceSpeed(Time.deltaTime * speedReduction);

                    // Log a message to indicate the ball has touched the oil slick
                    Debug.Log("Ball is on the oil slick!");
                }
            }
        }
    }

    private void ReduceBallSpeed(Rigidbody2D ballRigidbody)
    {
        // Get the ball's current speed
        float currentSpeed = ballRigidbody.velocity.magnitude;

        // Reduce the speed by the specified amount
        float newSpeed = Mathf.Max(0f, currentSpeed - (Time.deltaTime * speedReduction));

        // Update the ball's velocity with the new speed
        ballRigidbody.velocity = ballRigidbody.velocity.normalized * newSpeed;
        ballRigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }
}