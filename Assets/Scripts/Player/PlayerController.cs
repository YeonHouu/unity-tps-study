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

    // �ʱ�ȭ�� �Լ�, ��ü ������ �ʿ��� �ʱ�ȭ �۾��� �ִٸ� ���⼭ ����
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
        // (ȸ�� ���� ��)�¿� ȸ���� ���� ���� ��ȯ
        Vector3 camRotateDir = movement.SetAimRotation();

        float moveSpeed;
        if (status.IsAiming.Val) moveSpeed = status.WalkSpeed;
        else moveSpeed = status.RunSpeed;

        Vector3 moveDir = movement.SetMove(moveSpeed);
        // ���Ͱ� ���ΰ� �ƴ϶�� isMoving�� ����
        status.IsMoving.Val = (moveDir != Vector3.zero);

        Vector3 avatarDir;
        // ī�޶� ���̹� ���̸� ī�޶�ȸ�� ����
        if (status.IsAiming.Val) avatarDir = camRotateDir;
        //�����̴� �������� ��ȯ
        else avatarDir = moveDir;

        movement.SetAvatarRotation(avatarDir);

        //SetAnimationParameter
        //status.IsAiming.Val�� true �� ��
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

