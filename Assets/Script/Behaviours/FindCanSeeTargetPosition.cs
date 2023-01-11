using System.Collections.Generic;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using TS.Commons;
using UnityEngine;
using UnityEngine.AI;

namespace TS.Behaviors
{
    [Category("Custom")]
    public class FindCanSeeTargetPosition : ActionTask<NavMeshAgent>
    {
        [RequiredField]
        public BBParameter<GameObject> Target;
        [Tooltip("A layer mask to use for line of sight check.")]
        public BBParameter<LayerMask> LayerMask;
        public BBParameter<float> Radius = 10f;
        public BBParameter<Vector2> RadiusRandomness = new Vector2(-3f, 3f);
        public BBParameter<Vector2> AngleRandomness = new Vector2(0f, 360f);
        public BBParameter<int> SamplePoints = 10;
        public BBParameter<bool> IsWalkable = true;
        public BBParameter<float> EyeHeight = 1f;
        [BlackboardOnly]
        public BBParameter<Vector3> OutPosition;

        private List<Vector3> candidates;
        private int selectedIndex;
        private RaycastHit canSeeHit;
        
        protected override string info => "Find position to see " + Target;

        protected override string OnInit()
        {
            candidates = new List<Vector3>();
            return base.OnInit();
        }

        protected override void OnExecute()
        {
            base.OnExecute();
            var t = Target.value.transform;
            
            // Sample points on one circle
            candidates.Clear();
            var targetCollider = t.GetComponentInChildren<Collider>();
            var center = t.position;
            var lineCastCenter = center;
            lineCastCenter.y += EyeHeight.value;
            var deltaAngle = 2f * Mathf.PI / SamplePoints.value;
            var angle = Random.Range(AngleRandomness.value.x, AngleRandomness.value.y);
            for (var i = 0; i < SamplePoints.value; i++)
            {
                var pos = center + new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) 
                    * (Radius.value + Random.Range(RadiusRandomness.value.x, RadiusRandomness.value.y));
                angle += deltaAngle;
                
                if (IsWalkable.value)
                {
                    if (!UnityUtil.FindNearestWalkablePosition(ref pos))
                        continue;
                }

                var lineCastPos = pos;
                lineCastPos.y += EyeHeight.value;
                if (Physics.Linecast(lineCastPos, lineCastCenter, out canSeeHit, LayerMask.value)
                    && canSeeHit.collider == targetCollider)
                {
                    candidates.Add(pos);
                }

                Debug.DrawLine(lineCastPos, lineCastCenter, canSeeHit.collider == targetCollider ? Color.green : Color.red, .5f);
            }

            // If no position found, return position of target
            if (candidates.Count == 0)
            {
                OutPosition.value = t.position;
                EndAction(true);
                return;
            }
            
            // Find nearest from candidates
            var minDistance = float.PositiveInfinity;
            for (var i = 0; i < candidates.Count; i++)
            {
                var distance = (candidates[i] - agent.transform.position).sqrMagnitude;
                if (distance < minDistance)
                {
                    selectedIndex = i;
                    minDistance = distance;
                }
            }

            // selectedIndex = Random.Range(0, candidates.Count);
            OutPosition.value = candidates[selectedIndex];
            EndAction(true);
        }

        public override void OnDrawGizmosSelected()
        {
            if (!Application.isPlaying || candidates == null || candidates.Count == 0) return;
            for (var i = 0; i < candidates.Count; i++)
            {
                if (i == selectedIndex)
                    Gizmos.color = new Color(0, 1, 0, 0.2f);
                else
                    Gizmos.color = new Color(1, 1, 1, 0.2f);
                Gizmos.DrawSphere(candidates[i], .5f);
            }
        }
        
    }
}