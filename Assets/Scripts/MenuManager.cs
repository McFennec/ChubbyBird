using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    
    [Header("Sprites des chiffres (0-9)")]
    [SerializeField] private Sprite[] numberSprites;

    [Header("Images pour chaque panel")]
    [SerializeField] private Image[] highScoreDigitsMenu;

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
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateScoreDisplay(highScoreDigitsMenu, highScore);
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

    public void OnPlayPress()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.clickSound);
        SceneManager.LoadScene("GameScene");
    }

    public void OnExitPress()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.clickSound);
        Application.Quit();
    }
}
