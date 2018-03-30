using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerOld : MonoBehaviour {

    private int currentLevel;

    public event Action OnGameFirstStart;
    public event Action OnGameMenu;
    public event Action OnGamePlay;
    public event Action OnGameOver;
    public event Action OnSimulationObjectsPlay;
    public event Action OnSimulationObjectsReset;
    

    private static GameManagerOld m_Instance;

    public static GameManagerOld Instance
    {
        get
        {
            return m_Instance;
        }
    }

    private void Awake()
    {
        if (m_Instance == null) m_Instance = this;
        else Destroy(gameObject);
    }

    public void StartGame()
    {
        if (OnGamePlay != null) OnGamePlay();
    }

    // Use this for initialization
    void Start () {
        if (OnGameFirstStart != null) OnGameFirstStart();
        if (OnGameMenu != null) OnGameMenu();
        currentLevel = 0;
    }

    public void NextLevel()
    {
        currentLevel++;
        SceneManager.LoadScene(currentLevel);
    }

    public void PlaySimulation()
    {
        if (OnSimulationObjectsPlay != null) OnSimulationObjectsPlay();
    }

    public void ResetSimulation()
    {
        if (OnSimulationObjectsReset != null) OnSimulationObjectsReset();
    }
}
