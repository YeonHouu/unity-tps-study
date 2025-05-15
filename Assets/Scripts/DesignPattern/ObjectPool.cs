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
                // ������ Instantiate
                CreatePoolObject();
            }
        }

        public PooledObject PopPool()
        {
            // Ǯ�� ������Ʈ�� ���ٸ�
            if(stack.Count == 0)
            {
                // ���� ����
                CreatePoolObject();
            }

            PooledObject pooledObject = stack.Pop();
            pooledObject.gameObject.SetActive(true);
            return pooledObject;
        }

        public void ReturnPool(PooledObject target)
        {
            // Ǯ�� ���� ���� ������Ʈ�� ���� ������Ʈ�� ���� (�ð������� ��Ÿ���� ����)
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
