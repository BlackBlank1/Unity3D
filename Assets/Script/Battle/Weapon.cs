using TS.Actors;
using UnityEngine;

namespace TS.Battle
{

    public abstract class Weapon : MonoBehaviour
    {
        public Actor Owner;

        public abstract void BeginAttack();

        public abstract void EndAttack();
    }

}