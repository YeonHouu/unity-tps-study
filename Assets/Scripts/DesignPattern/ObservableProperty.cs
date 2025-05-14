using UnityEngine;
using UnityEngine.Events;

namespace DesignPattern
{
    public class ObservableProperty<T>
    {
        [SerializeField] private T val;

        public T Val
        {
            get => val;
            set
            {
                //새로 입력받은 값이 이전 값과 같다면 리턴
                if (val.Equals(value)) return;
                val = value;
                //val이 변경될 때 알림 전송
                Notify();
            }
        }

        private UnityEvent<T> onValueChanged = new();

        public ObservableProperty(T value = default)
        {
            val = value;
        }

        public void Subscribe(UnityAction<T> action)
        {
            onValueChanged.AddListener(action);
        }
        public void UnSubscribe(UnityAction<T> action)
        {
            onValueChanged.RemoveListener(action);
        }

        public void UnSubscribeAll()
        {
            onValueChanged.RemoveAllListeners();
        }

        private void Notify()
        {
            onValueChanged?.Invoke(Val);
        }
    }
}
