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

        async UniTask<bool> IRepository.Save(JObject data, CancellationToken cancellationToken)
        {
            var version=0;

            var bytes = Encoding.UTF8.GetBytes(data.ToString());
            var url = $"{_uri}/save?version={version}";
            var request = new UnityWebRequest(url, "PUT")
            {
                uploadHandler = new UploadHandlerRaw(bytes),
                downloadHandler = new DownloadHandlerBuffer()
            };
            request.SetRequestHeader("Content-Type", "application/json");
            try
            {
                await request.SendWebRequest().WithCancellation(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Save cancelled");
                return false;
            }

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Save error: {request.error}");
                return false;
            }

            Debug.Log($"Save Completed {data}");
            return true;
        }

        async UniTask<DataResponse> IRepository.Load(CancellationToken cancellationToken)
        {
            return DataResponse.WithError();
        }
    }
}