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

    private PlayerProgress playerProgress;

    private int currentLevel;
    private int totalLevels = 0;

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
        currentLevel = 0;
        totalScore = 0;
        LoadHighScore();
        if (OnGameMenu != null) OnGameMenu();
    }

    public void ReplayGame()
    {
        currentLevel = 0;
        totalScore = 0;
        SceneManager.LoadScene(0);
        if (OnGamePlay != null) OnGamePlay();
        if (ScoreChanged != null) ScoreChanged(totalScore);
    }

    public void StartGame()
    {
        if (OnGamePlay != null) OnGamePlay();
    }

    public void ChangeLevel()
    {
        if (TargetManager.Instance != null)
        {
            bool isLevelFinished = TargetManager.Instance.CheckIfLevelFinished();
            if(isLevelFinished)
            {
                if (currentLevel < totalLevels)
                {
                    currentLevel++;
                    //SceneManager.LoadScene(currentLevel);
                    if (OnLevelChanged != null) OnLevelChanged();
                }
            }
        }
        if (currentLevel >= totalLevels)
        {
            if (totalScore >= playerProgress.m_highestScore)
            {
                SavePlayerProgress(totalScore);
            }
            if (OnGameVictory != null) OnGameVictory();
        }
    }

    public void ShotFired()
    {
        if (OnFire != null) OnFire();
    }

    private void LoadHighScore()
    {
        playerProgress = new PlayerProgress();
        if(PlayerPrefs.HasKey("highestScore"))
        {
            playerProgress.m_highestScore = PlayerPrefs.GetInt("highestScore");
        }
    }

    private void SavePlayerProgress(int newHighScore)
    {
        playerProgress.m_highestScore = newHighScore;
        PlayerPrefs.SetInt("highestScore", playerProgress.m_highestScore);
    }

    public int GetHighestScore()
    {
        int hs = playerProgress.m_highestScore;
        return hs;
    }

    public int GetEndScore()
    {
        return totalScore;
    }

    public void UpdateScore(int score)
    {
        totalScore += score;
        if (ScoreChanged != null) ScoreChanged(totalScore);
        
    }

    public void GameOver()
    {
        if (totalScore >= playerProgress.m_highestScore)
        {
            SavePlayerProgress(totalScore);
        }
        if (OnGameOver != null) OnGameOver();
    }
}
