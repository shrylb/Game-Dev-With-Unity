using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerX : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI finalScoreText;
    public GameObject titleScreen;
    public Button restartButton;
    public bool isGameActive;
    public int countdownTime = 60;
    public GhoulSpawner ghoulSpawner; 

    private int score;
    private float timeLeft;

    // Start the game, remove title screen, reset score, and adjust spawnRate based on difficulty button clicked

    public void StartGame(int difficulty)
    {
        timeLeft = countdownTime;
        isGameActive = true;
        score = 0;
        UpdateScore(0);
        titleScreen.SetActive(false);

        // Start the ghoul spawner
        ghoulSpawner.StartSpawning(difficulty);
    }

    private void Update()
    {
        Countdown();
    }

    void Countdown()
    {
        if (!isGameActive) return;
        else
            while (timeLeft > 0)
            {
                countdownText.text = "TIME: " + Mathf.FloorToInt(timeLeft);
                timeLeft -= Time.deltaTime;
                return;
            }

        GameOver();
    }

    // Update score with value from target clicked
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "SCORE: " + score;
    }

    // Stop game, bring up game over text and restart button
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        finalScoreText.text = "FINAL SCORE: " + score ;
        finalScoreText.gameObject.SetActive(true);

        isGameActive = false;
    }

    // Restart game by reloading the scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
