using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    Paddle paddle;
    public Rigidbody2D ball;
    private Rigidbody2D rb;

    public float moveSpeed = 10f;

    void Start()
    {
        // Get a reference to the Paddle component attached to this GameObject
        paddle = GetComponent<Paddle>();

        // Get a reference to the Rigidbody2D component attached to this GameObject
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*
         * Check if the ball's x-position is greater than or equal to 0 (on the right side of the screen)
         * and the ball's velocity in the x-direction is positive (moving towards the right)
         * This means the ball is moving towards the AI player's side of the screen
         */
        if (ball.transform.localPosition.x >= 0 && ball.velocity.x > 0)
        {
            /*
             * Move the paddle towards the ball's y-position using Lerp (Linear Interpolation)
             * This will make the paddle smoothly follow the ball's vertical position
             * The moveSpeed variable determines the speed at which the paddle moves
             */
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, ball.transform.position.y, transform.position.z), Time.deltaTime * moveSpeed);

            /*
             * Clamp the paddle's y-position to the screen boundaries
             * This ensures the paddle doesn't go off the top or bottom of the screen
             */
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Clamp(transform.localPosition.y, -455, 455), transform.localPosition.z);
        }
    }
}