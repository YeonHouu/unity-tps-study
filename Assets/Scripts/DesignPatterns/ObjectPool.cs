using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


namespace DesignPattern
{
    public class ObjectPool
    {
        private Stack<PooledObject> stack;
        private PooledObject targetPrefab;
        private GameObject poolObject;

        public ObjectPool(Transform parent, PooledObject targetPrefab, int initSize = 5) => Init(parent,targetPrefab, initSize);
       
        private void Init(Transform parent, PooledObject targetPrefab, int initSize)
        {
            stack = new Stack<PooledObject>(initSize);
            this.targetPrefab = targetPrefab;
            poolObject = new GameObject($"{targetPrefab.name} Pool");
            poolObject.transform.parent = parent;

            for (int i = 0; i < initSize; i++)
            {
                // 프리팹 Instantiate
                CreatePoolObject();
            }
        }

        public PooledObject PopPool()
        {
            // 풀에 오브젝트가 없다면
            if(stack.Count == 0)
            {
                // 새로 생성
                CreatePoolObject();
            }

            PooledObject pooledObject = stack.Pop();
            pooledObject.gameObject.SetActive(true);
            return pooledObject;
        }

        public void ReturnPool(PooledObject target)
        {
            // 풀에 들어가는 순간 오브젝트의 하위 오브젝트로 생성 (시각적으로 나타내기 위함)
            target.transform.parent = poolObject.transform;
            
            target.gameObject.SetActive(false);
            stack.Push(target);
        }

        private void CreatePoolObject()
        {
            PooledObject obj = MonoBehaviour.Instantiate(targetPrefab);
            obj.PooledInit(this);
            ReturnPool(obj);
        }
    }
}
