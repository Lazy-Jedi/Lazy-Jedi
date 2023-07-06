using LazyJedi.IO;
using LazyJedi.Utility;
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

        public byte[] Key = null;
        public byte[] IV = null;

        #endregion

        #region UNITY METHODS

        private void OnValidate()
        {
            if (SaveButton)
            {
                SaveButton = false;
                // DataIO.Save(Data);
                // DataSO.Save();
                //
                // DataIO.SaveToSlot(DataSlot1, 1);
                // SecureDataIO.Save(Data);
                SecureDataIO.Save(Data, ref Key, ref IV);
            }

            if (LoadButton)
            {
                LoadButton = false;
                // Data = SecureDataIO.Load<Data>();
                Data = SecureDataIO.Load<Data>(Key, IV);
            }
        }

        #endregion
    }
}