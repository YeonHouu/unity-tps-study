using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DesignPattern
{
    // MonoBehaviour를 상속받아야 한다
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
                // 프리팹 Instantiate
                CreatePoolObject();
            }
        }

        public T Get()
        {
            // 풀에 오브젝트가 없다면
            if(stack.Count == 0)
            {
                // 새로 생성
                CreatePoolObject();
            }

            T pooledObject = stack.Pop();
            pooledObject.gameObject.SetActive(true);
            return pooledObject;
        }

        public void ReturnPool(T target)
        {
            // 풀에 들어가는 순간 오브젝트의 하위 오브젝트로 생성 (시각적으로 나타내기 위함)
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
