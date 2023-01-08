using UnityEngine;

namespace TS.Battle
{

    public class AimLine : MonoBehaviour
    {
        [SerializeField]
        private float length = 10f;
        
        private LineRenderer aimLine;

        private void Start()
        {
            aimLine = GetComponent<LineRenderer>();
        }

        public void Show(Vector3 startPosition, Vector3 direction)
        {
            var endPosition = startPosition + direction * length;
            Draw(startPosition, endPosition);
        }
        
        public void Draw(Vector3 startPosition, Vector3 endPosition)
        {
            aimLine.enabled = true;
            aimLine.SetPosition(0, startPosition);
            aimLine.SetPosition(1, endPosition);
        }

        public void Hide()
        {
            aimLine.enabled = false;
        }
    }

}