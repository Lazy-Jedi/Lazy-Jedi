using System;
using System.Collections;
using System.Collections.Generic;
using LazyJedi.Extensions;
using UnityEngine;

namespace LazyJedi
{
    public class NewBehaviourScript : MonoBehaviour
    {
        #region FIELDS

        public bool Button;

        public float Time = 1800;

        #endregion

        #region UNITY METHODS

        private void OnValidate()
        {
            if (Button) Button = false;
            print($"Time : {Time.ToTimeHMS()}");
        }

        #endregion

        #region METHODS

        #endregion
    }
}