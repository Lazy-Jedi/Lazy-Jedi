using LazyJedi.Extensions;
using LazyJedi.IO;
using UnityEngine;

namespace LazyJedi.Examples
{
    public class IOTest : MonoBehaviour
    {
        #region FIELDS

        [Header("Debug")]
        public bool SaveButton = false;
        public bool LoadButton = false;

        [Header("Serializable Data")]
        public Data Data;
        public Data DataSlot1;
        public DataSO DataSO;

        #endregion

        #region UNITY METHODS

        private void OnValidate()
        {
            if (SaveButton)
            {
                SaveButton = false;
                LazyDataIO.Save(Data);
                DataSO.Save();

                LazyDataIO.SaveToSlot(DataSlot1, 1);
            }

            if (LoadButton)
            {
                LoadButton = false;
                Data = LazyDataIO.Load<Data>();
                DataSlot1 = LazyDataIO.LoadFromSlot<Data>(1);
                DataSO.Overwrite();
            }
        }

        #endregion
    }
}