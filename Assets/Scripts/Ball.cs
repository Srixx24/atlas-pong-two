using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    private Vector3 initialPosition;
    private Vector3 entryPoint = new Vector3(0f, 630f, 0f);
    private Rigidbody2D rb;
    public float speed = 500f;
    public float accelerationRate = 0.1f;
    public float maxSpeed = 5000f;
    public Vector3 velocity;
    private bool isBallActive = false;
    private AudioSource audioSource;
    public AudioClip paddleSound;
    public AudioClip goalSound;
    public AudioClip borderSound;
    private float pitchRange = 0.4f;
    public Image explosionImage;
    private float explosionDuration = .25f;
    public GameObject ImpactReaction; // Reference to the particle system prefab
    private float impactPSDuration = 1f;
    private ScreenShakeManager screenShakeManager;
    public float color1Speed = 1500f;
    public float color2Speed = 1800f;
    public Sprite[] colorSprites;
    private int currentColorIndex = 0;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //initialPosition = transform.position;

        initialPosition = transform.position;
        StartCoroutine(FallIntoBall());

        // Set the initial velocity of the ball
        velocity = new Vector3(speed, 0f, 0f);

        audioSource = GetComponent<AudioSource>();
        screenShakeManager = GetComponent<ScreenShakeManager>();

        // Load the color sprites into an array
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    IEnumerator FallIntoBall()
    {
        isBallActive = false;
        float fallDuration = 1f;
        float startTime = Time.time;

        while (Time.time - startTime < fallDuration)
        {
            float t = (Time.time - startTime) / fallDuration;
            transform.position = Vector3.Lerp(entryPoint, Vector3.zero, t);
            GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, Mathf.Lerp(630f, 0f, t));
            yield return null;
        }
    
        transform.position = Vector3.zero;
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        isBallActive = true;

        // Display the explosion effect
        if (explosionImage != null)
        {
            explosionImage.gameObject.SetActive(true);
            AddForce();
            yield return new WaitForSeconds(explosionDuration);
            explosionImage.gameObject.SetActive(false);
        }
    }

    public void AddForce()
    {
        /* The AddForce() method applies an initial force to the ball in a random 
        * direction. This ensures that the ball starts moving in a different direction 
        * each time the game is played.
        */
        float angle = Random.value < 1f ? -1f : 1f;
        float force = Random.value < 0.5f ? Random.Range(-10f, -0.8f) : Random.Range(0.8f, 10f);

        Vector2 direction = new Vector2(angle, force);
        rb.AddForce(direction * this.speed);
    }

    public void ReduceSpeed(float amount)
    {
        // Reduce the ball's speed
        speed = Mathf.Max(0f, speed - amount);
    }

    void Update()
    {
        // Maintain a constant ball speed
        rb.velocity = rb.velocity.normalized * speed;

        // Increase the ball's speed over time
        IncreaseSpeed();

        ChangeColor();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the ball collided with a paddle or a goal
        if (collision.gameObject.CompareTag("Paddle") || collision.gameObject.CompareTag("Goal") || collision.gameObject.CompareTag("Border"))
        {
            // Play the impact sound effect
            PlayImpactSound(collision);

            // Get the ball's speed at the moment of impact
            float impactSpeed = rb.velocity.magnitude;

            // Calculate the shake intensity based on the impact speed
            float shakeIntensity = Mathf.Clamp(impactSpeed / 100000f, 1f, 5f);
            float shakeDuration = Mathf.Clamp(impactSpeed / 200000f, 1f, 5f);

            // Call the ScreenShake function
            screenShakeManager.ShakeCamera(shakeIntensity, shakeDuration);
        }

        // Instantiate a new copy of the particle system at the ball's position
        GameObject particleSystemInstance = Instantiate(ImpactReaction, transform.position, Quaternion.identity);

        // Start the particle system
        var particleSystem = particleSystemInstance.GetComponent<ParticleSystem>();
        particleSystem.Play();

        // Destroy the particle system instance
        Destroy(particleSystemInstance, impactPSDuration);
    }

    private void IncreaseSpeed()
    {
        // Increase the ball's velocity
        velocity *= (1 + accelerationRate * Time.deltaTime);

        speed = velocity.magnitude;

        // Limit the ball's speed to the maximum speed
        if (speed > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
            speed = maxSpeed;
        }
    }

    private void ChangeColor()
    {
        // Change the ball's color to the first color when the speed reaches 1500
        if (speed >= color1Speed && currentColorIndex < colorSprites.Length - 1)
        {
            currentColorIndex = 1;
            spriteRenderer.sprite = colorSprites[currentColorIndex];
        }
        // Change the ball's color to the second color when the speed reaches 1800
        else if (speed >= color2Speed && currentColorIndex < colorSprites.Length - 1)
        {
            currentColorIndex = 2;
            spriteRenderer.sprite = colorSprites[currentColorIndex];
        }
    }

    private void PlayImpactSound(Collision2D collision)
    {
        // Randomize the pitch of the audio source
        audioSource.pitch = Random.Range(1.0f - pitchRange, 1.0f + pitchRange);

        if (collision.gameObject.CompareTag("Paddle"))
        {
            audioSource.clip = paddleSound;
        }
        else if (collision.gameObject.CompareTag("Goal"))
        {
            audioSource.clip = goalSound;
        }
        else if (collision.gameObject.CompareTag("Border"))
        {
            audioSource.clip = borderSound;
        }

        audioSource.Play();
    }

    public void ResetBall()
    {
        /* The ResetBall() method is responsible for resetting the ball's
        * position and velocity when the game is restarted or a point is
        * scored. It sets the ball's velocity to zero, moves the ball back to
        * its initial position, and then applies a new random force to the ball
        * using the AddForce() method.
        */
        ResetVelocityAndSpeed();
        transform.position = new Vector3(1278f, 715f, 42f);
        AddForce();
    }

    private void ResetVelocityAndSpeed()
    {
        /* The ResetVelocityAndSpeed() method resets the ball's velocity to the
        * initial velocity and then applies the IncreaseSpeed() method to
        * gradually increase the ball's speed back to the desired level.
        */
        rb.velocity = Vector2.zero;
        velocity = new Vector3(speed, 0f, 0f);
        IncreaseSpeed();
    }
}