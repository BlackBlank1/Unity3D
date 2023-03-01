using System;
using TS.Battle;
using UnityEngine;

namespace TS.Actors.Player
{
    public class Fallingthunder : Weapon
    {
        [SerializeField]
        protected Bullet bulletPrefab;

        [SerializeField]
        private Transform[] firePoints;
        

        public float mutiplier = 1.2f;
        
        private void Awake()
        {
            var addHpButton = FindObjectOfType<Skills>();
            addHpButton.DamageFalling += DamageFalling;
        }

        private void DamageFalling()
        {
            for (int i = 0; i < firePoints.Length; i++)
            {
                var bullet = Instantiate(bulletPrefab, firePoints[i].position, firePoints[i].rotation);
                bullet.damage = Owner.damage * mutiplier;
            }
        }

        public override void BeginAttack()
        {
            DamageFalling();
        }

        public override void EndAttack()
        {
        }
    }
}