using System.Collections.Generic;
using UnityEngine;

namespace LazyJedi.Examples
{
    // The SerializeReference attribute, added in Unity 2019.3, makes it possible to serialize references to interfaces and abstract
    // classes.
    // The SubclassSelector attribute allows you to easily set subclasses of those abstract classes in the Editor that are serialized by SerializeReference attribute.
    public class SerializedReferenceExamples : MonoBehaviour
    {
        #region VARIABLES

        [Header("Command")]
        [SerializeReference, SubclassSelector]
        public ICommand Command;

        [Header("Commands")]
        [SerializeReference, SubclassSelector]
        public List<ICommand> Commands = new List<ICommand>();

        #endregion

        #region UNITY METHODS

        private void Start()
        {
            Command?.Execute();

            foreach (ICommand command in Commands)
            {
                command?.Execute();
            }
        }

        #endregion
    }
}