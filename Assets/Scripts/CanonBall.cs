using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
public class CanonBall : MonoBehaviour {

    private Rigidbody rb;
    
    public float forceFactor;
    
    private CameraView mainCameraView;

    private Transform CameraPositionToFollowBall;

    private Canon canon;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mainCameraView = FindObjectOfType<CameraView>();
        CameraPositionToFollowBall = gameObject.transform.GetChild(0);
        canon = FindObjectOfType<Canon>();
    }

    // Use this for initialization
    void Start () {
        rb.AddRelativeForce(Vector3.forward * forceFactor, ForceMode.Impulse);
	}

    private void Update()
    {
        if (gameObject.transform.position.y < 0f)
        {
            Destroy(gameObject);
            return;
        }
            if (!Canon.isAllowedToFire)
        {
            mainCameraView.FollowCanonBall(gameObject, CameraPositionToFollowBall.position);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            if (TargetManager.Instance != null) TargetManager.Instance.TargetHit(collision.gameObject,20);
            FindObjectOfType<AudioManager>().Play("Ding");
            Destroy(gameObject);
            canon.ShotHit(true);
            mainCameraView.ResetCameraPosition();
            Transform targetTransform = collision.gameObject.transform;
            targetTransform.DOScale(0, 1f).SetEase(Ease.InOutCirc);
        }
        else if (collision.gameObject.CompareTag("Tree"))
        {
            mainCameraView.ResetCameraPosition();
            Transform treeTransform = collision.gameObject.transform;
            FindObjectOfType<AudioManager>().Play("treefallingdown");

            treeTransform.DORotate(new Vector3(75, treeTransform.localEulerAngles.y, treeTransform.localEulerAngles.z), 2)
                    .SetEase(Ease.InExpo)
                    .OnComplete
            (delegate
                {
                    Destroy(collision.gameObject, 1);
                }
            );
            canon.ShotHit(false);
            Destroy(gameObject, 1);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            FindObjectOfType<AudioManager>().Play("HitSomething");
            mainCameraView.ResetCameraPosition();
            canon.ShotHit(false);
            Destroy(gameObject);
        }
        
        Canon.isAllowedToFire = true;
    }
}
