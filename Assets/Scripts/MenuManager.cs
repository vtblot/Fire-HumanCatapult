using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    private static MenuManager m_Instance;

    public static MenuManager Instance
    {
        get
        {
            return m_Instance;
        }
    }
    private int score;
    [SerializeField]
    private Text endScore;
    [SerializeField]
    private Text highScore;



    private void Awake()
    {
        if (m_Instance == null) m_Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGamePlay += GameStarted;
            GameManager.Instance.ScoreChanged += ScoreChanged;
            GameManager.Instance.OnGameVictory += GameVictory;
            GameManager.Instance.OnGameOver += GameOver;
        }
    }

    public void StartGame()
    {
        if (GameManager.Instance != null) GameManager.Instance.StartGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Replay()
    {
        if (GameManager.Instance != null) GameManager.Instance.ReplayGame();
    }

    private void updateHighScore()
    {
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    private void GameStarted()
    {
        updateHighScore();
    }

    private void ScoreChanged(int newScore)
    {

    }

    private void GameVictory()
    {
        updateHighScore();
    }

    private void GameOver()
    {
        updateHighScore();
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGamePlay -= GameStarted;
            GameManager.Instance.OnGameVictory -= GameVictory;
            GameManager.Instance.OnGameOver -= GameOver;
        }
    }
}
