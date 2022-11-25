using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    private float currentGameSpeed = 0f;
    public float gameSpeed { get; private set; }

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    public Button retryButton;

    // Question group
    public GameObject questionGroup;

    private Player player;
    private Spawner spawner;

    // Global score var
    public int diamondsCount { get; set; }
    private float score;

    public float gravity = 9.8f * 2f;

    private void Awake()
    {
        // check on singleton
        if (Instance == null) {
            Instance = this;
        } else {
            DestroyImmediate(gameObject);
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();

        NewGame();
    }

    

    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime;
        //scoreText.text = Mathf.FloorToInt(score).ToString("D5");
        // Diamonds
        scoreText.text = Mathf.FloorToInt(diamondsCount).ToString("D5");
    }

    public void NewGame()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach (var obstacle in obstacles) {
            Destroy(obstacle.gameObject);
        }

        gameSpeed = initialGameSpeed;
        score = 0f;
        diamondsCount = 0;
        enabled = true;

        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);

        questionGroup.gameObject.SetActive(false);

        UpdateHighscore();
    }

    public void GameOver() {
        gameSpeed = 0f;
        enabled = false;

        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);

        UpdateHighscore();
    }

    public void GamePause()
    {
        currentGameSpeed = gameSpeed;
        gameSpeed = 0f;
        enabled = false;

        // player.gameObject.SetActive(false);
        // Toggle off animation
        player.GetComponent<AnimatedSprite>().enabled = false;
        player.GetComponent<Player>().enabled = false;
        spawner.gameObject.SetActive(false);

        questionGroup.gameObject.SetActive(true);
        // Anim question
        questionGroup.GetComponent<Animator>().SetBool("isOpened", true);
    }


    public void GameResume()
    {
        gameSpeed = currentGameSpeed;
        enabled = true;

        // player.gameObject.SetActive(true);
        // Toggle on animation
        player.GetComponent<AnimatedSprite>().enabled = true;
        player.GetComponent<Player>().enabled = true;
        spawner.gameObject.SetActive(true);

        
        // Anim question
        questionGroup.GetComponent<Animator>().SetBool("isOpened", false);
        questionGroup.gameObject.SetActive(false);
    }

    private void onDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void UpdateHighscore()
    {
        float highscore = PlayerPrefs.GetFloat("highscore", 0);
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetFloat("highscore", highscore);
        }

        highscoreText.text = Mathf.FloorToInt(highscore).ToString("D5");
    }
}
