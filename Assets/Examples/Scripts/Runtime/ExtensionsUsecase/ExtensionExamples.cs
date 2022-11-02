using System.Collections.Generic;
using LazyJedi.Extensions;
using UnityEngine;

namespace LazyJedi.Examples
{
    public class ExtensionExamples : MonoBehaviour
    {
        #region VARIABLES

        [Header("List Example")]
        public List<string> WordsList = new List<string>()
        {
            "Hello",
            "World!",
            "Simple",
            "Shuffle"
        };

        [Header("Array Example")]
        public string[] WordsArray = new[]
        {
            "Hello",
            "World!",
            "Simple",
            "Shuffle"
        };

        [Header("GameObject")]
        public bool DestroyYourObject = false;
        public GameObject YourObject;

        [Header("Parents")]
        public Transform YourParent;
        public Transform ParentWithChildren;

        [Header("Layer Masks")]
        public LayerMask LayerMaskA;
        public LayerMask LayerMaskB;

        #endregion

        #region UNITY METHODS

        private void Start()
        {
            ArraysAndListsExtensions();

            UnityObjectExtensions();
            GameObjectExtensions();
            UnityObjectExtensions();

            StringExtensions();

            TransformExtensions();

            LayerMaskExtensions();
        }

        #endregion

        #region METHODS

        private void ArraysAndListsExtensions()
        {
            WordsList.Shuffle();
            WordsArray.Shuffle();
        }

        private void UnityObjectExtensions()
        {
            if (YourObject.IsNull())
            {
                print("Your Object is Null!");
            }

            if (YourObject.IsNotNull())
            {
                print("Your Object is Not Null!");
            }
        }

        private void StringExtensions()
        {
            string word = "Hello, World!";

            string word64 = word.ToBase64();
            string from64 = word64.FromBase64();

            byte[] wordBytes = word.ToBytes();
            string fromBytes = wordBytes.FromBytes();

            print($"Base64 - {word64}");
            print($"From Base 64 - {from64}");

            print($"Word Bytes Length - {wordBytes.Length}");
            print($"From Bytes - {fromBytes}");

            if (word.IsNull())
            {
                print("Word is Null");
            }

            if (word.IsNotNull())
            {
                print("Word is Not Null");
            }
        }

        private void GameObjectExtensions()
        {
            YourObject.Deactivate();
            YourObject.Activate();

            YourObject.SetParent(YourParent);

            Transform parent = YourObject.GetParent();
            print($"Your Objects Parent - {parent.name}");

            GameObject goParent = YourObject.GetParentGo();
            print($"Your Objects Parent GameObject - {goParent.name}");

            GameObject clone = YourObject.Clone();
            print($"Your Objects Clone - {clone.name}");

            if (DestroyYourObject) YourObject.Destroy();
        }

        private void TransformExtensions()
        {
            // Clone Parent
            Transform clonedParentWithChildren = ParentWithChildren.Clone();
            print("Cloned Parent With Children");

            // Change Layer Masks of Children that have Colliders
            clonedParentWithChildren.SetColliderInteractionLayers(LayerMask.LayerToName(LayerMaskA));
            print("Set Collider Interaction Layer");

            // Destroy All Children
            ParentWithChildren.DestroyAllChildren();
            print("Delete Original Parent With Children");
        }

        private void LayerMaskExtensions()
        {
            print($"Is Layer Mask A, in Layer Mask B - {LayerMaskB.InLayerMask(LayerMaskA)}");
        }

        #endregion
    }
}