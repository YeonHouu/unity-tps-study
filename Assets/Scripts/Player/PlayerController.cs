using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public bool IsControlActivate { get; set; } = true;

    private PlayerStatus status;
    private PlayerMovement movement;
    private Animator animator;

    [SerializeField] private CinemachineVirtualCamera aimCamera;
    [SerializeField] private Gun gun;

    [SerializeField] private KeyCode aimKey = KeyCode.Mouse1;
    [SerializeField] private KeyCode shootKey = KeyCode.Mouse0;

    private void Awake() => Init();
    private void OnEnable() => SubscribeEvents();
    private void Update() => HandlePlayerControl();
    private void OnDisable() => UnsubscribeEvents();

    // 초기화용 함수, 객체 생성시 필요한 초기화 작업이 있다면 여기서 수행
    private void Init()
    {
        status = GetComponent<PlayerStatus>();
        movement = GetComponent<PlayerMovement>();
        //mainCamera = Camera.main.gameObject;
        animator = GetComponent<Animator>();
    }

    private void HandlePlayerControl()
    {
        if (!IsControlActivate) return;

        HandleMovement();
        HandleAiming();
        HandleShooting();
    }

    private void HandleShooting()
    {
        if( status.IsAiming.Val && Input.GetKey(shootKey))
        {
            status.IsAttacking.Val = gun.Shoot();
        }
        else
        {
            status.IsAttacking.Val = false;
        }
    }

    private void HandleMovement()
    {
        // (회전 수행 후)좌우 회전에 대한 벡터 반환
        Vector3 camRotateDir = movement.SetAimRotation();

        float moveSpeed;
        if (status.IsAiming.Val) moveSpeed = status.WalkSpeed;
        else moveSpeed = status.RunSpeed;

        Vector3 moveDir = movement.SetMove(moveSpeed);
        // 벡터가 제로가 아니라면 isMoving에 대입
        status.IsMoving.Val = (moveDir != Vector3.zero);

        Vector3 avatarDir;
        // 카메라 에이밍 중이면 카메라회전 방향
        if (status.IsAiming.Val) avatarDir = camRotateDir;
        //움직이는 방향으로 변환
        else avatarDir = moveDir;

        movement.SetAvatarRotation(avatarDir);

        //SetAnimationParameter
        //status.IsAiming.Val가 true 일 때
        if (status.IsAiming.Val)
        {
            Vector3 input = movement.GetInputDirection();
            animator.SetFloat("X", input.x);
            animator.SetFloat("Z", input.z);
        }
    }

    private void HandleAiming()
    {
        status.IsAiming.Val = Input.GetKey(aimKey);
    }

    public void SubscribeEvents()
    {
        status.IsMoving.Subscribe(SetMoveAnimation);

        status.IsAiming.Subscribe(aimCamera.gameObject.SetActive);
        status.IsAiming.Subscribe(SetAimAnimation);
        status.IsAttacking.Subscribe(SetAttackAnimation);
    }

    public void UnsubscribeEvents()
    {
        status.IsMoving.UnSubscribe(SetMoveAnimation);

        status.IsAiming.UnSubscribe(aimCamera.gameObject.SetActive);
        status.IsAiming.UnSubscribe(SetAimAnimation);
        status.IsAttacking.UnSubscribe(SetAttackAnimation);
    }

    private void SetAimAnimation(bool value) => animator.SetBool("IsAim", value);
    private void SetMoveAnimation(bool value) => animator.SetBool("IsMove", value);
    private void SetAttackAnimation(bool value) => animator.SetBool("IsAttack", value);
}

