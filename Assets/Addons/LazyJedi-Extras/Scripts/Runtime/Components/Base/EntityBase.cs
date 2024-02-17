using LazyJedi.Common.Extensions;
using UnityEngine;

namespace LazyJedi.Components.Base
{
    public abstract class EntityBase : MonoBehaviour
    {
        #region STATE METHODS

        /// <summary>
        /// Activate this GameObject
        /// </summary>
        public virtual void Activate()
        {
            gameObject.Activate();
        }

        /// <summary>
        /// Deactivate this GameObject
        /// </summary>
        public virtual void Deactivate()
        {
            gameObject.Deactivate();
        }

        /// <summary>
        /// Destroy this GameObject
        /// </summary>
        public virtual void DestroySelf()
        {
            Destroy(gameObject);
        }

        /// <summary>
        /// Create a Clone of this Object
        /// </summary>
        public virtual Object Clone()
        {
            return Instantiate(this);
        }

        #endregion

        #region DO METHODS

        /// <summary>
        /// Do anything required to be called during Awake here.
        /// Example:
        /// Initialization of Objects, Fields and Properties
        /// Subscribe to Events
        /// </summary>
        protected virtual void DoOnAwake()
        {
        }

        /// <summary>
        /// Do anything required to be called during OnEnable here.
        /// Example:
        /// Reset Properties, Objects and Fields
        /// Subscribe to Events
        /// </summary>
        protected virtual void DoOnEnable()
        {
        }

        /// <summary>
        /// Do anything required to be called during Start here.
        /// Example:
        /// Initialize Objects
        /// Subscribe to Events
        /// DontDestroyOnLoad
        /// </summary>
        protected virtual void DoOnStart()
        {
        }

        /// <summary>
        /// Do anything required to be called during OnDisable here.
        /// Example:
        /// Unsubscribe from Events
        /// </summary>
        protected virtual void DoOnDisable()
        {
        }

        /// <summary>
        /// Do anything required to be called during OnDestroy here.
        /// Example:
        /// Dispose of any Objects
        /// </summary>
        protected virtual void DoOnDestroy()
        {
        }

        #endregion
    }
}