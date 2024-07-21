using UnityEngine;

public class Player : MonoBehaviour
{
    // Variables to store the up and down keys to control the paddle.
    public KeyCode upKey;
    public KeyCode downKey;

    // Set the movement speed of the paddle.
    public float moveSpeed = 10f;

    //Private reference to the Paddle component attached to this GameObject.
    private Paddle paddle;

    void Start()
    {
        paddle = GetComponent<Paddle>();
    }

    void Update()
    {
        /*
         * Initialize a direction variable to store the vertical movement direction.
         * It will be set to 1 for upward movement, -1 for downward movement, and 0 for no movement.
         */
        float direction = 0f;

        /*
         * Check if the up key is being pressed.
         * If so, set the direction to 1 (upward).
         */
        if (Input.GetKey(upKey))
        {
            direction = 1f;
        }
        /*
         * Check if the down key is being pressed.
         * If so, set the direction to -1 (downward).
         */
        else if (Input.GetKey(downKey))
        {
            direction = -1f;
        }

        /*
         * Call the MoveVertically method of the Paddle component,
         * passing the calculated direction multiplied by the move speed.
         * This will move the paddle vertically based on the player's input.
         */
        paddle.MoveVertically(direction * moveSpeed);
    }
}