using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateSimulationObjectMenu : MonoBehaviour
{

    [SerializeField]
    private SimulationObject objectToUpdate;
    [SerializeField]
    private TextMeshProUGUI massText;
    [SerializeField]
    private TextMeshProUGUI dragText;

    private Rigidbody rb;
    private float mass;
    private float drag;

    // Use this for initialization
    void Start()
    {           
        rb = objectToUpdate.GetComponent<Rigidbody>();
        mass = rb.mass;
        drag = rb.drag;

        massText.text = mass.ToString();
        dragText.text = drag.ToString();
    }

    public void UpdateSimulationObject()
    {
        mass = float.Parse(massText.text);
        drag = float.Parse(dragText.text);
        objectToUpdate.UpdateObject(mass, drag);
        massText.text = mass.ToString();
        dragText.text = drag.ToString();
    }
}
