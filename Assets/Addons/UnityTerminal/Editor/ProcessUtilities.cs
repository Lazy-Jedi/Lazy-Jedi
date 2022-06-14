using System.Diagnostics;

namespace UnityTerminal
{
    public static class ProcessUtilities
    {
        /// <summary>
        /// Start a standard Process without Arguments
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="runAsAdmin"></param>
        public static void StartProcess(string filename, bool runAsAdmin = false)
        {
            using Process process = new Process();
            process.StartInfo.FileName = filename;
            if (runAsAdmin) process.StartInfo.Verb = "RunAs";
            process.Start();
        }

        /// <summary>
        /// Start and Advanced Process with Arguments
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="argument"></param>
        /// <param name="hideWindow"></param>
        /// <param name="runAsAdmin"></param>
        public static void StartAdvProcess(string filename, string argument, bool hideWindow = false, bool runAsAdmin = false)
        {
            using Process process = new Process();
            process.StartInfo.FileName = filename;
            if (runAsAdmin) process.StartInfo.Verb = "RunAs";
            if (hideWindow) process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.Arguments = argument;
            process.Start();
        }
    }
}