#if UNITY_EDITOR
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
        #region PROPERTIES

        private static string ResourcesFolder
        {
            get
            {
                ProjectSetup projectSetup = new ProjectSetup();
                projectSetup.LoadSettings();
                return projectSetup.ResourcesFolder.Replace("/", "\\");
            }
        }

        #endregion

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
            ProcessUtilities.StartAdvProcess("explorer.exe", ResourcesFolder);
        }

        #endregion
    }
}

#endif