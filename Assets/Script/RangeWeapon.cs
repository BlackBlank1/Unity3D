using UnityEngine;

public class RangeWeapon : Weapon
{
    [SerializeField]
    private Bullet bulletPrefab;
    
    [SerializeField]
    private Transform firePoint;
    public override void BeginAttack(Transform instigator)
    {
        Instantiate(bulletPrefab, firePoint.position, instigator.rotation);
    }

    public override void EndAttack()
    {
        
    }
}