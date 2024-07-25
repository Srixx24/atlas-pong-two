using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBlock : MonoBehaviour
{
    public AudioClip triggerSound; // Audio clip to play when the trigger is hit
    private AudioSource audioSource;
    private float wallActiveTime = 10f; // Time the walls stay active (in seconds)

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Deactivate the walls initially
        DeactivateWalls();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("WallTrigger"))
        {
            // Activate the walls
            ActivateWalls();

            // Play the trigger sound
            PlayTriggerSound();

            // Start a coroutine to deactivate the walls after the specified duration
            StartCoroutine(DeactivateWallsAfterDelay());
            Debug.Log("Ball hit wall power up!");
        }
    }

    private void ActivateWalls()
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject wall in walls)
        {
            wall.SetActive(true);
        }
    }

    private void DeactivateWalls()
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject wall in walls)
        {
            wall.SetActive(false);
        }
    }

    private IEnumerator DeactivateWallsAfterDelay()
    {
        yield return new WaitForSeconds(wallActiveTime);
        DeactivateWalls();
    }

    private void PlayTriggerSound()
    {
        if (triggerSound != null)
        {
            audioSource.PlayOneShot(triggerSound);
        }
    }
}