using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DesignPattern
{
    // �߻�Ŭ������ MonoBehaviour�� ��� �޾Ƶ� ������Ʈ�� �� �� ����
    // ������ ��� ���� Ŭ������ ������Ʈ ����
    public abstract class PooledObject : MonoBehaviour
    {
        public ObjectPool ObjPool { get; private set; }

        public void PooledInit(ObjectPool objPool)
        {
            ObjPool = objPool;
        }    

        public void ReturnPool()
        {
            ObjPool.ReturnPool(this);
        }
    }
}
