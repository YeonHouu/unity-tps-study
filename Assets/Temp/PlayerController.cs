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

            // 몸체의 회전 기능 구현 후 호출
            
        }
    }
}
