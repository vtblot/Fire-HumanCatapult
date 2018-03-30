using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CanonBall : MonoBehaviour {

    private Rigidbody rb;
    
    public float forceFactor;
    
    private CameraView mainCameraView;

    private Transform CameraPositionToFollowBall;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mainCameraView = FindObjectOfType<CameraView>();
        CameraPositionToFollowBall = gameObject.transform.GetChild(0);
    }

    // Use this for initialization
    void Start () {
        rb.AddRelativeForce(Vector3.forward * forceFactor, ForceMode.Impulse);
	}

    private void Update()
    {
        if (!Canon.isAllowedToFire)
        {
            mainCameraView.FollowCanonBall(gameObject, CameraPositionToFollowBall.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            mainCameraView.ResetCameraPosition();
            Destroy(collision.gameObject, 2);
            //if (GameManager.Instance != null) GameManager.Instance.NextLevel();
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            mainCameraView.ResetCameraPosition();
            Destroy(gameObject, 2);
        }
        Canon.isAllowedToFire = true;
    }
}
