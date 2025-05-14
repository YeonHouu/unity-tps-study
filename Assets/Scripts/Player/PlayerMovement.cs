using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 캐릭터의 회전/이동 기능
public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform avatar;
    [SerializeField] private Transform aim;

    private Rigidbody rigid;
    private PlayerStatus playerStatus;

    [Header("Mouse Config")]
    // 수평선 기준 최소/최대 각도 설정
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
        //PlayerCharacter 이동방향
        Vector3 moveDirection = GetMoveDirection();

        Vector3 velocity = rigid.velocity;
        velocity.x = moveDirection.x * moveSpeed;
        velocity.z = moveDirection.z * moveSpeed;
        
        rigid.velocity = velocity;

        return moveDirection;
    }

    public Vector3 SetAimRotation()
    {
        // x는 가로의 움직임 y는 세로의 움직임
        Vector2 mouseDir = GetMouseDirection();

        // x축 제한 필요x
        currentRotation.x += mouseDir.x;
        
        // y축은 각도 제한을 걸어서 고정시킴
        currentRotation.y = Mathf.Clamp(currentRotation.y + mouseDir.y, minPitch, maxPitch);

        // 캐릭터 오브젝트의 경우에는 좌우 회전만 반영
        transform.rotation = Quaternion.Euler(0, currentRotation.x, 0);
        
        // 에임의 경우 상하 회전 반영
        Vector3 currentEuler = aim.localEulerAngles;        //현재 에임의 각도
        // 마우스의 y축 움직임은 유니티 상에서 x 축 rotation
        aim.localEulerAngles = new Vector3(currentRotation.y, currentEuler.y, currentEuler.z);

        //회전 방향 벡터 반환
        Vector3 rotateDirVector = transform.forward;
        rotateDirVector.y = 0;
        return rotateDirVector.normalized;

    }

    public void SetBodyRotation()
    {

    }

    private Vector2 GetMouseDirection()
    {
        // 가로, 세로 값만 반환 (=Vector2)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = -Input.GetAxis("Mouse Y") * mouseSensitivity;
        // Y값은 반대여서 - 곱해야 함

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
