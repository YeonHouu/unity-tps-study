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

            //IsAiming ����� �׽�Ʈ �ڵ�
            status.IsAiming.Val = Input.GetKey(KeyCode.Mouse1);
        }

        public void MoveTest()
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
        }
    }
}
