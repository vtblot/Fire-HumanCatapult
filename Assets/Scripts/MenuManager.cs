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
        if (m_Instance == null) m_Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameMenu += MainMenu;
            GameManager.Instance.OnGameVictory += GameVictory;
            GameManager.Instance.OnGameOver += GameOver;
        }
    }

    private void MainMenu()
    {
        MainMenuCanvas.enabled = true;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        MainMenuCanvas.enabled = false;
        Cursor.visible = false;
        if (GameManager.Instance != null) GameManager.Instance.StartGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Replay()
    {
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
            GameManager.Instance.OnGameMenu -= MainMenu;
            GameManager.Instance.OnGameVictory -= GameVictory;
            GameManager.Instance.OnGameOver -= GameOver;
        }
    }
}
