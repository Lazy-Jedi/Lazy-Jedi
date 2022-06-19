#if UNITY_EDITOR
using LazyJedi.Editors.Internal;
using System.IO;
using UnityEditor;
using SevenZipExtractor;
using UnityEngine;

namespace LazyJedi.Editors.Tools
{
    public static class ExtractArchive
    {
        #region VARIABLES

        private static string _7ZipLibrary = "Assets/Plugins/7zip/7z.dll";

        #endregion

        #region METHODS

        [MenuItem("Assets/Archive/Extract Files", false, 19)]
        public static void ExtractFiles()
        {
            string inPath = EditorSelection.GetSelectedFilePath();
            string outPath = EditorUtility.OpenFolderPanel("Select Folder", Path.GetDirectoryName(inPath), "");

            if (string.IsNullOrEmpty(outPath))
            {
                Debug.Log("No Folder Selected.");
                return;
            }

            using (ArchiveFile archiveFile = new ArchiveFile(Path.GetFullPath(inPath), _7ZipLibrary))
            {
                archiveFile.Extract(Path.GetFullPath(outPath));
            }

            AssetDatabase.Refresh();
        }

        [MenuItem("Assets/Archive/Extract Here", false, 19)]
        public static void ExtractHere()
        {
            string inPath = EditorSelection.GetSelectedFilePath();
            string outPath = EditorSelection.GetSelectedFolderPath();

            using (ArchiveFile archiveFile = new ArchiveFile(Path.GetFullPath(inPath), _7ZipLibrary))
            {
                archiveFile.Extract(Path.GetFullPath(outPath));
            }

            AssetDatabase.Refresh();
        }

        [MenuItem("Assets/Archive/Extract to Folder", false, 19)]
        public static void ExtractToFolder()
        {
            CleanFilePaths(out string inPath, out string outPath);
            using (ArchiveFile archiveFile = new ArchiveFile(Path.GetFullPath(inPath), _7ZipLibrary))
            {
                archiveFile.Extract(Path.GetFullPath(outPath));
            }

            AssetDatabase.Refresh();
        }


        private static void CleanFilePaths(out string inPath, out string outPath)
        {
            outPath = string.Empty;
            inPath = EditorSelection.GetSelectedFilePath();
            if (string.IsNullOrEmpty(inPath)) return;

            outPath = $"{Path.GetDirectoryName(inPath)}{Path.DirectorySeparatorChar}{Path.GetFileNameWithoutExtension(inPath)}";
            if (!Directory.Exists(outPath))
            {
                Directory.CreateDirectory(outPath);
            }
        }

        #endregion
    }
}
#endif