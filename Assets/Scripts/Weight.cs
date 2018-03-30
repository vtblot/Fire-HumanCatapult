using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weight : MonoBehaviour {

    private Rigidbody rb;

    [SerializeField]
    private float massFactor;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        rb.AddForce(Vector3.down * rb.mass * massFactor);
	}
}
