using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DesignPattern
{
    // MonoBehaviour�� ��ӹ޾ƾ� �Ѵ�
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private Stack<T> stack;
        private T targetPrefab;
        private GameObject poolObject;

        public ObjectPool(T targetPrefab, int initSize = 5) => Init(targetPrefab, initSize);
       
        private void Init(T targetPrefab, int initSize)
        {
            stack = new Stack<T>(initSize);
            this.targetPrefab = targetPrefab;
            poolObject = new GameObject($"{targetPrefab.name} Pool");

            for (int i = 0; i < initSize; i++)
            {
                // ������ Instantiate
                CreatePoolObject();
            }
        }

        public T Get()
        {
            // Ǯ�� ������Ʈ�� ���ٸ�
            if(stack.Count == 0)
            {
                // ���� ����
                CreatePoolObject();
            }

            T pooledObject = stack.Pop();
            pooledObject.gameObject.SetActive(true);
            return pooledObject;
        }

        public void ReturnPool(T target)
        {
            // Ǯ�� ���� ���� ������Ʈ�� ���� ������Ʈ�� ���� (�ð������� ��Ÿ���� ����)
            target.transform.parent = poolObject.transform;
            
            target.gameObject.SetActive(false);
            stack.Push(target);
        }

        private void CreatePoolObject()
        {
            T obj = MonoBehaviour.Instantiate(targetPrefab);
            ReturnPool(obj);
        }
    }
}
