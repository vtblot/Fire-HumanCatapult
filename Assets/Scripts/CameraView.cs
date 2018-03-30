using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraView : MonoBehaviour {

    [SerializeField]
    private Transform target;
    [SerializeField]
    private Ease MoveAnimation;
    [SerializeField]
    private float MoveDuration;


    [SerializeField]
    private Transform CanonViewAnchor;


    public void MoveTo(Transform _target)
    {
        transform.DOMove(_target.position, MoveDuration).SetEase(MoveAnimation);
        transform.DORotate(_target.rotation.eulerAngles, MoveDuration).SetEase(MoveAnimation);
    }

    public void FollowCanonBall(GameObject canonBall, Vector3 AnchorPos)
    {
       transform.position = AnchorPos;
       transform.LookAt(canonBall.transform);
    }

    public void ResetCameraPosition()
    {
        MoveTo(CanonViewAnchor);
    }
}
