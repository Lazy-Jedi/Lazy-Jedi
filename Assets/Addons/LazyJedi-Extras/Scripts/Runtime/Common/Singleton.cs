using UnityEngine;

namespace LazyJedi.Common
{
    /// <summary>
    /// Singleton class that can be inherited to create a singleton.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        #region FIELDS

        private static T _instance;

        #endregion

        #region PROPERTIES

        public static T Instance
        {
            get
            {
                
                if (_instance != null) return _instance;
                _instance = FindObjectOfType<T>();

                if (_instance != null) return _instance;

                GameObject singletonObject = new GameObject();
                _instance = singletonObject.AddComponent<T>();
                singletonObject.name = $"{typeof(T)} - [Singleton]";
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
        }

        #endregion
    }
}