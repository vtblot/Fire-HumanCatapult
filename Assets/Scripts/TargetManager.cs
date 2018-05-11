using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour {

    private List<GameObject> m_Targets = new List<GameObject>();
    private bool isLevelFinished;

    private static TargetManager m_Instance;

    public static TargetManager Instance
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
        isLevelFinished = false;
    }

    private void Start()
    {
        UpdateTargetList();
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnLevelChanged += LevelChanged;
        }
    }
    
    private void OnDestroy()
    {
        UpdateTargetList();

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnLevelChanged -= LevelChanged;
        }
    }

    void UpdateTargetList()
    {

        GameObject[] existingTargets = GameObject.FindGameObjectsWithTag("Target");
        foreach (var target in existingTargets)
        {
            m_Targets.Add(target);
        }
    }

    private void LevelChanged()
    {
        UpdateTargetList();
    }

    public void TargetHit(GameObject Target, int score)
    {
        if (GameManager.Instance != null) GameManager.Instance.UpdateScore(score);
        RemoveFromList(Target);
    }

    private void RemoveFromList(GameObject go)
    {
        m_Targets.Remove(go);
        Destroy(go,2);
        if (m_Targets.Count == 0)
        {
            isLevelFinished = true;
            if (GameManager.Instance != null) GameManager.Instance.ChangeLevel();
        }
    }

    public bool CheckIfLevelFinished()
    {
        return isLevelFinished;
    }

}
