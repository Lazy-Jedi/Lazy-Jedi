using System.Collections;
using UnityEngine;

namespace LazyJedi.Components
{
    public abstract class UIControllerBase : MonoBehaviour
    {
        #region VARIABLES

        [Header("Current Canvas")]
        [SerializeField]
        private Canvas _currentCanvas;

        [Header("Canvas Delay Properties")]
        [Tooltip("Amount of time to wait before activating the Canvas")]
        public float ActivateTime = 1f;
        [Tooltip("Amount of time to wait before deactivating the Canvas")]
        public float DeactivateTime = 1f;

        private UIControllerBase _otherUIController;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Reference to any Other Canvas
        /// </summary>
        public UIControllerBase OtherUIController
        {
            get => _otherUIController;
            set
            {
                _otherUIController = value;
                ActivateCanvas();
                _otherUIController.DeactivateCanvas();
            }
        }

        #endregion

        #region UNITY METHODS

        protected virtual void Awake()
        {
            if (!_currentCanvas)
            {
                _currentCanvas = GetComponent<Canvas>();
            }
        }

        #endregion

        #region CANVAS STATE METHODS

        /// <summary>
        /// Activate the Current Canvas
        /// </summary>
        public virtual void ActivateCanvas()
        {
            _currentCanvas.enabled = true;
        }

        /// <summary>
        /// Activate the Current Canvas and Deactivate Other Canvas
        /// </summary>
        /// <param name="otherCanvas"></param>
        public virtual void ActivateCanvas(UIControllerBase otherCanvas)
        {
            OtherUIController = otherCanvas;
        }

        public virtual void ActivateCanvasDelay()
        {
            StartCoroutine(SetActiveCanvasRoutine(true, ActivateTime));
        }

        public virtual void DeactivateCanvas()
        {
            _currentCanvas.enabled = false;
        }

        public virtual void DeactivateCanvasDelay()
        {
            StartCoroutine(SetActiveCanvasRoutine(false, DeactivateTime));
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
        /// Do anything required during Awake here.
        /// Example:
        /// Initialization of Objects, Fields and Properties
        /// Subscribe to Events
        /// </summary>
        protected virtual void DoOnAwake()
        {
        }

        /// <summary>
        /// Do anything required during OnEnable here.
        /// Example:
        /// Reset Properties, Objects and Fields
        /// Subscribe to Events
        /// </summary>
        protected virtual void DoOnEnable()
        {
        }

        /// <summary>
        /// Do anything required during Start here.
        /// Example:
        /// Initialize Objects
        /// Subscribe to Events
        /// DontDestroyOnLoad
        /// </summary>
        protected virtual void DoOnStart()
        {
        }

        /// <summary>
        /// Do anything required during OnDisable here.
        /// Example:
        /// Unsubscribe from Events
        /// </summary>
        protected virtual void DoOnDisable()
        {
        }

        /// <summary>
        /// Do anything that is required during OnDestroy here.
        /// Example:
        /// Dispose of any Objects
        /// </summary>
        protected virtual void DoOnDestroy()
        {
        }

        #endregion

        #region HELPER METHODS

        public virtual IEnumerator SetActiveCanvasRoutine(bool state, float time)
        {
            WaitForSeconds delay = new WaitForSeconds(time);
            yield return delay;
            _currentCanvas.enabled = state;
        }

        #endregion
    }
}