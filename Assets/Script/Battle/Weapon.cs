using TS.Actors;
using UnityEngine;

namespace TS.Battle
{

    public abstract class Weapon : MonoBehaviour
    {
        public Actor Owner;

        public abstract void BeginAttack(Transform instigator);

        public abstract void EndAttack();
    }

}