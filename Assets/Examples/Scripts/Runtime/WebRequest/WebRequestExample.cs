using System.IO;
using LazyJedi.Common;
using UnityEngine;

namespace LazyJedi
{
    public class WebRequestExample : MonoBehaviour
    {
        #region FIELDS

        [Header("Music URL")]
        public AudioType AudioType = AudioType.MPEG;
        public string AudioURL = "https://opengameart.org/sites/default/files/regular%20battle.mp3";

        [Header("Image URL")]
        public TextureType TextureType = TextureType.JPG;
        public bool IsTextureReadable = true;
        public string ImageURL = "https://w.wallhaven.cc/full/m3/wallhaven-m3py8m.jpg";

        [Header("Zip File URL")]
        public string ZipURL = "https://www.kenney.nl/media/pages/assets/tower-defense-kit/983a08988f-1677580495/kenney_tower-defense-kit.zip";
        public string ZipURL2 = "https://cdn.discordapp.com/attachments/875460594935955506/1150141574924210176/spacesniffer_1_3_0_2.zip";

        [Header("Text File URL")]
        public string TextFileURL = "https://sample-videos.com/csv/Sample-Spreadsheet-500000-rows.csv";

        #endregion

        #region UNITY METHODS

        private void Start()
        {
            DownloadAudioFile();
            DownloadTexture2D();
            DownloadFile();
        }

        #endregion

        #region METHODS

        private void DownloadAudioFile()
        {
            StartCoroutine(WebRequestUtility.DownloadAudio(
                    AudioURL,
                    AudioType,
                    response => { File.WriteAllBytes(Path.Combine("Assets", Path.GetFileName(AudioURL)), response.Data); },
                    progress => { Debug.unityLogger.Log($"Audio Progress - {progress}"); }
                )
            );
        }

        private void DownloadTexture2D()
        {
            StartCoroutine(WebRequestUtility.DownloadTexture(
                ImageURL,
                IsTextureReadable,
                TextureType,
                response =>
                {
                    if (IsTextureReadable)
                    {
                        File.WriteAllBytes(Path.Combine("Assets", Path.GetFileName(ImageURL)), response.Data);
                    }
                },
                progress => { Debug.unityLogger.Log($"Texture Progress - {progress}"); }
            ));
        }

        private void DownloadFile()
        {
            string filePath = Path.Combine("Assets", Path.GetFileName(ZipURL));
            StartCoroutine(WebRequestUtility.DownloadFileBuffer(
                ZipURL,
                response => { File.WriteAllBytes(filePath, response.Data); },
                progress => { Debug.unityLogger.Log($"File Progress - {progress}"); }
            ));

            string textPath = Path.Combine("Assets", Path.GetFileName(TextFileURL));
            StartCoroutine(WebRequestUtility.DownloadFile(
                TextFileURL,
                textPath,
                response => { File.WriteAllBytes(textPath, response.Data); },
                progress => { Debug.unityLogger.Log($"Text Progress - {progress}"); }
            ));

            string filePath2 = Path.Combine("Assets", $"{Path.GetFileNameWithoutExtension(ZipURL2)}-2{Path.GetExtension(ZipURL2)}");
            StartCoroutine(WebRequestUtility.HTTPGet(ZipURL2, response => { File.WriteAllBytes(filePath2, response.Data); },
                progress => { Debug.unityLogger.Log($"File Progress 2 - {progress}"); }));
        }

        #endregion
    }
}