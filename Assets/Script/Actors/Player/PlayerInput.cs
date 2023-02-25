using TS.Commons;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TS.Actors.Player
{
    public class PlayerInput : Singleton<PlayerInput>
    {
        public Vector3 movment;
        public bool fire;
        public bool fireDown;
        public bool aim;
        
        [SerializeField]
        private DynamicJoystick moveJoystick;

        [SerializeField]
        private FixedJoystick fireJoystick;

        public Vector3 fireDirection;

        private void Update()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            
            //判断如果没有输入再获取摇杆的值
            horizontal = horizontal == 0 ? moveJoystick.Horizontal : horizontal;
            vertical = vertical == 0 ? moveJoystick.Vertical : vertical;
            movment = new Vector3(horizontal, 0, vertical).normalized;
            fireDirection = new Vector3(fireJoystick.Horizontal, 0, fireJoystick.Vertical);

            if (!EventSystem.current.IsPointerOverGameObject())
            {
                fire = Input.GetButton("Fire1");
                if (Input.GetButtonDown("Fire1"))
                    fireDown = true;
                
                aim = Input.GetButton("Fire2");
            }
            else
            {
                // if (fireJoystick.Horizontal != 0 || fireJoystick.Vertical != 0)
                // {
                //     fire = true;
                //     fireDown = true;
                //     aim = true;
                // }
                fire = fireJoystick.Horizontal != 0 || fireJoystick.Vertical != 0;
                fireDown = aim = fire;
            }
        }

        public void Invalidate()
        {
            fireDown = false;
            fire = false;
        }
    }
}