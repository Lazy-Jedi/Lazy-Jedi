using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor.Events;
#endif

/*
 * Observer class is a generic class that can be used to observe changes in a value.
 * https://gist.github.com/adammyhre/353195d4870e8fd0cc0028659e66f208
 */

namespace LazyJedi.Common.Observers
{
    [Serializable]
    public class Observer<T>
    {
        [SerializeField]
        T _value;
        [SerializeField]
        UnityEvent<T> _onValueChanged;

        public T Value
        {
            get => _value;
            set => Set(value);
        }

        public static implicit operator T(Observer<T> observer) => observer._value;

        public Observer(T value, UnityAction<T> callback = null)
        {
            _value = value;
            _onValueChanged = new UnityEvent<T>();
            if (callback != null) _onValueChanged.AddListener(callback);
        }

        public void Set(T value)
        {
            if (Equals(_value, value)) return;
            _value = value;
            Invoke();
        }

        public void Invoke()
        {
            Debug.Log($"Invoking {_onValueChanged.GetPersistentEventCount()} listeners");
            _onValueChanged.Invoke(_value);
        }

        public void AddListener(UnityAction<T> callback)
        {
            if (callback == null) return;
            if (_onValueChanged == null) _onValueChanged = new UnityEvent<T>();

#if UNITY_EDITOR
            UnityEventTools.AddPersistentListener(_onValueChanged, callback);
#else
        onValueChanged.AddListener(callback);
#endif
        }

        public void RemoveListener(UnityAction<T> callback)
        {
            if (callback == null) return;
            if (_onValueChanged == null) return;

#if UNITY_EDITOR
            UnityEventTools.RemovePersistentListener(_onValueChanged, callback);
#else
        onValueChanged.RemoveListener(callback);
#endif
        }

        public void RemoveAllListeners()
        {
            if (_onValueChanged == null) return;

#if UNITY_EDITOR
            FieldInfo fieldInfo = typeof(UnityEventBase).GetField("m_PersistentCalls", BindingFlags.Instance | BindingFlags.NonPublic);
            object value = fieldInfo.GetValue(_onValueChanged);
            value.GetType().GetMethod("Clear").Invoke(value, null);
#else
        onValueChanged.RemoveAllListeners();
#endif
        }

        public void Dispose()
        {
            RemoveAllListeners();
            _onValueChanged = null;
            _value = default;
        }
    }
}