using UnityEngine;

namespace LazyJedi.DesignPatterns.Creational
{
    /// <summary>
    /// A base class for creating a singleton MonoBehaviour. <br/>
    /// The singleton will be created if it does not exist when calling <see cref="Instance"/>. <br/>
    /// </summary>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        #region FIELDS

        private static T _instance;

        #endregion

        #region PROPERTIES

        public static T Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }
                _instance = FindFirstObjectByType<T>();
                if (_instance != null)
                {
                    return _instance;
                }
                GameObject singletonObject = new GameObject();
                _instance = singletonObject.AddComponent<T>();
                singletonObject.name = $"[{typeof(T)}]";
                DontDestroyOnLoad(singletonObject);
                return _instance;
            }
        }

        #endregion

        #region UNITY METHODS

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = (T)this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            DoOnAwake();
        }

        #endregion

        #region DO METHODS

        protected virtual void DoOnAwake()
        {
        }

        #endregion
    }
}