using UnityEngine;
using UnityEngine.EventSystems;

namespace TS.UI
{
    public class ModelDragArea : MonoBehaviour, IDragHandler
    {

        [SerializeField]
        private Transform rotateTarget;

        [SerializeField]
        private float rotateSpeed;
        
        public void OnDrag(PointerEventData eventData)
        {
            var delta = eventData.delta;
            rotateTarget.Rotate(Vector3.up, Time.deltaTime * rotateSpeed * -delta.x);
        }
    }
}