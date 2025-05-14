using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ĳ������ ȸ��/�̵� ���
public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform avatar;
    [SerializeField] private Transform aim;

    private Rigidbody rigid;
    private PlayerStatus playerStatus;

    [Header("Mouse Config")]
    // ���� ���� �ּ�/�ִ� ���� ����
    [SerializeField][Range(-90, 0)] private float minPitch;
    [SerializeField][Range(0, 90)] private float maxPitch;
    [SerializeField][Range(0, 5)] private float mouseSensitivity = 1;

    private Vector2 currentRotation;

    private void Awake() => Init();

    private void Init()
    {
        rigid = GetComponent<Rigidbody>();
        playerStatus = GetComponent<PlayerStatus>();
    }

    public Vector3 SetMove(float moveSpeed)
    {
        //PlayerCharacter �̵�����
        Vector3 moveDirection = GetMoveDirection();

        Vector3 velocity = rigid.velocity;
        velocity.x = moveDirection.x * moveSpeed;
        velocity.z = moveDirection.z * moveSpeed;
        
        rigid.velocity = velocity;

        return moveDirection;
    }

    public Vector3 SetAimRotation()
    {
        // x�� ������ ������ y�� ������ ������
        Vector2 mouseDir = GetMouseDirection();

        // x�� ���� �ʿ�x
        currentRotation.x += mouseDir.x;
        
        // y���� ���� ������ �ɾ ������Ŵ
        currentRotation.y = Mathf.Clamp(currentRotation.y + mouseDir.y, minPitch, maxPitch);

        // ĳ���� ������Ʈ�� ��쿡�� �¿� ȸ���� �ݿ�
        transform.rotation = Quaternion.Euler(0, currentRotation.x, 0);
        
        // ������ ��� ���� ȸ�� �ݿ�
        Vector3 currentEuler = aim.localEulerAngles;        //���� ������ ����
        // ���콺�� y�� �������� ����Ƽ �󿡼� x �� rotation
        aim.localEulerAngles = new Vector3(currentRotation.y, currentEuler.y, currentEuler.z);

        //ȸ�� ���� ���� ��ȯ
        Vector3 rotateDirVector = transform.forward;
        rotateDirVector.y = 0;
        return rotateDirVector.normalized;

    }

    public void SetBodyRotation()
    {

    }

    private Vector2 GetMouseDirection()
    {
        // ����, ���� ���� ��ȯ (=Vector2)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = -Input.GetAxis("Mouse Y") * mouseSensitivity;
        // Y���� �ݴ뿩�� - ���ؾ� ��

        return new Vector2(mouseX, mouseY);
    }

    public Vector3 GetMoveDirection()
    {
        Vector3 input = GetInputDirection();

        Vector3 direction =
            (transform.right * input.x) + 
            (transform.forward * input.z);

        return direction.normalized;

    }
    public Vector3 GetInputDirection()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        return new Vector3(x, 0, z);
    }
}
