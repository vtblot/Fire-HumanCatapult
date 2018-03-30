using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFocus : MonoBehaviour {

    [SerializeField]
    private Transform target;
    [SerializeField]
    private Ease MoveAnimation;
    [SerializeField]
    private float MoveDuration;

    private bool needToFocusLaunchedObject = false;
    private Tweener lookAtTweener;

    public void MoveTo(GameObject _targetObject)
    {

      transform.DOMove(_targetObject.transform.position, MoveDuration).SetEase(MoveAnimation);
      transform.DORotate(_targetObject.transform.rotation.eulerAngles, MoveDuration).SetEase(MoveAnimation);

        if (_targetObject.gameObject.name.Equals("OverviewPosition"))
        {
            if (!needToFocusLaunchedObject)
            {
                needToFocusLaunchedObject = true;
                lookAtTweener = transform.DOLookAt(target.position, 4);
            }
            else
            {
                needToFocusLaunchedObject = false;
                lookAtTweener.Kill();
            }
        }
    }
}
