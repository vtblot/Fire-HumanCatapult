using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{

    private static HUDManager m_Instance;

    public static HUDManager Instance
    {
        get
        {
            return m_Instance;
        }
    }
    [SerializeField]
    private List<Image> AmmosAvailable = new List<Image>();
    [SerializeField]
    private TextMeshProUGUI AmmoText;
    [SerializeField]
    private Text scoreText;

    private void Awake()
    {
        if (m_Instance == null) m_Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        Init();
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnFire += OnFire;
            GameManager.Instance.ScoreChanged += ScoreChanged;
            GameManager.Instance.OnGamePlay += GamePlay;
            GameManager.Instance.OnGameVictory += GameVictory;
            GameManager.Instance.OnGameOver += GameOver;
        }
    }

    private void Init()
    {
        AmmoText.enabled = true;
        foreach (var ammo in AmmosAvailable)
        {
            ammo.enabled = true;
        }
    }

    private void OnDestroy()
    {

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnFire -= OnFire;
            GameManager.Instance.ScoreChanged -= ScoreChanged;
            GameManager.Instance.OnGamePlay -= GamePlay;
            GameManager.Instance.OnGameVictory -= GameVictory;
            GameManager.Instance.OnGameOver -= GameOver;
        }
    }

    private void OnFire()
    {

        foreach (var ammo in AmmosAvailable)
        {
            ammo.enabled = false;
            break;
        }
        AmmosAvailable.RemoveAt(0);
        if (AmmosAvailable.Count < 1)
        {
            AmmoText.enabled = false;
        }
    }

    private void ScoreChanged(int score)
    {
        scoreText.text = score.ToString();
    }

    private void GamePlay()
    {
        //SHOW SCORE TEXT
    }

    private void GameVictory()
    {
        //UPDATE HIGHSCORE TEXT
    }

    private void GameOver()
    {
        //UPDATE HIGHSCORE TEXT
    }
}

