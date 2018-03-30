using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuManagerOld : MonoBehaviour {

    [SerializeField]
    private Canvas jumperCanvas;
    [SerializeField]
    private Canvas launchedObjectCanvas;

    private static MenuManagerOld m_Instance;

    public static MenuManagerOld Instance
    {
        get
        {
            return m_Instance;
        }
    }

    private void Awake()
    {
        DisableSimulationMenu(jumperCanvas);
        DisableSimulationMenu(launchedObjectCanvas);
    }

    private void Start()
    {
        if (GameManagerOld.Instance != null)
        {
            GameManagerOld.Instance.OnGameFirstStart += GameFirstStart;
            GameManagerOld.Instance.OnGamePlay += GamePlay;
            GameManagerOld.Instance.OnGameOver += GameOver;
        }
    }
    private void OnDestroy()
    {
        if (GameManagerOld.Instance != null)
        {
            GameManagerOld.Instance.OnGameFirstStart -= GameFirstStart;
            GameManagerOld.Instance.OnGamePlay -= GamePlay;
            GameManagerOld.Instance.OnGameOver -= GameOver;
        }
    }

    private void GameFirstStart()
    {
        //Show Start Canvas
        ResetSimulation();
    }
    private void GamePlay()
    {
        //Hide Start Canvas
    }
    private void GameOver()
    {
        //Show End Canvas
    }

    public void PlaySimulation()
    {
        if (GameManagerOld.Instance != null) GameManagerOld.Instance.PlaySimulation();
    }

    public void ResetSimulation()
    {
        if (GameManagerOld.Instance != null) GameManagerOld.Instance.ResetSimulation();
    }

    public void EnableSimulationMenu(Canvas menuToEnable)
    {
        menuToEnable.gameObject.SetActive(true);
        menuToEnable.transform.localScale = Vector3.zero;
        menuToEnable.transform.DOScale(1, 1f).SetEase(Ease.InOutCirc);
    }

    public void DisableSimulationMenu(Canvas menuToDisable)
    {
        menuToDisable.transform.localScale = Vector3.one;
        menuToDisable.transform.DOScale(0, 1f).SetEase(Ease.InOutCirc).OnComplete(delegate
        {
            menuToDisable.gameObject.SetActive(false);
        });
    }
}
