using System;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace UseCases.Repository
{
    public sealed class RemoteRepository : IRepository
    {
        private readonly string _uri;

        public RemoteRepository(string uri) => _uri = uri;

        async UniTask<SaveResult> IRepository.Save(JObject data, CancellationToken cancellationToken)
        {
            try
            {
                var version = int.Parse(data["version"].ToString());
                var bytes = Encoding.UTF8.GetBytes(data.ToString());
                var url = $"{_uri}/save?version={version}";
                var request = new UnityWebRequest(url, "PUT")
                {
                    uploadHandler = new UploadHandlerRaw(bytes),
                    downloadHandler = new DownloadHandlerBuffer()
                };
                request.SetRequestHeader("Content-Type", "application/json");

                await request.SendWebRequest().WithCancellation(cancellationToken);

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"Save error: {request.error}");
                    return SaveResult.WithError();
                }

                Debug.Log($"Save Completed {data}");
                return SaveResult.WithSuccess(version);
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Save cancelled");
                return SaveResult.WithError();
            }
        }

        async UniTask<LoadResult> IRepository.Load(int version, CancellationToken cancellationToken)
        {
            var request = UnityWebRequest.Get($"{_uri}/load?version={version}");
            request.downloadHandler = new DownloadHandlerBuffer();
            try
            {
                await request.SendWebRequest().WithCancellation(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Load cancelled");
                return LoadResult.WithError();
            }

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Load error: {request.error}");
                return LoadResult.WithError();
            }

            var response = JObject.Parse(request.downloadHandler.text);
            var jsonText = response.ToString();

            if (string.IsNullOrEmpty(jsonText))
                return LoadResult.WithError();

            var gameData = JObject.Parse(jsonText);
            return LoadResult.WithSuccess(gameData);
        }
    }
}