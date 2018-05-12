using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    private int highScore;
    [Header("Different Screen to display")]
    [SerializeField]
    private Canvas MainMenuCanvas;
    [SerializeField]
    private Canvas EndCanvas;
    [SerializeField]
    private TextMeshProUGUI WinMessage;
    [SerializeField]
    private TextMeshProUGUI LooseMessage;

    [Header("Scores Display")]
    [SerializeField]
    private TextMeshProUGUI endScoreText;
    [SerializeField]
    private TextMeshProUGUI highScoreText;
    


    private void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameMenu += GameMenu;
            GameManager.Instance.OnGameVictory += GameVictory;
            GameManager.Instance.OnGameOver += GameOver;
        }
    }
    
    private void Start()
    {
       
    }

    private void GameMenu()
    {
        FindObjectOfType<AudioManager>().Play("Intro");
        MainMenuCanvas.enabled = true;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        MainMenuCanvas.enabled = false;
        Cursor.visible = false;
        FindObjectOfType<AudioManager>().Stop("Intro");
        if (GameManager.Instance != null) GameManager.Instance.StartGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Replay()
    {
        if(FindObjectOfType<AudioManager>().IsPlaying("Success"))
            FindObjectOfType<AudioManager>().Stop("Success");
        else if(FindObjectOfType<AudioManager>().IsPlaying("GameOver"))
            FindObjectOfType<AudioManager>().Stop("GameOver");

        if (EndCanvas.enabled)
            EndCanvas.enabled = false;
        else if (WinMessage.enabled)
            WinMessage.enabled = false;
        else if (LooseMessage.enabled)
            LooseMessage.enabled = false;


        if (GameManager.Instance != null) GameManager.Instance.ReplayGame();
    }

    private void GameVictory()
    {
        FindObjectOfType<AudioManager>().Play("Success");
        if (GameManager.Instance != null) highScore = GameManager.Instance.GetHighestScore();
        highScoreText.text = highScore.ToString();
        if (GameManager.Instance != null) score = GameManager.Instance.GetEndScore();
        endScoreText.text = score.ToString();
        Cursor.visible = true;
        EndCanvas.enabled = true;
        WinMessage.enabled = true;
    }

    private void GameOver()
    {
        FindObjectOfType<AudioManager>().Play("GameOver");
        if (GameManager.Instance != null) highScore = GameManager.Instance.GetHighestScore();
        highScoreText.text = highScore.ToString();
        if (GameManager.Instance != null) score = GameManager.Instance.GetEndScore();
        endScoreText.text = score.ToString();
        Cursor.visible = true;
        EndCanvas.enabled = true;
        LooseMessage.enabled = true;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameMenu -= GameMenu;
            GameManager.Instance.OnGameVictory -= GameVictory;
            GameManager.Instance.OnGameOver -= GameOver;
        }
    }
}
