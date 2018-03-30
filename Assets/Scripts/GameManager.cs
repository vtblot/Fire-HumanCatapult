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

    public event Action OnGameMenu;
    public event Action OnGamePlay;
    public event Action OnGameVictory;
    public event Action OnGameOver;
    public event Action OnLevelChanged;

    private int currentLevel;
    private int totalLevels = 3;

    private void Awake()
    {
        if (m_Instance == null) m_Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        InitGame();

        OnGameOver += GameOver;
    }

    private void InitGame()
    {
        currentLevel = 0;
    }

    public void NextLevel()
    {
        if (currentLevel < totalLevels)
        {
            currentLevel++;
            if (OnLevelChanged != null) OnLevelChanged();
        }
        else
        {
            if (OnGameVictory != null) OnGameVictory();
        }
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
