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
        [Tooltip("Amount of time to wait before activating the Canvas")]
        public float ActivateTime = 1f;
        [Tooltip("Amount of time to wait before deactivating the Canvas")]
        public float DeactivateTime = 1f;

        private UIControllerBase _otherCanvas;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Reference to any Other Canvas
        /// </summary>
        public UIControllerBase OtherCanvas
        {
            get => _otherCanvas;
            set
            {
                _otherCanvas = value;
                ActivateCanvas();
                _otherCanvas.DeactivateCanvas();
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

        /// <summary>
        /// Activate the Current Canvas
        /// </summary>
        public virtual void ActivateCanvas()
        {
            CurrentCanvas.enabled = true;
        }

        /// <summary>
        /// Activate the Current Canvas and Deactivate Other Canvas
        /// </summary>
        /// <param name="otherCanvas"></param>
        public virtual void ActivateCanvas(UIControllerBase otherCanvas)
        {
            OtherCanvas = otherCanvas;
        }

        public virtual void ActivateCanvasDelay()
        {
            StartCoroutine(SetActiveCanvasRoutine(true, ActivateTime));
        }

        public virtual void DeactivateCanvas()
        {
            CurrentCanvas.enabled = false;
        }

        public virtual void DeactivateCanvasDelay()
        {
            StartCoroutine(SetActiveCanvasRoutine(false, ActivateTime));
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
        public virtual void DoOnAwake()
        {
        }

        /// <summary>
        /// Do anything required during OnEnable here.
        /// Example:
        /// Reset Properties, Objects and Fields
        /// Subscribe to Events
        /// </summary>
        public virtual void DoOnEnable()
        {
        }

        /// <summary>
        /// Do anything required during Start here.
        /// Example:
        /// Initialize Objects
        /// Subscribe to Events
        /// DontDestroyOnLoad
        /// </summary>
        public virtual void DoOnStart()
        {
        }

        /// <summary>
        /// Do anything required during OnDisable here.
        /// Example:
        /// Unsubscribe from Events
        /// </summary>
        public virtual void DoOnDisable()
        {
        }

        /// <summary>
        /// Do anything that is required during OnDestroy here.
        /// Example:
        /// Dispose of any Objects
        /// </summary>
        public virtual void DoOnDestroy()
        {
        }

        #endregion

        #region HELPER METHODS

        public virtual IEnumerator SetActiveCanvasRoutine(bool state, float time)
        {
            WaitForSeconds delay = new WaitForSeconds(time);
            yield return delay;
            CurrentCanvas.enabled = state;
        }

        #endregion
    }
}