using System;
using UnityEngine;

[Serializable]
public class NotifyValue<T>
{
    public delegate void ValueChanged(T prev, T next);
    public event ValueChanged OnValueChanged; // 값 변경 이벤트

    [SerializeField] private T value; // 실직적 값 변수

    public T Value // 값 프로퍼티
    {
        get => value;
        set
        {
            T before = this.value;
            this.value = value;
            if ((before == null && value != null) || !before.Equals(this.value)) // Null 확인 & 값 변경 체크
            {
                OnValueChanged?.Invoke(before, this.value); // 값 변경 이벤트 실행
            }
        }
    }
    
    public NotifyValue()
    {
        value = default(T);
    }

    public NotifyValue(T value)
    {
        this.value = value;
    }
}
