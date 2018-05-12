using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour {

    private List<GameObject> m_Targets;
    private bool isLevelFinished;

    [SerializeField]
    private Transform TargetLevel1DefaultPos;
    [SerializeField]
    private Transform TargetLevel2DefaultPos;
    [SerializeField]
    private Transform Target2Level2DefaultPos;

    [SerializeField]
    private GameObject TargetLevel1;
    [SerializeField]
    private GameObject TargetLevel2;
    [SerializeField]
    private GameObject Target2Level2;

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
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnLevelChanged += LevelChanged;
        }
    }

    private void Start()
    {
        isLevelFinished = false;
        UpdateTargetList();
        
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
        m_Targets = new List<GameObject>();
        m_Targets.TrimExcess();
        GameObject[] existingTargets = GameObject.FindGameObjectsWithTag("Target");
        foreach (var target in existingTargets)
        {
            m_Targets.Add(target);
        }
    }

    private void LevelChanged()
    {
        UpdateTargetList();
        ResetTargets();
    }

    public void TargetHit(GameObject Target, int score)
    {
        if (GameManager.Instance != null) GameManager.Instance.UpdateScore(score);
        RemoveFromList(Target);
    }

    private void RemoveFromList(GameObject go)
    {
        m_Targets.Remove(go);
        go.SetActive(false);
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

    public void ResetTargets()
    {
        TargetLevel1.gameObject.transform.position = TargetLevel1DefaultPos.position;
        TargetLevel1.gameObject.transform.rotation = TargetLevel1DefaultPos.rotation;
        TargetLevel1.gameObject.transform.localScale = TargetLevel1DefaultPos.localScale;

        TargetLevel2.gameObject.transform.position = TargetLevel2DefaultPos.position;
        TargetLevel2.gameObject.transform.rotation = TargetLevel2DefaultPos.rotation;
        TargetLevel2.gameObject.transform.localScale = TargetLevel2DefaultPos.localScale;

        Target2Level2.gameObject.transform.position = Target2Level2DefaultPos.position;
        Target2Level2.gameObject.transform.rotation = Target2Level2DefaultPos.rotation;
        Target2Level2.gameObject.transform.localScale = TargetLevel1DefaultPos.localScale;
    }

}
