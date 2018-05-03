﻿using System.Collections;
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
    public int ammunitions;

    public static bool isAllowedToFire;


    private float horizontal;
    private float vertical;

    private void Start()
    {
        InitCanon();
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnLevelChanged += LevelChanged;
        }
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnLevelChanged -= LevelChanged;
        }
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        // pivotTube.Rotate(new Vector3(vertical, 0, -horizontal) * rotationSpeed * Time.deltaTime);
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
                Instantiate(canonBallPrefab, spawner.position, spawner.rotation);
                ammunitions--;
                isAllowedToFire = false;
                if(GameManager.Instance != null)
                {
                    GameManager.Instance.ShotFired();
                }
            }
        }
    }

    private void LevelChanged()
    {
        InitCanon();
    }

    public void InitCanon()
    {
        ammunitions = 3;
        isAllowedToFire = true;
    }
}
