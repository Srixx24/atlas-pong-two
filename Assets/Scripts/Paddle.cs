using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float moveSpeed = 10f;

    private Transform paddleTransform;

    public void Start()
    {
        paddleTransform = transform;
    }

    public void MoveVertically(float direction)
    {
        // Move the paddle based on the direction
        paddleTransform.Translate(Vector3.up * direction * moveSpeed * Time.deltaTime);

        // Clamp the paddle's y-position to the screen boundaries
        transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Clamp(transform.localPosition.y, -455, 455), transform.localPosition.z);
    }
}