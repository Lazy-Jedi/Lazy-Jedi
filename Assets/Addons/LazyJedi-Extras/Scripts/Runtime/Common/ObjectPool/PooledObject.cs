using System;
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
}