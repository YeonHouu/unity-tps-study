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
                //���� �Է¹��� ���� ���� ���� ���ٸ� ����
                if (val.Equals(value)) return;
                val = value;
                //val�� ����� �� �˸� ����
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
