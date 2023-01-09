using System;
using UnityEngine;

namespace TS.Actors.Player
{
    public class PlayerRotate : MonoBehaviour
    {
        [SerializeField]
        private float speed = 2f;

        [SerializeField]
        private float angle = 2f;
        
        private void Update()
        {
            Rotate();
        }

        private void Rotate()
        {
            if (Input.GetMouseButton(0))
            {
                
            }
        }
    }
}