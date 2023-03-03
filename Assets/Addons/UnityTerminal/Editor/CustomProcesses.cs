#if UNITY_EDITOR
using System.IO;
using System.Threading.Tasks;
using LazyJedi.Editors.Internal;
using UnityEditor;

namespace UnityTerminal
{
    /// <summary>
    /// You can add your custom Command Prompt Processes here with a MenuItem Attribute.<br/>
    ///<br/>
    /// For simple processes use the ProcessUtilities.StartProcess(Filename, (optional) runAsAdmin)<br/>
    /// <br/>
    /// For advanced processes use the ProcessUtilities.StartAdvProcess(Filename, (optional) arguments, (optional) hideWindow, (optional) runAsAdmin)<br/>
    ///<br/>
    /// </summary>
    public static class CustomProcesses
    {
        #region PYTHON

        [MenuItem("Window/Python/Python Shell")]
        public static async void OpenPythonShell()
        {
            await Task.Run(() => ProcessUtilities.StartProcess("python", true));
        }

        [MenuItem("Window/Python/IDLE")]
        public static async void OpenPythonIdle()
        {
            await Task.Run(() => ProcessUtilities.StartAdvProcess("python", @"-m idlelib", true));
        }

        #endregion

        #region PERSONAL

        [MenuItem("Lazy-Jedi/Open/Resources Folder %#O", priority = 200)]
        public static void OpenPersonalResourcesFolder()
        {
            ProcessUtilities.StartAdvProcess("explorer.exe", new ProjectSetup().LoadSettings().ResourcesFolder.Replace("/", "\\"));
        }

        [MenuItem("Lazy-Jedi/Open/Temporary Folder %#T", priority = 200)]
        public static void OpenTemporaryFolder()
        {
            string path = new ProjectSetup().LoadSettings().TemporaryFolder;
            ProcessUtilities.StartAdvProcess("explorer.exe", Path.Combine(string.IsNullOrEmpty(path) ? LazyEditorStrings.DEFAULT_TEMPORARY_PATH : path));
        }

        #endregion
    }
}

#endif