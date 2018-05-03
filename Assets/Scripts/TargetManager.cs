using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour {

    List<GameObject> m_Targets = new List<GameObject>();

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
    }

    private void Start()
    {
        AddTargetsToList();

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnLevelChanged += LevelChanged;
        }
    }

    private void OnDestroy()
    {
        AddTargetsToList();

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnLevelChanged -= LevelChanged;
        }
    }

    void AddTargetsToList()
    {
        if (m_Targets.Count > 0)
            m_Targets.Clear();

        GameObject[] existingTargets = GameObject.FindGameObjectsWithTag("Target");
        foreach (var target in existingTargets)
        {
            m_Targets.Add(target);
        }
    }

    private void LevelChanged()
    {
        AddTargetsToList();
    }

    public void TargetHit(int score)
    {
        if (GameManager.Instance != null) GameManager.Instance.UpdateScore(score);
    }

}
