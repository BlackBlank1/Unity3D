using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Actor
{
    [SerializeField]
    private float gravity = 20f;

    [SerializeField]
    private float speed = 10f;

    [SerializeField]
    private float fireDelay = 0.1f;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private float deadDelay;

    [SerializeField]
    private float rayDistance = 10f;

    private CharacterController cc;
    private Gun gun;
    private new Camera camera;
    private Animator animator;
    private bool isFiring;
    private AimLine aimLine;

    protected override void Start()
    {
        base.Start();
        cc = GetComponent<CharacterController>();
        gun = GetComponentInChildren<Gun>();
        camera = Camera.main;
        animator = GetComponent<Animator>();
        aimLine = GetComponentInChildren<AimLine>();
    }

    void Update()
    {
        if (IsDead)
        {
            return;
        }

        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");
        var velocity = new Vector3(x, 0, z);
        velocity.y = -gravity;
        velocity = speed * velocity;
        cc.Move(velocity * Time.deltaTime);

        //a根据鼠标位置来进行转向
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        var isHit = Physics.Raycast(ray, out var hitInfo, float.PositiveInfinity, groundLayer);
        if (isHit)
        {
            var lookAt = hitInfo.point;
            lookAt.y = transform.position.y;
            transform.LookAt(lookAt);
        }

        if (Input.GetButtonDown("Fire1") && !isFiring)
        {
            StartCoroutine(Fire());
        }
        if (Input.GetButton("Fire2") || Input.GetButton("Fire1"))
        {
            var direction = hitInfo.point - transform.position;
            var endPoint = gun.firePoint.position + direction.normalized * rayDistance;
        }
        else
        {
            aimLine.Hide();
        }

        var localVelocity = transform.InverseTransformVector(velocity);

        animator.SetBool("IsRunning", x != 0 || z != 0);
        animator.SetFloat("SpeedX", localVelocity.x);
        animator.SetFloat("SpeedZ", localVelocity.z);
    }
    
    

    private IEnumerator Fire()
    {
        isFiring = true;
        animator.SetBool("IsFiring", true);
        yield return new WaitForSeconds(fireDelay);
        do
        {
            if (IsDead)
            {
                yield break;
            }

            gun.Fire(transform);
            yield return null;
        } while (Input.GetButton("Fire1"));

        animator.SetBool("IsFiring", false);
        isFiring = false;
    }

    // private IEnumerator Aiming()
    // {
    //     animator.SetBool("IsAiming", true);
    //     yield return WaitForSeconds(fireDelay);
    //     do
    //     {
    //         aimLine.Show(gun.firePoint.position, endPoint);
    //         yield return null;
    //     } while (Input.GetButton("Fire2"));
    // }

    protected override void Die()
    {
        animator.SetTrigger("Dead");
        StartCoroutine(Util.Delay(deadDelay, () => { Destroy(gameObject); }));
    }
}