using System.Collections;
using RobustFSM.Mono;
using TS.Actors.Player.States;
using TS.Battle;
using TS.Commons;
using UnityEngine;

namespace TS.Actors.Player
{

    public class PlayerController : Actor
    {
        [SerializeField]
        private float gravity = 20f;

        [SerializeField]
        private float speed = 10f;

        public float fireDelay = 0.1f;

        [SerializeField]
        private LayerMask groundLayer;

        [SerializeField]
        private float deadDelay;

        private MonoFSM fsm;
        private CharacterController cc;
        public Gun gun;
        private new Camera camera;
        public Animator animator;
        private bool isFiring;
        public AimLine aimLine;
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
        public void HandleMovement()
        {
            var x = Input.GetAxis("Horizontal");
            var z = Input.GetAxis("Vertical");
            var velocity = new Vector3(x, 0, z);
            velocity.y = -gravity;
            velocity = speed * velocity;
            cc.Move(velocity * Time.deltaTime);
            
            var localVelocity = transform.InverseTransformVector(velocity);
            animator.SetBool("IsRunning", x != 0 || z != 0);
            animator.SetFloat("SpeedX", localVelocity.x);
            animator.SetFloat("SpeedZ", localVelocity.z);
        }

        /// <summary>
        /// 转身朝向瞄准方向
        /// </summary>
        public void RotateTowardsAim()
        {
            // TODO 适配触屏摇杆
            // 根据鼠标位置来进行转向
            var ray = camera.ScreenPointToRay(Input.mousePosition);
            var isHit = Physics.Raycast(ray, out var hitInfo, float.PositiveInfinity, groundLayer);
            if (isHit)
            {
                var lookAt = hitInfo.point;
                lookAt.y = transform.position.y;
                transform.LookAt(lookAt); // TODO

                aimDirection = hitInfo.point - transform.position;
            }
            else
            {
                aimDirection = transform.forward;
            }
        }
        
        protected override void Die()
        {
            animator.SetTrigger("Dead");
            StartCoroutine(Util.Delay(deadDelay, () => { Destroy(gameObject); }));
        }
    }

}