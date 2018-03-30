using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ObjectToLaunch : MonoBehaviour {

    static Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>()
;	}

    public static void PropulseObject(Vector3 velocity)
    {
        rb.AddForce(new Vector3(0, Mathf.Abs(velocity.y), 0),ForceMode.Impulse);
    }
}
