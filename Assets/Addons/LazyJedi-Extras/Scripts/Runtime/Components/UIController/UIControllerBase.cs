using System.Collections;
using UnityEngine;

namespace LazyJedi.Components
{
    public abstract class UIControllerBase : MonoBehaviour
    {
        #region VARIABLES

        [Header("Current Canvas")]
        public Canvas CurrentCanvas;

        [Header("Canvas Delay Properties")]
        public float ActivateDelay = 1f;

        private UIControllerBase _callingController;

        #endregion

        #region PROPERTIES

        public UIControllerBase CallingController
        {
            get => _callingController;
            set
            {
                _callingController = value;
                ActivateCanvas();
                _callingController.DeactivateCanvas();
            }
        }

        #endregion

        #region UNITY METHODS

        public virtual void Awake()
        {
            if (!CurrentCanvas) CurrentCanvas = GetComponent<Canvas>();
        }

        #endregion

        #region CANVAS STATE METHODS

        public virtual void ActivateCanvas()
        {
            CurrentCanvas.enabled = true;
        }

        public virtual void ActivateCanvas(UIControllerBase callingController)
        {
            CallingController = callingController;
        }

        public virtual void ActivateCanvasDelay()
        {
            StartCoroutine(ActivateCanvasDelayRoutine());
        }

        public virtual IEnumerator ActivateCanvasDelayRoutine()
        {
            WaitForSeconds delay = new WaitForSeconds(ActivateDelay);
            yield return delay;
            CurrentCanvas.enabled = true;
        }

        public virtual void DeactivateCanvas()
        {
            CurrentCanvas.enabled = false;
        }

        #endregion

        #region GAMEOBJECT STATE METHODS

        /// <summary>
        /// Activate Object
        /// </summary>
        public void ActivateGO()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Deactivate Object
        /// </summary>
        public virtual void DeactivateGO()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Destroy Object
        /// </summary>
        public virtual void DestroyGO()
        {
            Destroy(gameObject);
        }

        #endregion

        #region DO METHODS

        /// <summary>
        /// Do anything required to be called during Awake here.
        /// Example:
        /// Initialization of Objects, Fields and Properties
        /// Subscribe to Events
        /// </summary>
        public virtual void DoOnAwake()
        {
        }

        /// <summary>
        /// Do anything required to be called during OnEnable here.
        /// Example:
        /// Reset Properties, Objects and Fields
        /// Subscribe to Events
        /// </summary>
        public virtual void DoOnEnable()
        {
        }

        /// <summary>
        /// Do anything required to be called during Start here.
        /// Example:
        /// Initialize Objects
        /// Subscribe to Events
        /// DontDestroyOnLoad
        /// </summary>
        public virtual void DoOnStart()
        {
        }

        /// <summary>
        /// Do anything required to be called during OnDisable here.
        /// Example:
        /// Unsubscribe from Events
        /// </summary>
        public virtual void DoOnDisable()
        {
        }

        /// <summary>
        /// Do anything required to be called during OnDestroy here.
        /// Example:
        /// Dispose of any Objects
        /// </summary>
        public virtual void DoOnDestroy()
        {
        }

        #endregion
    }
}