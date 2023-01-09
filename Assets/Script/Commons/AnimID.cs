using UnityEngine;

namespace TS.Commons
{
    public static class AnimID
    {
        public static readonly int IsAiming = Animator.StringToHash("IsAiming");
        public static readonly int IsFiring = Animator.StringToHash("IsFiring");
        public static readonly int Dead = Animator.StringToHash("Dead");
    }
}