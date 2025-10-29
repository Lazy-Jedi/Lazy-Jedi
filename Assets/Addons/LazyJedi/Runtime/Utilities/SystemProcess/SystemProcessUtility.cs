using System.Diagnostics;

namespace LazyJedi.Utilities
{
    public static class SystemProcessUtility
    {
        /// <summary>
        /// Start a standard Process without Arguments
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="runAsAdmin"></param>
        public static void BasicProcess(string filename, bool runAsAdmin = false)
        {
            using Process process = new Process();
            process.StartInfo.FileName = filename;
            if (runAsAdmin) process.StartInfo.Verb = "RunAs";
            process.Start();
        }

        /// <summary>
        /// Start an Advanced Process with Arguments
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="argument"></param>
        /// <param name="hideWindow"></param>
        /// <param name="runAsAdmin"></param>
        public static void AdvancedProcess(string filename, string argument, bool hideWindow = false, bool runAsAdmin = false)
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