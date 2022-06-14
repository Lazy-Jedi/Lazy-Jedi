#if UNITY_EDITOR
using System.Threading.Tasks;
using UnityEditor;

namespace UnityTerminal
{
    public static class WindowsCommandPrompt
    {
        #region METHODS

        [MenuItem("System/Command Prompt/cmd.exe %#l")]
        public static async Task OpenCmdAsNonAdmin()
        {
            await Task.Run(() => ProcessUtilities.StartProcess(@"C:\windows\system32\cmd.exe"));
        }

        [MenuItem("System/Command Prompt/cmd.exe (admin) %&l")]
        public static async Task OpenCmdAsAdmin()
        {
            await Task.Run(() => ProcessUtilities.StartProcess(@"C:\windows\system32\cmd.exe", true));
        }

        [MenuItem("System/Powershell/powershell.exe %#h")]
        public static async Task OpenPowershellAsNonAdmin()
        {
            await Task.Run(() => ProcessUtilities.StartProcess("powershell.exe"));
        }

        [MenuItem("System/Powershell/powershell.exe (admin) %&h")]
        public static async Task OpenPowershellAsAdmin()
        {
            await Task.Run(() => ProcessUtilities.StartProcess("powershell.exe", true));
        }

        #endregion
    }
}
#endif