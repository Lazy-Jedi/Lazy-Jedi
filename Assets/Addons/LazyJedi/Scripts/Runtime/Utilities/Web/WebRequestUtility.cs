using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace LazyJedi.Common
{
    public static class WebRequestUtility
    {
        #region METHODS

        /// <summary>
        /// HTTP GET request, this will return the raw string data. Use this method to get string data from the server.
        /// </summary>
        /// <param name="url">This is the GET RestAPI URL</param>
        /// <param name="response">Response Callback</param>
        /// <param name="progress">Progress Callback</param>
        /// <param name="headers">Add custom headers to the request.</param>
        public static IEnumerator HttpGet(
            string url,
            Action<Response<string>> response,
            Action<float> progress = null,
            Dictionary<string, string> headers = null)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> header in headers)
                    {
                        webRequest.SetRequestHeader(header.Key, header.Value);
                    }
                }

                webRequest.SendWebRequest();
                while (!webRequest.isDone)
                {
                    progress?.Invoke(webRequest.downloadProgress);
                    yield return null;
                }

                LogRequestResult(webRequest.result);
                response?.Invoke(new Response<string>
                {
                    Data = webRequest.downloadHandler.text,
                    StatusCode = webRequest.responseCode,
                    Error = webRequest.error
                });
            }
        }

        /// <summary>
        /// HTTP GET request, this will return the raw byte array data. Use this method to download files.
        /// </summary>
        /// <param name="url">This is the GET RestAPI URL</param>
        /// <param name="response">Response Callback</param>
        /// <param name="progress">Progress Callback</param>
        /// <param name="headers">Add custom headers to the request.</param>
        public static IEnumerator HttpGet(
            string url,
            Action<Response<byte[]>> response,
            Action<float> progress = null,
            Dictionary<string, string> headers = null)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> header in headers)
                    {
                        webRequest.SetRequestHeader(header.Key, header.Value);
                    }
                }

                webRequest.SendWebRequest();
                while (!webRequest.isDone)
                {
                    progress?.Invoke(webRequest.downloadProgress);
                    yield return null;
                }

                LogRequestResult(webRequest.result);
                response?.Invoke(new Response<byte[]>
                {
                    Data = webRequest.downloadHandler.data,
                    StatusCode = webRequest.responseCode,
                    Error = webRequest.error
                });
            }
        }

        /// <summary>
        /// HTTP POST request. Use this method to send text data to the server.<br/>
        /// Use this method to send text, json, xml, etc.
        /// </summary>
        /// <param name="url">This is the POST RestAPI URL</param>
        /// <param name="body">The body parameter is a string, you can use JsonUtility.ToJson() to convert an object to a string.</param>
        /// <param name="response">Response Callback</param>
        /// <param name="progress">Progress Callback</param>
        /// <param name="contentType">The contentType parameter is the content type of the body parameter, default is application/json.</param>
        /// <param name="headers">Add custom headers to the request.</param>
        public static IEnumerator HttpPost(
            string url,
            string body,
            Action<Response<byte[]>> response,
            Action<float> progress = null,
            string contentType = "application/json",
            Dictionary<string, string> headers = null)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Post(url, body, contentType))
            {
                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> header in headers)
                    {
                        webRequest.SetRequestHeader(header.Key, header.Value);
                    }
                }

                webRequest.uploadHandler.contentType = contentType;
                webRequest.SendWebRequest();

                while (!webRequest.isDone)
                {
                    progress?.Invoke(webRequest.uploadProgress);
                    yield return null;
                }

                LogRequestResult(webRequest.result);
                response?.Invoke(new Response<byte[]>
                {
                    Data = webRequest.uploadHandler.data,
                    StatusCode = webRequest.responseCode,
                    Error = webRequest.error
                });
            }
        }

        /// <summary>
        /// HTTP PUT request. Use this method to send data to the server.
        /// </summary>
        /// <param name="url">This is the PUT RestAPI URL</param>
        /// <param name="body">The body parameter is a string, you can use JsonUtility.ToJson() to convert an object to a string.</param>
        /// <param name="response">Response Callback</param>
        /// <param name="progress">Progress Callback</param>
        /// <param name="contentType">The contentType parameter is the content type of the body parameter, default is application/json.</param>
        /// <param name="headers">Add custom headers to the request.</param>
        /// <returns></returns>
        public static IEnumerator HttpPut(
            string url,
            string body,
            Action<Response<byte[]>> response,
            Action<float> progress = null,
            string contentType = "application/json",
            Dictionary<string, string> headers = null)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Put(url, body))
            {
                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> header in headers)
                    {
                        webRequest.SetRequestHeader(header.Key, header.Value);
                    }
                }

                webRequest.uploadHandler.contentType = contentType;
                webRequest.SendWebRequest();

                while (!webRequest.isDone)
                {
                    progress?.Invoke(webRequest.uploadProgress);
                    yield return null;
                }

                LogRequestResult(webRequest.result);
                response?.Invoke(new Response<byte[]>
                {
                    Data = webRequest.uploadHandler.data,
                    StatusCode = webRequest.responseCode,
                    Error = webRequest.error
                });
            }
        }

        /// <summary>
        /// HTTP DELETE request. Use this method to delete data on the server.
        /// </summary>
        /// <param name="url">This is the POST RestAPI URL</param>
        /// <param name="response">Response Callback</param>
        /// <param name="headers">Add custom headers to the request.</param>
        public static IEnumerator HttpDelete(
            string url,
            Action<Response<string>> response,
            Dictionary<string, string> headers = null)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Delete(url))
            {
                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> header in headers)
                    {
                        webRequest.SetRequestHeader(header.Key, header.Value);
                    }
                }

                yield return webRequest.SendWebRequest();
                LogRequestResult(webRequest.result);
                response(new Response<string>
                {
                    StatusCode = webRequest.responseCode
                });
            }
        }

        /// <summary>
        /// HTTP HEAD request. Use this method to get the headers of a file.
        /// </summary>
        /// <param name="url">This is the POST RestAPI URL</param>
        /// <param name="response">Response Callback</param>
        public static IEnumerator HttpHead(
            string url,
            Action<Response<Dictionary<string, string>>> response)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Head(url))
            {
                yield return webRequest.SendWebRequest();
                LogRequestResult(webRequest.result);
                Dictionary<string, string> responseHeaders = webRequest.GetResponseHeaders();
                response(new Response<Dictionary<string, string>>
                {
                    StatusCode = webRequest.responseCode,
                    Error = webRequest.error,
                    Data = responseHeaders
                });
            }
        }

        #endregion

        #region DOWNLOADER METHODS

        /// <summary>
        /// Download Audio from URL.
        /// </summary>
        /// <param name="url">URL of the file</param>
        /// <param name="audioType">Audio Format</param>
        /// <param name="response">Response Callback</param>
        /// <param name="progress">Progress Callback</param>
        /// <param name="headers">Add custom headers to the request.</param>
        public static IEnumerator DownloadAudio(
            string url,
            AudioType audioType,
            Action<AudioResponse> response,
            Action<float> progress = null,
            Dictionary<string, string> headers = null)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> header in headers)
                    {
                        webRequest.SetRequestHeader(header.Key, header.Value);
                    }
                }

                DownloadHandlerAudioClip downloader = new DownloadHandlerAudioClip(url, audioType);
                webRequest.downloadHandler = downloader;
                webRequest.SendWebRequest();

                while (!webRequest.isDone)
                {
                    progress?.Invoke(webRequest.downloadProgress);
                    yield return null;
                }

                LogRequestResult(webRequest.result);
                response?.Invoke(new AudioResponse
                {
                    AudioClip = downloader.audioClip,
                    Data = downloader.data,
                    StatusCode = webRequest.responseCode,
                    Error = webRequest.error
                });
            }
        }

        /// <summary>
        /// Download Texture2D from URL.
        /// </summary>
        /// <param name="url">URL of the file</param>
        /// <param name="isTextureReadable">Set this value to True if you want to write the Texture to disk</param>
        /// <param name="textureType">Format of the Texture</param>
        /// <param name="response">Response Callback</param>
        /// <param name="progress">Progress Callback</param>
        /// <param name="headers">Add custom headers to the request.</param>
        public static IEnumerator DownloadTexture(
            string url,
            bool isTextureReadable,
            TextureType textureType,
            Action<Texture2DResponse> response,
            Action<float> progress = null,
            Dictionary<string, string> headers = null)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> header in headers)
                    {
                        webRequest.SetRequestHeader(header.Key, header.Value);
                    }
                }

                DownloadHandlerTexture downloader = new DownloadHandlerTexture(isTextureReadable);
                webRequest.downloadHandler = downloader;
                webRequest.SendWebRequest();

                while (!webRequest.isDone)
                {
                    progress?.Invoke(webRequest.downloadProgress);
                    yield return null;
                }

                LogRequestResult(webRequest.result);
                response?.Invoke(new Texture2DResponse
                {
                    Texture2D = downloader.texture,
                    Data = EncodedTextureFormat(textureType, downloader.texture),
                    StatusCode = webRequest.responseCode,
                    Error = webRequest.error
                });
            }
        }

        /// <summary>
        /// Download AssetBundle from URL. 
        /// </summary>
        /// <param name="url">URL of the file</param>
        /// <param name="checksum">Checksum is used to verify the integrity of the file, use 0 to skip integrity check.</param>
        /// <param name="response">Response Callback</param>
        /// <param name="progress">Progress Callback</param>
        /// <param name="headers">Add custom headers to the request.</param>
        public static IEnumerator DownloadAssetBundle(
            string url,
            uint checksum,
            Action<Response<AssetBundle>> response,
            Action<float> progress = null,
            Dictionary<string, string> headers = null)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> header in headers)
                    {
                        webRequest.SetRequestHeader(header.Key, header.Value);
                    }
                }

                DownloadHandlerAssetBundle downloader = new DownloadHandlerAssetBundle(url, checksum);
                webRequest.downloadHandler = downloader;
                webRequest.SendWebRequest();

                while (!webRequest.isDone)
                {
                    progress?.Invoke(webRequest.downloadProgress);
                    yield return null;
                }

                LogRequestResult(webRequest.result);
                response?.Invoke(new Response<AssetBundle>
                {
                    Data = downloader.assetBundle,
                    StatusCode = webRequest.responseCode,
                    Error = webRequest.error
                });
            }
        }


        /// <summary>
        /// Download file to memory, this is useful for small files such as text, json, xml, etc.
        /// </summary>
        /// <param name="url">URL of the file</param>
        /// <param name="response">Response Callback</param>
        /// <param name="progress">Progress Callback</param>
        /// <param name="headers">Add custom headers to the request.</param>
        public static IEnumerator DownloadFileBuffer(
            string url,
            Action<Response<byte[]>> response,
            Action<float> progress = null,
            Dictionary<string, string> headers = null)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> header in headers)
                    {
                        webRequest.SetRequestHeader(header.Key, header.Value);
                    }
                }

                DownloadHandlerBuffer downloader = new DownloadHandlerBuffer();
                webRequest.downloadHandler = downloader;
                webRequest.SendWebRequest();

                while (!webRequest.isDone)
                {
                    progress?.Invoke(webRequest.downloadProgress);
                    yield return null;
                }

                LogRequestResult(webRequest.result);
                response?.Invoke(new Response<byte[]>
                {
                    Data = webRequest.downloadHandler.data,
                    StatusCode = webRequest.responseCode,
                    Error = webRequest.error
                });
            }
        }

        /// <summary>
        /// Download file to disk, this is useful for large files such as videos, audio, binaries, etc.
        /// </summary>
        /// <param name="url">URL of the file</param>
        /// <param name="path">Path to where the file will be saved on disk</param>
        /// <param name="response">Response Callback</param>
        /// <param name="progress">Progress Callback</param>
        /// <param name="headers">Add custom headers to the request.</param>
        public static IEnumerator DownloadFile(
            string url,
            string path,
            Action<Response<byte[]>> response,
            Action<float> progress = null,
            Dictionary<string, string> headers = null)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> header in headers)
                    {
                        webRequest.SetRequestHeader(header.Key, header.Value);
                    }
                }

                DownloadHandlerFile downloader = new DownloadHandlerFile(path);
                downloader.removeFileOnAbort = true;
                webRequest.downloadHandler = downloader;
                webRequest.SendWebRequest();

                while (!webRequest.isDone)
                {
                    progress?.Invoke(webRequest.downloadProgress);
                    yield return null;
                }

                LogRequestResult(webRequest.result);
                try
                {
                    response?.Invoke(new Response<byte[]>
                    {
                        Data = downloader.data,
                        StatusCode = webRequest.responseCode,
                        Error = webRequest.error
                    });
                }
                catch
                {
                    // Ignore Exception - NotSupportedException: Raw data access is not supported for this DownloadHandler
                    // Have not found a solution to this yet, alternatively use HttpGET or DownloadFileBuffer method.
                    // DownloadHandlerBuffer is not suitable for large files
                }
                finally
                {
                    downloader.Dispose();
                }
            }
        }

        #endregion

        #region UPLOAD METHODS

        /// <summary>
        /// Upload file to server, this is useful for large files such binaries, videos, etc.<br/>
        /// </summary>
        /// <param name="url">URL of the file</param>
        /// <param name="path">Path to where the file will be saved on disk</param>
        /// <param name="httpMethod">This is used to specify the HTTP Method Type.</param>
        /// <param name="response">Response Callback</param>
        /// <param name="progress">Progress Callback</param>
        /// <param name="headers">Add custom headers to the request.</param>
        public static IEnumerator UploadFile(
            string url,
            string path,
            HttpMethodType httpMethod,
            Action<Response<byte[]>> response,
            Action<float> progress = null,
            Dictionary<string, string> headers = null)
        {
            using (UnityWebRequest webRequest = new UnityWebRequest(url, GetHttpMethod(httpMethod)))
            {
                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> header in headers)
                    {
                        webRequest.SetRequestHeader(header.Key, header.Value);
                    }
                }

                UploadHandlerFile uploadHandler = new UploadHandlerFile(path);
                webRequest.uploadHandler = uploadHandler;
                webRequest.SendWebRequest();

                while (!webRequest.isDone)
                {
                    progress?.Invoke(webRequest.uploadProgress);
                    yield return null;
                }

                LogRequestResult(webRequest.result);
                response(new Response<byte[]>
                {
                    StatusCode = webRequest.responseCode,
                    Error = webRequest.error,
                    Data = null
                });
            }
        }

        /// <summary>
        /// Upload file to server, this is useful for small files such as text, json, xml, etc.<br/>
        /// The data is copied to memory before being sent to the server, not suitable for large files.
        /// </summary>
        /// <param name="url">URL of the file</param>
        /// <param name="rawData"></param>
        /// <param name="httpMethod">This is used to specify the HTTP Method Type.</param>
        /// <param name="response">Response Callback</param>
        /// <param name="progress">Progress Callback</param>
        /// <param name="headers">Add custom headers to the request.</param>
        public static IEnumerator UploadRawFile(
            string url,
            byte[] rawData,
            HttpMethodType httpMethod,
            Action<Response<byte[]>> response,
            Action<float> progress = null,
            Dictionary<string, string> headers = null)
        {
            using (UnityWebRequest webRequest = new UnityWebRequest(url, GetHttpMethod(httpMethod)))
            {
                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> header in headers)
                    {
                        webRequest.SetRequestHeader(header.Key, header.Value);
                    }
                }

                UploadHandlerRaw uploadHandler = new UploadHandlerRaw(rawData);
                webRequest.uploadHandler = uploadHandler;
                webRequest.SendWebRequest();

                while (!webRequest.isDone)
                {
                    progress?.Invoke(webRequest.uploadProgress);
                    yield return null;
                }

                LogRequestResult(webRequest.result);
                response(new Response<byte[]>
                {
                    StatusCode = webRequest.responseCode,
                    Error = webRequest.error,
                    Data = null
                });
            }
        }

        #endregion

        #region HELPER METHODS

        private static string GetHttpMethod(HttpMethodType httpMethod)
        {
            return httpMethod switch
            {
                HttpMethodType.GET    => UnityWebRequest.kHttpVerbGET,
                HttpMethodType.POST   => UnityWebRequest.kHttpVerbPOST,
                HttpMethodType.PUT    => UnityWebRequest.kHttpVerbPUT,
                HttpMethodType.DELETE => UnityWebRequest.kHttpVerbDELETE,
                HttpMethodType.HEAD   => UnityWebRequest.kHttpVerbHEAD,
                _                     => throw new ArgumentOutOfRangeException(nameof(httpMethod), httpMethod, "Invalid HTTP Method")
            };
        }

        private static byte[] EncodedTextureFormat(TextureType type, Texture2D texture)
        {
            return type switch
            {
                TextureType.PNG => texture.EncodeToPNG(),
                TextureType.JPG => texture.EncodeToJPG(),
                TextureType.EXR => texture.EncodeToEXR(),
                TextureType.TGA => texture.EncodeToTGA(),
                _               => texture.GetRawTextureData()
            };
        }

        private static void LogRequestResult(UnityWebRequest.Result result)
        {
            switch (result)
            {
                case UnityWebRequest.Result.InProgress:
                    Debug.unityLogger.Log("Request is in progress.");
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.unityLogger.Log("Request completed successfully.");
                    break;
                case UnityWebRequest.Result.ConnectionError:
                    Debug.unityLogger.Log("A network error occurred.");
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.unityLogger.Log("A protocol error occurred.");
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.unityLogger.Log("A data processing error occurred.");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(result), result, "Invalid Request Result");
            }
        }

        #endregion
    }

    public enum HttpMethodType
    {
        GET,
        POST,
        PUT,
        DELETE,
        HEAD
    }

    public enum TextureType
    {
        PNG,
        JPG,
        EXR,
        TGA
    }

    public class Response<T>
    {
        public long StatusCode { get; set; }
        public string Error { get; set; }
        public T Data { get; set; }
    }

    public class AudioResponse : Response<byte[]>
    {
        public AudioClip AudioClip;
    }

    public class Texture2DResponse : Response<byte[]>
    {
        public Texture2D Texture2D;

        public Sprite GetSpriteFromTexture(Vector2 pivot, float pixelsPerUnit)
        {
            return Sprite.Create(Texture2D, new Rect(0, 0, Texture2D.width, Texture2D.height), pivot, pixelsPerUnit);
        }

        public byte[] EncodeTexture2D(TextureType type)
        {
            switch (type)
            {
                case TextureType.PNG:
                    return Texture2D.EncodeToPNG();
                case TextureType.JPG:
                    return Texture2D.EncodeToJPG();
                case TextureType.EXR:
                    return Texture2D.EncodeToEXR();
                case TextureType.TGA:
                    return Texture2D.EncodeToTGA();
            }

            return Texture2D.GetRawTextureData();
        }
    }
}