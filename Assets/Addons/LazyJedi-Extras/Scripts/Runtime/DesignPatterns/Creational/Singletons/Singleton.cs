using UnityEngine;

namespace LazyJedi.Patterns.Singletons
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        #region FIELDS

        protected static T _instance;

        #endregion

        #region PROPERTIES

        public static bool HasInstance => _instance != null;

        public static T TryGetInstance() => HasInstance ? _instance : null;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<T>();
                    if (_instance == null)
                    {
                        var go = new GameObject(typeof(T).Name + " Auto-Generated");
                        _instance = go.AddComponent<T>();
                    }
                }

                return _instance;
            }
        }

        #endregion

        #region UNITY METHODS

        /// <summary>
        /// Make sure to call base.Awake() in override if you need awake.
        /// </summary>
        protected virtual void Awake()
        {
            InitializeSingleton();
        }

        protected virtual void InitializeSingleton()
        {
            if (!Application.isPlaying) return;

            _instance = this as T;
        }

        #endregion
    }
}