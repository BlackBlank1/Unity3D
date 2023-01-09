using System.Collections;
using TS.Commons;
using TS.UI;
using UnityEngine;

namespace TS.Actors.Player.States
{
    public class DeathState : BaseState
    {
        public override void Enter()
        {
            base.Enter();
            p.animator.SetTrigger(AnimID.Dead);
            // p.StartCoroutine(Util.Delay(p.deadDelay, () => { Object.Destroy(p.gameObject); }));
        }
    }
}