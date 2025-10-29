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

                // Secure Data and Data IO Class - Save ScriptableObject as Plain and Encrypted string Data
                DataIO.Save(UserSO, filename: "userSOPlain", pathType: PathType.DefaultFolder, prettyPrint: true);

                // Secure Data and Data IO Class - Save to Slot Plain and Encrypted string Data
                DataIO.SaveToSlot(User, filename: "userPlain_1", slotIndex: 1, pathType: PathType.DefaultFolder, prettyPrint: true);
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

                // Secure Data and Data IO Class - Load and Overwrite ScriptableObject with Plain and Encrypted Data
                DataIO.LoadAndOverwrite(UserSO, filename: "userSOPlain", pathType: PathType.DefaultFolder);

                // Secure Data and Data IO Class - Load From Slot Plain and Encrypted Data
                DataIO.LoadFromSlot<User>(filename: "userPlain_1", slotIndex: 1, pathType: PathType.DefaultFolder);
            }
        }

        #endregion
    }
}