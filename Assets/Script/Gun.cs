using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private float fireInterval = 0.5f;

    private float fireCounter = 0;
    
    public void Fire(Transform instigator)
    {
        if (fireCounter <= 0)
        {
            Instantiate(bulletPrefab, firePoint.position, instigator.rotation);
            fireCounter = fireInterval;
        }
    }

    private void Update()
    {
        if (fireCounter > 0)
        {
            fireCounter -= Time.deltaTime;
        }
    }
}
