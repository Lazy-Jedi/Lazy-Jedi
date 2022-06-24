#if UNITY_EDITOR
using System.Threading.Tasks;
using UnityEditor;

namespace UnityTerminal
{
    public static class WindowsCommandPrompt
    {
        #region METHODS

        [MenuItem("Window/Command Prompt/cmd.exe %#l")]
        public static async void OpenCmdAsNonAdmin()
        {
            await Task.Run(() => ProcessUtilities.StartProcess(@"C:\windows\system32\cmd.exe"));
        }

        [MenuItem("Window/Command Prompt/cmd.exe (admin) %&l")]
        public static async void OpenCmdAsAdmin()
        {
            await Task.Run(() => ProcessUtilities.StartProcess(@"C:\windows\system32\cmd.exe", true));
        }

        [MenuItem("Window/Powershell/powershell.exe %#h")]
        public static async void OpenPowershellAsNonAdmin()
        {
            await Task.Run(() => ProcessUtilities.StartProcess("powershell.exe"));
        }

        [MenuItem("Window/Powershell/powershell.exe (admin) %&h")]
        public static async void OpenPowershellAsAdmin()
        {
            await Task.Run(() => ProcessUtilities.StartProcess("powershell.exe", true));
        }

        #endregion
    }
}
#endif