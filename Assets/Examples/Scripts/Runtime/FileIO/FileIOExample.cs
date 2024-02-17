using System.IO;
using LazyJedi.Common.Extensions;
using LazyJedi.IO;
using UnityEngine;

namespace LazyJedi.Examples
{
    public class FileIOExample : MonoBehaviour
    {
        #region FIELDS

        public bool Save = false;
        public bool Load = false;

        [Header("User Data")]
        public User User;
        public UserSO UserSO;

        [Header("AES Key and IV")]
        public byte[] Key;
        public byte[] IV;

        string userPath => Path.Combine(Application.persistentDataPath, "User.json");

        #endregion

        #region UNITY METHODS

        private void OnValidate()
        {
            if (Save)
            {
                Save = false;

                // IO Extensions - Short Hand Write to File Methods
                userPath.WriteText(User.ToJson());
                userPath.StreamWriter(User.ToJson());
                
                // Secure Data and Data IO Class - Saving Plain and Encrypted string Data
                DataIO.Save(User, filename: "userPlain", pathType: PathType.DefaultFolder, prettyPrint: true);
                SecureDataIO.Save(User, ref Key, ref IV, filename: "userSecure", pathType: PathType.DefaultFolder, prettyPrint: true);

                // Secure Data and Data IO Class - Save ScriptableObject as Plain and Encrypted string Data
                DataIO.Save(UserSO, filename: "userSOPlain", pathType: PathType.DefaultFolder, prettyPrint: true);
                SecureDataIO.Save(UserSO, ref Key, ref IV, filename: "userSOSecure", pathType: PathType.DefaultFolder, prettyPrint: true);

                // Secure Data and Data IO Class - Save to Slot Plain and Encrypted string Data
                DataIO.SaveToSlot(User, filename: "userPlain_1", slotIndex: 1, pathType: PathType.DefaultFolder, prettyPrint: true);
                SecureDataIO.SaveToSlot(User, ref Key, ref IV, filename: "userSecure_1", slotIndex: 1, pathType: PathType.DefaultFolder, prettyPrint: true);
            }

            if (Load)
            {
                Load = false;
                // IO Extensions - Short Hand Read from File Methods
                string userJson = userPath.ReadText();
                User = userJson.FromJson<User>();
                userJson = userPath.StreamReader();
                User = userJson.FromJson<User>();

                // Secure Data and Data IO Class - Loading Plain and Encrypted Data
                DataIO.Load<User>(filename: "userPlain", pathType: PathType.DefaultFolder);
                SecureDataIO.Load<User>(Key, IV, filename: "userSecure", pathType: PathType.DefaultFolder);

                // Secure Data and Data IO Class - Load and Overwrite ScriptableObject with Plain and Encrypted Data
                DataIO.LoadAndOverwrite(UserSO, filename: "userSOPlain", pathType: PathType.DefaultFolder);
                SecureDataIO.LoadAndOverwrite(UserSO, Key, IV, filename: "userSOSecure", pathType: PathType.DefaultFolder);

                // Secure Data and Data IO Class - Load From Slot Plain and Encrypted Data
                DataIO.LoadFromSlot<User>(filename: "userPlain_1", slotIndex: 1, pathType: PathType.DefaultFolder);
                SecureDataIO.LoadFromSlot<User>(Key, IV, filename: "userSecure_1", slotIndex: 1, pathType: PathType.DefaultFolder);
            }
        }

        #endregion
    }
}