using System.Collections;
using RobustFSM.Mono;
using TS.Actors.Player.States;
using TS.Battle;
using TS.Commons;
using TS.UI;
using UnityEngine;

namespace TS.Actors.Player
{

    public class PlayerController : Actor
    {
        [SerializeField]
        private float gravity = 20f;
        
        public float normalSpeed = 10f;
        
        public float aimSpeed = 5f;

        public float normalTurnSpeed = 1440f;

        public float aimTurnSpeed = 360f;

        public float fireDelay = 0.1f;

        [SerializeField]
        private LayerMask groundLayer;
        
        public float deadDelay;

        private MonoFSM fsm;
        private CharacterController cc;
        public Animator animator;
        public Gun gun;
        private new Camera camera;


        public Vector3 moveDirection;
        [HideInInspector]
        public AimLine aimLine;
        [HideInInspector]
        public Vector3 aimDirection;
        [HideInInspector]
        public PlayerInput input;

        
        private void Awake()
        {
            cc = GetComponent<CharacterController>();
            gun = GetComponentInChildren<Gun>();
            camera = Camera.main;
            animator = GetComponent<Animator>();
            aimLine = GetComponentInChildren<AimLine>();
            fsm = GetComponent<MonoFSM>();
            InitFSM();
        }

        protected override void Start()
        {
            base.Start();
            input = PlayerInput.Instance;
            moveDirection = transform.forward;
        }

        /// <summary>
        /// 初始化状态机
        /// </summary>
        private void InitFSM()
        {
            fsm.AddState<NormalState>();
            fsm.AddState<PreAimState>();
            fsm.AddState<AimState>();
            fsm.AddState<DeathState>();
            
            fsm.SetInitialState<NormalState>();
        }

        /// <summary>
        /// 处理移动
        /// </summary>
        public void HandleMovement(float speed)
        {
            var movement = new Vector3(
                Input.GetAxis("Horizontal"), 
                0,  
                Input.GetAxis("Vertical")).normalized;

            if (movement != Vector3.zero)
                moveDirection = movement;
            
            var velocity = movement;
            velocity.y = -gravity;
            velocity = speed * velocity;
            cc.Move(velocity * Time.deltaTime);
            
            var localVelocity = transform.InverseTransformVector(velocity);
            animator.SetBool("IsRunning", movement.x != 0 || movement.z != 0);
            animator.SetFloat("SpeedX", localVelocity.x);
            animator.SetFloat("SpeedZ", localVelocity.z);
        }

        public void RotateTowards(Vector3 direction, float turnSpeed)
        {
            var targetRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, turnSpeed * Time.deltaTime);
        }

        public void RotateTowardsMovement()
        {
            RotateTowards(moveDirection, normalTurnSpeed);
        }

        /// <summary>
        /// 转身朝向瞄准方向
        /// </summary>
        public void RotateTowardsAim(float? turnSpeed = null)
        {
            if (turnSpeed == null)
                turnSpeed = aimTurnSpeed;
            
            // TODO 适配触屏摇杆
            // 根据鼠标位置来进行转向
            var ray = camera.ScreenPointToRay(Input.mousePosition);
            var isHit = Physics.Raycast(ray, out var hitInfo, float.PositiveInfinity, groundLayer);
            if (isHit)
            {
                aimDirection = hitInfo.point - transform.position;
                aimDirection.y = 0;
                RotateTowards(aimDirection, turnSpeed.Value);
            }
            else
            {
                aimDirection = transform.forward;
            }
        }
        
        protected override void Die()
        {
            base.Die();
            fsm.ChangeState<DeathState>();
        }
    }

}
