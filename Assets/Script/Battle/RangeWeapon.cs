using UnityEngine;

namespace TS.Battle
{

    public class RangeWeapon : Weapon
    {
        [SerializeField]
        protected Bullet bulletPrefab;

        [SerializeField]
        private Transform firePoint;

        public override void BeginAttack()
        {
            var bullet = Instantiate(bulletPrefab, firePoint.position, Owner.transform.rotation);
            bullet.damage = Owner.damage;
        }

        public override void EndAttack()
        {

        }
    }

}