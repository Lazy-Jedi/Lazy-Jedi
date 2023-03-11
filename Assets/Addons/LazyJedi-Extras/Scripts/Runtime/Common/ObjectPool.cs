using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace LazyJedi.Common
{
    public class PooledObject<T> : IDisposable where T : class
    {
        #region PROPERTIES

        public T PoolObject { get; }
        public IObjectPool<T> Pool { get; }

        #endregion

        #region CONSTRUCTORS

        public PooledObject(T value, IObjectPool<T> pool)
        {
            PoolObject = value;
            Pool = pool;
        }

        #endregion

        #region METHODS

        public void Dispose()
        {
            Pool.Release(PoolObject);
        }

        #endregion
    }

    public class ObjectPool<T> : IDisposable, IObjectPool<T> where T : class
    {
        #region FIELDS

        private readonly List<T> _pool;
        private readonly int _maxSize;

        private readonly Func<T> _onCreate;
        private readonly Action<T> _onGet;
        private readonly Action<T> _onRelease;
        private readonly Action<T> _onDestroy;

        private readonly bool _collectionCheck;

        #endregion

        #region PROPERTIES

        public int CountAll { get; private set; }
        public int CountActive => CountAll - CountInactive;
        public int CountInactive => _pool.Count;

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Creates a new ObjectPool
        /// </summary>
        /// <param name="onCreate"></param>
        /// <param name="onGet"></param>
        /// <param name="onRelease"></param>
        /// <param name="onDestroy"></param>
        /// <param name="collectionCheck"></param>
        /// <param name="defaultCapacity"></param>
        /// <param name="maxSize"></param>
        public ObjectPool(Func<T> onCreate, Action<T> onGet = null, Action<T> onRelease = null, Action<T> onDestroy = null,
            bool collectionCheck = false, int defaultCapacity = 10, int maxSize = 10000)
        {
            if (maxSize <= 0)
            {
                Debug.unityLogger.LogError("ObjectPool", "Max Size must be greater than 0.");
                return;
            }
            if (onCreate == null)
            {
                Debug.unityLogger.LogError("onCreate", $"A function to Create {typeof(T)} must be provided.");
                return;
            }

            _pool = new List<T>(defaultCapacity);
            _onCreate = onCreate;
            _onGet = onGet;
            _onRelease = onRelease;
            _onDestroy = onDestroy;
            _collectionCheck = collectionCheck;
            _maxSize = maxSize;
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Get an Item from the Pool.
        /// </summary>
        /// <returns></returns>
        public T Get()
        {
            T item;
            if (_pool.Count == 0)
            {
                item = _onCreate.Invoke();
                ++CountAll;
            }
            else
            {
                int lastIndex = _pool.Count - 1;
                // By removing the last item, we can avoid shifting the entire array.
                item = _pool[lastIndex];
                _pool.RemoveAt(lastIndex);
            }
            _onGet?.Invoke(item);
            return item;
        }

        /// <summary>
        /// DO NOT USE THIS METHOD! USE GetPoolObject INSTEAD!
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public UnityEngine.Pool.PooledObject<T> Get(out T value)
        {
            throw new Exception("PLEASE DO NOT USE THIS GET METHOD! USE GetPoolObject INSTEAD!");
        }

        /// <summary>
        /// Get a Pooled Object.
        /// The PooledObject has references to the Object Pool as well the Object that was retrieved from the Object Pool.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public PooledObject<T> GetPooledObject(out T value)
        {
            return new PooledObject<T>(value = Get(), this);
        }

        /// <summary>
        /// Return an Item to the Pool.
        /// </summary>
        /// <param name="element"></param>
        /// <exception cref="ArgumentException"></exception>
        public void Release(T element)
        {
            if (_collectionCheck && _pool.Contains(element))
            {
                Debug.unityLogger.LogError("ObjectPool", "Object already released to pool.");
                return;
            }

            _onRelease?.Invoke(element);
            if (CountInactive < _maxSize)
            {
                _pool.Add(element);
            }
            else
            {
                _onDestroy?.Invoke(element);
            }
        }

        /// <summary>
        /// Clear the Pool.
        /// </summary>
        public void Clear()
        {
            if (_onDestroy != null)
            {
                foreach (T item in _pool)
                {
                    _onDestroy(item);
                }
            }
            _pool.Clear();
            CountAll = 0;
        }

        /// <summary>
        /// Dispose the Pool.
        /// </summary>
        public void Dispose() => Clear();

        #endregion
    }
}