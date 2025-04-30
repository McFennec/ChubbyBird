using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;

    [Header("Panels")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject guiPanel;
    [SerializeField] private GameObject gameOverPanel;

    [Header("Sprites des chiffres (0-9)")]
    [SerializeField] private Sprite[] numberSprites;

    [Header("Images pour chaque panel")]
    [SerializeField] private Image[] scoreDigitsGUI;
    [SerializeField] private Image[] highScoreDigitsGUI;
    [SerializeField] private Image[] scoreDigitsPause;
    [SerializeField] private Image[] highScoreDigitsPause;
    [SerializeField] private Image[] scoreDigitsGameOver;
    [SerializeField] private Image[] highScoreDigitsGameOver;

    private int score;
    private int highScore;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        pausePanel.SetActive(false);
        guiPanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }

    public void UpdateScore(int score, int highScore)
    {
        this.score = Mathf.Clamp(score, 0, 999);
        this.highScore = Mathf.Clamp(highScore, 0, 999);

        UpdateScoreDisplay(scoreDigitsGUI, this.score);
        UpdateScoreDisplay(highScoreDigitsGUI, this.highScore);
    }

    private void UpdateScoreDisplay(Image[] digits, int value)
    {
        int hundreds = value / 100;
        int tens = (value / 10) % 10;
        int units = value % 10;

        digits[0].sprite = numberSprites[hundreds];
        digits[1].sprite = numberSprites[tens];
        digits[2].sprite = numberSprites[units];
    }

    public void OnPausePress()
    {
        UpdateScoreDisplay(scoreDigitsPause, score);
        UpdateScoreDisplay(highScoreDigitsPause, highScore);

        AudioManager.Instance.PlaySound(AudioManager.Instance.clickSound);
        GameManager.Instance.PauseGame();

        pausePanel.SetActive(true);
        guiPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void OnResumePress()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.clickSound);

        GameManager.Instance.ResumeGame();
        pausePanel.SetActive(false);
        guiPanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }

    public void OnRestartPress()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.clickSound);

        GameManager.Instance.RestartGame();
        pausePanel.SetActive(false);
        guiPanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }

    public void OnGameOver()
    {
        UpdateScoreDisplay(scoreDigitsGameOver, score);
        UpdateScoreDisplay(highScoreDigitsGameOver, highScore);

        pausePanel.SetActive(false);
        guiPanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void OnExitPress()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.clickSound);
        
        Application.Quit();
    }
}
