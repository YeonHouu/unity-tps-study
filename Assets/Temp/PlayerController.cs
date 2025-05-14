using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CYH_Test
{
    public class PlayerController : MonoBehaviour
    {
        public PlayerMovement movement;
        public PlayerStatus status;

        private void Update()
        {
            MoveTest();

            //IsAiming 변경용 테스트 코드
            status.IsAiming.Val = Input.GetKey(KeyCode.Mouse1);
        }

        public void MoveTest()
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
        }
    }
}
