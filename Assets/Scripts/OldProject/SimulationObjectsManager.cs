using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationObjectsManager : MonoBehaviour
{
    List<SimulationObject> m_SimulationObjects = new List<SimulationObject>();

    [SerializeField]
    private Bascule bascule;

    private static SimulationObjectsManager m_Instance;

    public static SimulationObjectsManager Instance
    {
        get
        {
            return m_Instance;
        }
    }

    private void Start()
    {
        if (GameManagerOld.Instance != null)
        {
            GameManagerOld.Instance.OnGamePlay += GamePlay;
            GameManagerOld.Instance.OnSimulationObjectsPlay += SimulationObjectsPlay;
            GameManagerOld.Instance.OnSimulationObjectsReset += SimulationObjectsReset;
        }

        AddSimulationObjectsToList();
        
    }
    private void OnDestroy()
    {
        if (GameManagerOld.Instance != null)
        {
            GameManagerOld.Instance.OnGamePlay -= GamePlay;
            GameManagerOld.Instance.OnSimulationObjectsPlay -= SimulationObjectsPlay;
            GameManagerOld.Instance.OnSimulationObjectsReset -= SimulationObjectsReset;
        }
    }

    void AddSimulationObjectsToList()
    {
        SimulationObject[] existingSimulationObjects = GameObject.FindObjectsOfType<SimulationObject>();
        foreach (var simulationObject in existingSimulationObjects)
        {
            m_SimulationObjects.Add(simulationObject);
        }
    }

    private void GamePlay()
    {

    }

    private void SimulationObjectsPlay()
    {
        foreach (var simulationObject in m_SimulationObjects)
        {
            simulationObject.UnfreezeObject();
        }
    }

    private void SimulationObjectsReset()
    {
        foreach (var simulationObject in m_SimulationObjects)
        {
            simulationObject.FreezeObject();
            simulationObject.ResetObject();
        }

        bascule.ResetBasculePosition();
    }
}
