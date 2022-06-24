using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace LazyJedi.Examples
{
    public interface ICommand
    {
        #region METHODS

        void Execute();

        #endregion
    }

    [Serializable]
    public class DebugCommand : ICommand
    {
        #region VARIABLES

        public string Message = string.Empty;

        #endregion

        #region METHODS

        public void Execute()
        {
            Debug.Log(Message);
        }

        #endregion
    }


    [Serializable]
    public class CreatorCommand : ICommand
    {
        #region VARIABLES

        public Object Prefab;

        #endregion

        #region METHODS

        public void Execute()
        {
            Object.Instantiate(Prefab, new Vector3(Random.value * 5f, Random.value * 5f, 0f), Quaternion.identity);
        }

        #endregion
    }

    [AddTypeMenu("Examples/Execute Command")]
    [Serializable]
    public class AddTypeMenuCommand : ICommand
    {
        public string Message;

        public void Execute()
        {
            Debug.Log(Message);
        }
    }
}