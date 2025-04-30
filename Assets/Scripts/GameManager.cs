using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private float difficultyIncreaseRate = 0.1f;
    [SerializeField] private float currentSpeed = 1f;

    private int score = 0;

    private const string HighScoreKey = "HighScore"; // nom de la clé de sauvegarde

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(HighScoreKey, 0); // 0 par défaut si pas encore sauvegardé
    }

    public void SetHighScore(int newScore)
    {
        PlayerPrefs.SetInt(HighScoreKey, newScore);
        PlayerPrefs.Save(); 
    }

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.PlayMusic(AudioManager.Instance.musicClip);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0f)
        {
            currentSpeed += difficultyIncreaseRate * Time.deltaTime;
        }
    }

    // Call this method to pause the game
    public void PauseGame()
    {
        AudioManager.Instance.PauseMusic();
        Time.timeScale = 0f;
    }

    // Call this method to resume the game
    public void ResumeGame()
    {
        AudioManager.Instance.ResumeMusic();
        Time.timeScale = 1f;
    }

    // Call this method to get the value of the current difficulty
    // This value is used to determine the speed of the player and the enemies
    public float GetCurrentDifficulty()
    {
        // return Mathf.Floor(currentSpeed);
        return currentSpeed;
    }

    // Call this method to increase the score by 1
    // This method also checks if the score is greater than the high score
    // If it is, it updates the high score
    public void IncreaseScore()
    {
        int highScore = GetHighScore();
        score += 1;
        if (score > highScore)
        {
            SetHighScore(score);
            highScore = score;
        }
        UiManager.Instance.UpdateScore(score, highScore);
    }   

    // Call this method to get the current score
    public int GetScore()
    {
        Debug.Log("Current Score: " + score);
        return score;
    }

    // Call this method when the game is over
    // This method pauses the game and shows the game over screen
    public void GameOver()
    {
        AudioManager.Instance.StopMusic();
        PauseGame();
        UiManager.Instance.OnGameOver();
    }

    // Call this method to reset the game
    // This method is called when the player presses the restart button
    public void RestartGame()
    {
        AudioManager.Instance.PlayMusic(AudioManager.Instance.musicClip);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

