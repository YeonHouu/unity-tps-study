using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DesignPattern
{
    // 추상클래스는 MonoBehaviour를 상속 받아도 컴포넌트가 될 수 없음
    // 하지만 상속 받은 클래스는 컴포넌트 가능
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
