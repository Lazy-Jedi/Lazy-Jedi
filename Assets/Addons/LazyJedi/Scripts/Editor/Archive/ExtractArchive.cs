#if UNITY_EDITOR
using LazyJedi.Editors.Internal;
using System.IO;
using UnityEditor;
using SevenZipExtractor;

namespace LazyJedi.Editors.Archiver
{
    public static class ExtractArchive
    {
        #region VARIABLES

        private static string _7ZipLibrary = "Assets/Plugins/7zip/7z.dll";

        #endregion

        #region METHODS

        [MenuItem("Assets/Create/Archive/Extract to Folder", false, 89)]
        public static void ExtractArchiveToFolder()
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

            outPath =
                $"{Path.GetDirectoryName(inPath)}{Path.DirectorySeparatorChar}{Path.GetFileNameWithoutExtension(inPath)}";
            if (!Directory.Exists(outPath))
            {
                Directory.CreateDirectory(outPath);
            }
        }

        #endregion
    }
}
#endif