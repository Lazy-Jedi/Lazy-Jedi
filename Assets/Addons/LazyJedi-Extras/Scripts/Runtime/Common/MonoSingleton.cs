using UnityEngine;

namespace LazyJedi.Common
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
            DoOnAwake();
        }

        #endregion

        #region DO METHODS

        protected virtual void DoOnAwake()
        {
        }

        #endregion
    }

    /// <summary>
    /// A base class for creating a simple singleton MonoBehaviour. <br/>
    /// Your singleton will not be created if it does not exist. <br/>
    /// You will need to create your singleton manually by inheriting this class. <br/>
    /// Then attach your singleton to a GameObject.
    /// </summary>
    public abstract class SimpleMonoSingleton<T> : MonoBehaviour where T : SimpleMonoSingleton<T>
    {
        #region FIELDS

        [Header("Singleton Settings")]
        public bool DoNotDestroyOnLoad = true;
        public static T Instance;

        #endregion

        #region UNITY METHODS

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = (T)this;
                if (DoNotDestroyOnLoad)
                {
                    DontDestroyOnLoad(gameObject);
                }
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

    /// <summary>
    /// The base class for creating a singleton class. <br/>
    /// These are for non-MonoBehaviour classes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Singleton<T> where T : class, new()
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }
                _instance ??= new T();
                return _instance;
            }
        }
    }
}