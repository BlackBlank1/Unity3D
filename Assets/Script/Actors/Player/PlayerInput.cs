using TS.Commons;
using UnityEngine;

namespace TS.Actors.Player
{
    public class PlayerInput : Singleton<PlayerInput>
    {
        public Vector2 movment;
        public bool fire;
        public bool fireDown;
        public bool aim;
        
        private void Update()
        {
            movment = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            fire = Input.GetButton("Fire1");
            if (Input.GetButtonDown("Fire1"))
                fireDown = true;
            
            aim = Input.GetButton("Fire2");
        }
        
    }
}