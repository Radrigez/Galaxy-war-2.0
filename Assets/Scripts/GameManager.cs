using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;
public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public GameObject titleScreen;
    public Button restartButton;
    public GameObject[] enemyShip;
    private MoveEnemy moveEnemy;
    public TextMeshProUGUI higtScoreText;
    private int score;
    private AudioSource souoom;
    public AudioClip boom;
    public Button playButton;

    public bool isGameActive;

    private float boardY = 10f;
    private float boardX = 7f;

    private float timeRepit = 2f;
    private float interval = 2f;
    private GameObject _player;
    public GameObject inveroment;
    public Button inveromentButton;

    
    void Start()
    {
        higtScoreText.text = PlayerPrefs.GetInt("best", 0).ToString();
        souoom = GetComponent<AudioSource>();
        _player = GameObject.Find("PlayerShip");
        moveEnemy = MoveEnemy.FindFirstObjectByType<MoveEnemy>();
        StartGame();
        YG2.StickyAdActivity(true);
    }
    public void AudioSoursBoom()
    {
        souoom.PlayOneShot(boom, 0.03f);
    }
    
    public void StartGame()
    {
         while (!isGameActive)
        {
            score = 0;
            UpdateScore(0);
            isGameActive = true;
            InvokeRepeating("SpawnEnemy", timeRepit, interval);   
            SpawnPlayer();
        }
    }
    public void MyMethod()
    {
        YG2.InterstitialAdvShow();
    }

    public void OnInveroment()
    {
        inveroment.gameObject.SetActive(!inveroment.activeSelf);
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
        if (score > PlayerPrefs.GetInt("best", 0))
        {
            PlayerPrefs.SetInt("best", score);
            higtScoreText.text = score.ToString();
        }
    }

    private void SpawnPlayer()
    { 
        _player = Resources.Load<GameObject>("PlayerShip");
        Vector2 pos = new Vector2(0,0);
        _player = Instantiate(_player, pos, Quaternion.identity);    
        
    }
    private void SpawnEnemy()
    {
        if (isGameActive)
        {
            float randomX = Random.Range(-boardX, boardX);
            float randomY = Random.Range(-boardY, boardY);
            int indexEnemy = Random.Range(0, enemyShip.Length);

            Vector2 position = new Vector2(randomX + _player.transform.position.x,
                randomY + _player.transform.position.y);

            Instantiate(enemyShip[indexEnemy], position, enemyShip[indexEnemy].transform.rotation);
        }
        
    }
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false; 
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        StartGame();
        MyMethod();
    }
    public void MoveMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
