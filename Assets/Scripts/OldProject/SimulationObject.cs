using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SimulationObject : MonoBehaviour {

    private Rigidbody rb;

    private Vector3 pos;

    private Quaternion rot;

    private float mass;

    private float drag;

	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody>();
        pos = transform.position;
        rot = transform.rotation;
        mass = rb.mass;
        drag = rb.drag;
	}

    public void ResetObject()
    {
        transform.position = pos;
        transform.rotation = rot;
        rb.mass = mass;
        rb.drag = drag;
    }

    public void UpdateObject(float mass, float drag)
    {
        rb.mass = mass;
        rb.drag = drag;
    }

    public void FreezeObject()
    {
        rb.isKinematic = true;
    }

    public void UnfreezeObject()
    {
        rb.isKinematic = false;
    }

    

}
