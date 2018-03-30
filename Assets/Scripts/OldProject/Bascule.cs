using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bascule : MonoBehaviour {

    private Vector3 pos;
    private Quaternion rot;

    private void Awake()
    {
        pos = transform.position;
        rot = transform.rotation;
    }

    public void ResetBasculePosition()
    {
        transform.position = pos;
        transform.rotation = rot;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            ObjectToLaunch.PropulseObject(rb.velocity);          
        }
    }
}
