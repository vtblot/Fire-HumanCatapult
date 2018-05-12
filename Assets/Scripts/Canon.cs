using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [Header("Canon Configuration")]
    [SerializeField]
    private Transform spawner;
    [SerializeField]
    private Transform pivotTube;
    [SerializeField]
    private float rotationSpeed;

    [Header("Canon Ball Configuration")]
    [SerializeField]
    private GameObject canonBallPrefab;
    [SerializeField]
    private int ammunitions;

    public static bool isAllowedToFire;
    private int ammunitionsAtStart;
    private int counterShotNotHitTarget;

    private float horizontal;
    private float vertical;

    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnLevelChanged += LevelChanged;
            GameManager.Instance.OnGamePlay += GamePlay;
        }
    }

    private void Start()
    {
        InitCanon();
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnLevelChanged -= LevelChanged;
            GameManager.Instance.OnGamePlay -= GamePlay;
        }
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        pivotTube.localEulerAngles =
            new Vector3(
                Utilities.ClampAngle(pivotTube.localEulerAngles.x + (vertical * rotationSpeed * Time.deltaTime), -70, -10),
                Utilities.ClampAngle(pivotTube.localEulerAngles.y + (horizontal * rotationSpeed * Time.deltaTime), -45, 45),
                0f
                );

        if (ammunitions > 0 && isAllowedToFire)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                FindObjectOfType<AudioManager>().Play("CanonShot");
                Instantiate(canonBallPrefab, spawner.position, spawner.rotation);
                isAllowedToFire = false;
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.ShotFired();
                }
            }
        }
    }

    private void GamePlay()
    {
        InitCanon();
    }

    private void LevelChanged()
    {
        InitCanon();
    }

    public void InitCanon()
    {
        ammunitions = 3;
        ammunitionsAtStart = ammunitions;
        counterShotNotHitTarget = 0;
        isAllowedToFire = true;
    }

    public void ShotHit(bool shotHitTarget)
    {
        ammunitions = ammunitions - 1;
        if (!shotHitTarget)
        {
            counterShotNotHitTarget++;
            int currentLevel;
            if (GameManager.Instance != null)
            {
                currentLevel = GameManager.Instance.GetCurrentLevel();
                if (currentLevel == 0 && counterShotNotHitTarget >= ammunitionsAtStart)
                    GameManager.Instance.GameOver();
                if (currentLevel == 1 && counterShotNotHitTarget == 2)
                    GameManager.Instance.GameOver();
            }
        }
    }
}
