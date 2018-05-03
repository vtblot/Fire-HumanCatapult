using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private static GameManager m_Instance;

    public static GameManager Instance
    {
        get
        {
            return m_Instance;
        }
    }

    public event Action OnGameMenu;
    public event Action OnGamePlay;
    public event Action OnGameVictory;
    public event Action OnGameOver;
    public event Action OnLevelChanged;
    public event Action OnFire;
    public event Action<int> ScoreChanged;

    private int currentLevel;
    private int totalLevels = 3;

    private int totalScore;

    private void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        OnGameOver += GameOver;
        currentLevel = 0;
        totalScore = 0;
        if (OnGameMenu != null) OnGameMenu();
    }

    public void ReplayGame()
    {
        currentLevel = 0;
        totalScore = 0;
        if (OnGamePlay != null) OnGamePlay();
        if (ScoreChanged != null) ScoreChanged(totalScore);
    }

    public void StartGame()
    {
        if (OnGamePlay != null) OnGamePlay();
    }

    public void NextLevel()
    {
        if (currentLevel < totalLevels)
        {
            currentLevel++;
            SceneManager.LoadScene(currentLevel);
            if (OnLevelChanged != null) OnLevelChanged();
        }
        else
        {
            if(totalScore > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", totalScore);
            }
            if (OnGameVictory != null) OnGameVictory();
        }
    }

    public void ShotFired()
    {
        if (OnFire != null) OnFire();
    }

    public void UpdateScore(int score)
    {
        totalScore += score;
        if (ScoreChanged != null) ScoreChanged(totalScore);
    }

    private void GameOver()
    {
        if (OnGameOver != null) OnGameOver();
    }

    private void OnDestroy()
    {
        OnGameOver -= GameOver;
    }
}
