using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public const int WinningScore = 11;
    public GameObject EndGameAnnouncement;
    public TextMeshProUGUI playerOneScoreText;
    public TextMeshProUGUI playerTwoScoreText;
    public TextMeshProUGUI FinalScore;
    public ScoreKeeper scoreKeeper;
    private AudioSource audioSource;
    public AudioClip endgameSound;


    private void Start()
    {
        scoreKeeper = GetComponent<ScoreKeeper>();

        scoreKeeper.playerOneScore = playerOneScoreText;
        scoreKeeper.playerTwoScore = playerTwoScoreText;

        //scoreKeeper.onScoreChanged.AddListener(OnScoreChanged);

        audioSource = GetComponent<AudioSource>();
    }

    public void Update()
    {
        // Check if the ball has entered the goal area
        CheckGoal();
    }

    private void CheckGoal()
    {
        // Use the OnTriggerEnter2D event in the ScoreKeeper class to detect goals
        Collider2D goalCollider = Physics2D.OverlapCircle(scoreKeeper.ball.transform.position, 0.5f, LayerMask.GetMask("Goal"));
        if (goalCollider != null)
        {
            // If a goal is detected, update the scores accordingly
            if (goalCollider.transform.position.x < 0)
            {
                // Goal scored by player two
                scoreKeeper.playerTwoScore.text = (int.Parse(scoreKeeper.playerTwoScore.text) + 1).ToString();
            }
            else
            {
                // Goal scored by player one
                scoreKeeper.playerOneScore.text = (int.Parse(scoreKeeper.playerOneScore.text) + 1).ToString();
            }

            // Check the winning condition after a goal is scored
            CheckWinningCondition(int.Parse(scoreKeeper.playerOneScore.text), int.Parse(scoreKeeper.playerTwoScore.text));
        }
    }

    // Checks for 11 points by either players
    private void CheckWinningCondition(int playerOneScore, int playerTwoScore)
    {
        if (playerOneScore >= WinningScore)
        {
            EndGame("Player One", "Player Two");
        }
        else if (playerTwoScore >= WinningScore)
        {
            EndGame("Player Two", "Player One");
        }
    }

    /*
     * EndGame handles the end-game logic, including freezing the game time, playing the end-game sound,
     * activating the end-game canvas, and displaying the final scores in the canvas
     */
    private void EndGame(string winner, string loser)
    {
        // Freeze the game
        Time.timeScale = 0f;

        // Play the endgame sound
        PlayEndgameSound();

        // Show the end game canvas
        EndGameAnnouncement.SetActive(true);

        // Display the final scores
        playerOneScoreText.text = playerOneScoreText.text;
        playerTwoScoreText.text = playerTwoScoreText.text;
        FinalScore.text = $"{winner} is the Victor!\n{loser} has been defeated!";
    }

    private void PlayEndgameSound()
    {
        audioSource.clip = endgameSound;
        audioSource.Play();
    }
}
