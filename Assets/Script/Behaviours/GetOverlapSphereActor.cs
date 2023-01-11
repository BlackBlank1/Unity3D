using NodeCanvas.Framework;
using ParadoxNotion.Design;
using TS.Actors;
using UnityEngine;

namespace TS.Behaviors
{
    [Category("Custom")]
    public class GetOverlapSphereActor : ActionTask<Transform>
    {
        public LayerMask LayerMask = -1;
        public BBParameter<float> Radius = 5f;
        public BBParameter<bool> NeedAlive = true;
        [BlackboardOnly]
        public BBParameter<Actor> OutActor; 

        private Collider[] outCols = new Collider[1];

        protected override void OnExecute()
        {
            var num = Physics.OverlapSphereNonAlloc(agent.position, Radius.value, outCols, LayerMask);

            var success = false;
            if (num != 0 && outCols[0].TryGetComponent(out Actor actor))
            {
                if (NeedAlive.value)
                    success = !actor.IsDead;
                else
                    success = true;

                if (success)
                    OutActor.value = actor;
            }

            EndAction(success);
        }

        public override void OnDrawGizmosSelected()
        {
            if (agent != null)
            {
                Gizmos.color = new Color(1, 1, 1, 0.2f);
                Gizmos.DrawSphere(agent.position, Radius.value);
            }
        }
        
    }
}