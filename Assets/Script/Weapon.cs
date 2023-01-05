using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public Actor Owner;
    
    public abstract void BeginAttack(Transform instigator);
    
    public abstract void EndAttack();
}