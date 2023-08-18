using UnityEngine;

namespace LazyJedi.DesignPatterns.Creational
{
    /// <summary>
    /// A base class for creating a simple singleton MonoBehaviour. <br/>
    /// Your singleton will not be created if it does not exist. <br/>
    /// You will need to create your singleton and add it to your scene after inheriting from this class <br/>
    /// Then attach your singleton to a GameObject.
    /// </summary>
    public abstract class SMonoSingleton<T> : MonoBehaviour where T : SMonoSingleton<T>
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
}