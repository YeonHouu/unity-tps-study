using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool IsControlActivate { get; set; } = true;

    private PlayerStatus status;
    private PlayerMovement movement;

    [SerializeField] private GameObject aimCamera;
    private GameObject mainCamera;

    [SerializeField] private KeyCode _aimKey = KeyCode.Mouse1;

    private void Awake() => Init();
    private void OnEnable() => SubscribeEvents();
    private void Update() => HandlePlayerControl();
    private void OnDisable() => UnsubscribeEvents();

    // �ʱ�ȭ�� �Լ�, ��ü ������ �ʿ��� �ʱ�ȭ �۾��� �ִٸ� ���⼭ ����
    private void Init()
    {
        status = GetComponent<PlayerStatus>();
        movement = GetComponent<PlayerMovement>();
        mainCamera = Camera.main.gameObject;
    }

    private void HandlePlayerControl()
    {
        if (!IsControlActivate) return;

        HandleMovement();
        HandleAiming();
    }

    private void HandleMovement()
    {
        // TODO: Movement ���ս� ��� �߰�����
    }

    private void HandleAiming()
    {
        status.IsAiming.Val = Input.GetKey(_aimKey);
    }

    public void SubscribeEvents()
    {
        status.IsAiming.Subscribe(value => SetActivateAimCamera(value));
    }

    public void UnsubscribeEvents()
    {
        status.IsAiming.UnSubscribe(value => SetActivateAimCamera(value));
    }

    private void SetActivateAimCamera(bool value)
    {
        aimCamera.SetActive(value);
        mainCamera.SetActive(!value);
    }
}

