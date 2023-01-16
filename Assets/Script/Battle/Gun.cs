using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace TS.Battle
{

    public class Gun : RangeWeapon
    {
        public Transform firePoint;

        [SerializeField]
        private float fireInterval = 0.5f;

        private float fireCounter = 0;

        public override void BeginAttack()
        {
            if (fireCounter <= 0)
            {
                var bullet = Instantiate(bulletPrefab, firePoint.position, Owner.transform.rotation);
                bullet.damage = Owner.damage;
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

}