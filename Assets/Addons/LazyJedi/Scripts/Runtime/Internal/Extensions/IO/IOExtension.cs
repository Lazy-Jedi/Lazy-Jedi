using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace LazyJedi.Extensions
{
    public static class IOExtension
    {
        #region FILE IO

        /// <summary>
        /// Write text to file
        /// </summary>
        /// <param name="path">Valid file path that includes a file extension</param>
        /// <param name="data">String data</param>
        /// <param name="encodingType">Default Encoding used is UTF8</param>
        public static void WriteText(this string path, string data, EncodingType encodingType = EncodingType.UTF8)
        {
            if (!IsPathValid(path))
            {
                Debug.unityLogger.LogError("IO Exception", "Path is invalid");
                return;
            }
            File.WriteAllText(path, data, Enums.GetEncoding(encodingType));
        }

        /// <summary>
        /// Write bytes to file
        /// </summary>
        /// <param name="path">Valid file path that includes a file extension</param>
        /// <param name="data">byte[] data</param>
        public static void WriteBytes(this string path, byte[] data)
        {
            if (!IsPathValid(path))
            {
                Debug.unityLogger.LogError("IO Exception", "Path is invalid");
                return;
            }
            File.WriteAllBytes(path, data);
        }

        /// <summary>
        /// Write lines to file
        /// </summary>
        /// <param name="path">Valid file path that includes a file extension</param>
        /// <param name="data">String data</param>
        /// <param name="encodingType">Default Encoding used is UTF8</param>
        public static void WriteLines(this string path, IEnumerable<string> data, EncodingType encodingType = EncodingType.UTF8)
        {
            if (!IsPathValid(path))
            {
                Debug.unityLogger.LogError("IO Exception", "Path is invalid");
                return;
            }
            File.WriteAllLines(path, data, Enums.GetEncoding(encodingType));
        }

        /// <summary>
        /// Write text to file 
        /// </summary>
        /// <param name="path">Valid file path that includes a file extension</param>
        /// <param name="data">String data</param>
        /// <param name="encodingType">Default Encoding used is UTF8</param>
        public static async Task WriteTextAsync(this string path, string data, EncodingType encodingType = EncodingType.UTF8)
        {
            if (!IsPathValid(path))
            {
                Debug.unityLogger.LogError("IO Exception", "Path is invalid");
                return;
            }
            await File.WriteAllTextAsync(path, data, Enums.GetEncoding(encodingType));
        }

        /// <summary>
        /// Write bytes to file
        /// </summary>
        /// <param name="path">Valid file path that includes a file extension</param>
        /// <param name="data">byte[] data</param>
        public static async Task WriteBytesAsync(this string path, byte[] data)
        {
            if (!IsPathValid(path))
            {
                Debug.unityLogger.LogError("IO Exception", "Path is invalid");
                return;
            }
            await File.WriteAllBytesAsync(path, data);
        }

        /// <summary>
        /// Write lines to file
        /// </summary>
        /// <param name="path">Valid file path that includes a file extension</param>
        /// <param name="data">String data</param>
        /// <param name="encodingType">Default Encoding used is UTF8</param>
        public static async Task WriteLinesAsync(this string path, IEnumerable<string> data, EncodingType encodingType = EncodingType.UTF8)
        {
            if (!IsPathValid(path))
            {
                Debug.unityLogger.LogError("IO Exception", "Path is invalid");
                return;
            }
            await File.WriteAllLinesAsync(path, data, Enums.GetEncoding(encodingType));
        }

        /// <summary>
        /// Read text from file
        /// </summary>
        /// <param name="path">Valid file path that includes a file extension</param>
        /// <param name="encodingType">Default Encoding used is UTF8</param>
        /// <returns>Text read from file</returns>
        public static string ReadText(this string path, EncodingType encodingType = EncodingType.UTF8)
        {
            if (IsPathValid(path))
            {
                return File.ReadAllText(path, Enums.GetEncoding(encodingType));
            }
            Debug.unityLogger.LogError("IO Exception", "Path is invalid");
            return string.Empty;
        }

        /// <summary>
        /// Read byte[] Array from file
        /// </summary>
        /// <param name="path">Valid file path that includes a file extension</param>
        /// <returns>byte[] Array read from file</returns>
        public static byte[] ReadBytes(this string path)
        {
            if (IsPathValid(path))
            {
                return File.ReadAllBytes(path);
            }
            Debug.unityLogger.LogError("IO Exception", "Path is invalid");
            return Array.Empty<byte>();
        }

        /// <summary>
        /// Read lines from file
        /// </summary>
        /// <param name="path">Valid file path that includes a file extension</param>
        /// <param name="encodingType">Default Encoding used is UTF8</param>
        /// <returns>Lines read from file</returns>
        public static IEnumerable<string> ReadLines(this string path, EncodingType encodingType = EncodingType.UTF8)
        {
            if (IsPathValid(path))
            {
                return File.ReadAllLines(path, Enums.GetEncoding(encodingType));
            }
            Debug.unityLogger.LogError("IO Exception", "Path is invalid");
            return Array.Empty<string>();
        }

        /// <summary>
        /// Read text from file
        /// </summary>
        /// <param name="path">Valid file path that includes a file extension</param>
        /// <param name="encodingType">Default Encoding used is UTF8</param>
        /// <returns>Text read from file</returns>
        public static async Task<string> ReadTextAsync(this string path, EncodingType encodingType = EncodingType.UTF8)
        {
            if (IsPathValid(path))
            {
                return await File.ReadAllTextAsync(path, Enums.GetEncoding(encodingType));
            }
            Debug.unityLogger.LogError("IO Exception", "Path is invalid");
            return string.Empty;
        }

        /// <summary>
        /// Read byte[] Array from file
        /// </summary>
        /// <param name="path">Valid file path that includes a file extension</param>
        /// <returns>byte[] Array read from file</returns>
        public static async Task<byte[]> ReadBytesAsync(this string path)
        {
            if (IsPathValid(path))
            {
                return await File.ReadAllBytesAsync(path);
            }
            Debug.unityLogger.LogError("IO Exception", "Path is invalid");
            return Array.Empty<byte>();
        }

        /// <summary>
        /// Read lines from file
        /// </summary>
        /// <param name="path">Valid file path that includes a file extension</param>
        /// <param name="encodingType">Default Encoding used is UTF8</param>
        /// <returns>Lines read from file</returns>
        public static async Task<IEnumerable<string>> ReadLinesAsync(this string path, EncodingType encodingType = EncodingType.UTF8)
        {
            if (IsPathValid(path))
            {
                return await File.ReadAllLinesAsync(path, Enums.GetEncoding(encodingType));
            }
            Debug.unityLogger.LogError("IO Exception", "Path is invalid");
            return Array.Empty<string>();
        }

        #endregion

        #region STREAM FILE IO

        /// <summary>
        /// Write to a file using Stream Writer
        /// </summary>
        /// <param name="path">Valid file path that includes a file extension</param>
        /// <param name="data">Generic Data of Type T</param>
        /// <param name="fileMode">The FileMode is used for creating, opening, or appending to a file</param>
        /// <param name="fileAccess">FileAccess is used for reading and writing to a file, and set to Write by default</param>
        /// <param name="fileShare">FileShare is used to determine how the file can be shared with other processes, and is set to None by default</param>
        /// <param name="encodingType">EncodingType is set to UTF8 by default</param>
        public static void StreamWriter<T>(
            this string path,
            T data,
            FileMode fileMode = FileMode.Create,
            FileAccess fileAccess = FileAccess.Write,
            FileShare fileShare = FileShare.None,
            EncodingType encodingType = EncodingType.UTF8)
        {
            using FileStream stream = new FileStream(path, fileMode, fileAccess, fileShare);
            using (StreamWriter writer = new StreamWriter(stream, Enums.GetEncoding(encodingType)))
            {
                writer.Write(data);
            }
        }

        /// <summary>
        /// Write to a file using an Async Stream Writer
        /// </summary>
        /// <param name="path">Valid file path that includes a file extension</param>
        /// <param name="data">Generic Data of Type T</param>
        /// <param name="fileMode">The FileMode is used for creating, opening, or appending to a file</param>
        /// <param name="fileAccess">FileAccess is used for reading and writing to a file, and set to Write by default</param>
        /// <param name="fileShare">FileShare is used to determine how the file can be shared with other processes, and is set to None by default</param>
        /// <param name="encodingType">EncodingType is set to UTF8 by default</param>
        public static async Task StreamWriterAsync(
            this string path,
            string data,
            FileMode fileMode = FileMode.Create,
            FileAccess fileAccess = FileAccess.Write,
            FileShare fileShare = FileShare.None,
            EncodingType encodingType = EncodingType.UTF8)
        {
            await using FileStream stream = new FileStream(path, fileMode, fileAccess, fileShare);
            await using (StreamWriter writer = new StreamWriter(stream, Enums.GetEncoding(encodingType)))
            {
                await writer.WriteAsync(data);
            }
        }

        /// <summary>
        /// Read from a File using Stream Reader
        /// </summary>
        /// <param name="path">Valid file path that includes a file extension</param>
        /// <param name="fileMode">The FileMode is used for creating, opening, or appending to a file</param>
        /// <param name="fileAccess">FileAccess is used for reading and writing to a file, and set to Write by default</param>
        /// <param name="fileShare">FileShare is used to determine how the file can be shared with other processes, and is set to None by default</param>
        /// <param name="encodingType">EncodingType is set to UTF8 by default</param>
        /// <returns>Returns the string data read from the file</returns>
        public static string StreamReader(
            this string path,
            FileMode fileMode = FileMode.Open,
            FileAccess fileAccess = FileAccess.Read,
            FileShare fileShare = FileShare.None,
            EncodingType encodingType = EncodingType.UTF8)
        {
            using FileStream stream = new FileStream(path, fileMode, fileAccess, fileShare);
            using (StreamReader reader = new StreamReader(stream, Enums.GetEncoding(encodingType)))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// Read from a File using an Async Stream Reader
        /// </summary>
        /// <param name="path">Valid file path that includes a file extension</param>
        /// <param name="fileMode">FileMode is used for creating, opening, or appending to a file</param>
        /// <param name="fileAccess">FileAccess is used for reading and writing to a file, and set to Write by default</param>
        /// <param name="fileShare">FileShare is used to determine how the file can be shared with other processes, and is set to None by default</param>
        /// <param name="encodingType">EncodingType is set to UTF8 by default</param>
        /// <returns>Returns the string data read from the file</returns>
        public static async Task<string> StreamReaderAsync(this string path,
            FileMode fileMode = FileMode.Open,
            FileAccess fileAccess = FileAccess.Read,
            FileShare fileShare = FileShare.None,
            EncodingType encodingType = EncodingType.UTF8)
        {
            await using FileStream stream = new FileStream(path, fileMode, fileAccess, fileShare);
            using (StreamReader reader = new StreamReader(stream, Enums.GetEncoding(encodingType)))
            {
                return await reader.ReadToEndAsync();
            }
        }

        #endregion

        #region HELPER METHODS

        private static bool IsPathValid(string path)
        {
            return !path.IsNullOrEmpty() && !Path.GetExtension(path).IsNullOrEmpty();
        }

        #endregion
    }
}