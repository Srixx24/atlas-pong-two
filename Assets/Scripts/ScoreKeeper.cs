using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    public Ball ball;
    public TextMeshProUGUI playerOneScore;
    public TextMeshProUGUI playerTwoScore;
    private int playerOnePoints = 0;
    private int playerTwoPoints = 0;
    public int score = 0;
    public UnityEvent<int, int> onScoreChanged;


    private void Start()
    {
        UpdateScoreText(playerOnePoints, playerTwoPoints);
    }

    private void OnEnable()
    {
        onScoreChanged.AddListener(UpdateScoreText);
    }

    private void OnDisable()
    {
        onScoreChanged.RemoveListener(UpdateScoreText);
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player2"))
        {
            Debug.Log("leftGoal");
            IncreasePlayerTwoScore();
            ball.ResetBall();
        }
        else if (collision.CompareTag("Player1"))
        {
        Debug.Log("rightGoal");
        IncreasePlayerOneScore();
        ball.ResetBall();
        }
    }

    public void IncreasePlayerOneScore()
    {
        playerOnePoints++;
        UpdateScoreText(playerOnePoints, playerTwoPoints);
        Debug.Log("Player 1 scored a goal!");
    }

    public void IncreasePlayerTwoScore()
    {
        playerTwoPoints++;
        UpdateScoreText(playerOnePoints, playerTwoPoints);
        Debug.Log("Player 2 scored a goal!");
    }

    public void UpdateScoreText(int p1Score, int p2Score)
    {
        /* The UpdateScoreText() method updates the text of the playerOneScore and
         * playerTwoScore TextMeshProUGUI components with the current scores for
         * each player.
         */
        playerOneScore.text = $"{p1Score}";
        playerTwoScore.text = $"{p2Score}";
    }

    public void ResetScore()
    {
        score = 0;
        playerOnePoints = 0;
        playerTwoPoints = 0;
        UpdateScoreText(playerOnePoints, playerTwoPoints);
    }
}
