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
        private FloatingJoystick moveJoystick;

        [SerializeField]
        private FixedJoystick fireJoystick;

        public Vector3 fireDirection;

        private void Update()
        {
            //获取水平输入
            var horizontal = Input.GetAxis("Horizontal");
            //获取垂直输入
            var vertical = Input.GetAxis("Vertical");

            //判断如果没有输入再获取摇杆的值
            horizontal = horizontal == 0 ? moveJoystick.Horizontal : horizontal;
            vertical = vertical == 0 ? moveJoystick.Vertical : vertical;
            //设置移动方向
            movment = new Vector3(horizontal, 0, vertical).normalized;
            //设置开火方向
            fireDirection = new Vector3(fireJoystick.Horizontal, 0, fireJoystick.Vertical);

            
            //判断是否在UI上
            bool isOnUI;
            
            //如果为移动平台
            if (Application.isMobilePlatform)
            {
                //如果为移动平台，则isOnUI为第一个手指的输入
                isOnUI = EventSystem.current.IsPointerOverGameObject(0);
            }
            else
            {
                isOnUI = EventSystem.current.IsPointerOverGameObject();
            }
            
            //如果不是在UI上
            if (!isOnUI)
            {
                //也不再安卓和苹果平台上
#if (!UNITY_ANDROID && !UNITY_IOS)
                //支持鼠标点击射击
                fire = Input.GetButton("Fire1");
                if (Input.GetButtonDown("Fire1"))
                    fireDown = true;

                aim = Input.GetButton("Fire2");
#else
                //判断当前如果fire为true且当前停止了射击
                if (fire && fireJoystick.Horizontal.Approximately(0) && fireJoystick.Vertical.Approximately(0))
                {
                    fire = aim = false;
                }
#endif
            }
            else
            {
                // if (fireJoystick.Horizontal != 0 || fireJoystick.Vertical != 0)
                // {
                //     fire = true;
                //     fireDown = true;
                //     aim = true;
                // }
                
                //Approximately用来近似数值，可能会有0.00000000001的这种
                fire = !fireJoystick.Horizontal.Approximately(0)  || !fireJoystick.Vertical.Approximately(0);
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