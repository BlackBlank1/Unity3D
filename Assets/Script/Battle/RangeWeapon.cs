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
            //生成子弹
            var bullet = Instantiate(bulletPrefab, firePoint.position, Owner.transform.rotation);
            //子弹的伤害等于发射子弹的物体的伤害
            bullet.damage = Owner.damage;
        }

        public override void EndAttack()
        {

        }
    }

}