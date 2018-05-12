using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager m_Instance;

    public static GameManager Instance
    {
        get
        {
            return m_Instance;
        }
    }
    [SerializeField]
    private GameObject[] levels;

    public event Action OnGameMenu;
    public event Action OnGamePlay;
    public event Action OnGameVictory;
    public event Action OnGameOver;
    public event Action OnLevelChanged;
    public event Action OnFire;
    public event Action<int> ScoreChanged;

    private PlayerProgress playerProgress;

    private int currentLevel;
    private int totalLevels = 1;

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
        LoadLevel(currentLevel);
        LoadHighScore();
        if (OnGameMenu != null) OnGameMenu();
    }

    public void ReplayGame()
    {
        currentLevel = 0;
        totalScore = 0;
        LoadLevel(currentLevel);
        if (TargetManager.Instance != null) TargetManager.Instance.ResetTargets();
        if (OnLevelChanged != null) OnLevelChanged();
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
                if (currentLevel <= totalLevels)
                {
                    currentLevel++;
                }
            }
        }
        if (currentLevel > totalLevels)
        {
            if (totalScore >= playerProgress.m_highestScore)
            {
                SavePlayerProgress(totalScore);
            }
            if (OnGameVictory != null) OnGameVictory();
        }
        else
        {
            LoadLevel(currentLevel);
            if (OnLevelChanged != null) OnLevelChanged();
        }
    }

    private void LoadLevel(int level)
    {
            for(int i = 0;  i<levels.Length;i++)
            {
                if (levels[i].activeInHierarchy)
                    levels[i].SetActive(false);
            }
            levels[level].SetActive(true);
        
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

    public int GetCurrentLevel()
    {
        return currentLevel;
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
