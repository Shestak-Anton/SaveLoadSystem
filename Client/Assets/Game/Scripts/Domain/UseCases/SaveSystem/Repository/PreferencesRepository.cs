using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace UseCases.Repository
{
    public sealed class PreferencesRepository : IRepository
    {
        private const string PreferencesKey = "LastGameVersion";

        private readonly string _key;

        public PreferencesRepository(string key = PreferencesKey) => _key = key;

        UniTask<bool> IRepository.Save(JObject data, CancellationToken cancellationToken)
        {
            if (data == null)
                return UniTask.FromResult(false);
            PlayerPrefs.SetString(_key, data.ToString());
            return UniTask.FromResult(true);
        }

        UniTask<DataResponse> IRepository.Load(CancellationToken cancellationToken)
        {
            if (!PlayerPrefs.HasKey(_key))
                return UniTask.FromResult(DataResponse.WithError());

            var raw = PlayerPrefs.GetString(_key);
            try
            {
                var data = JObject.Parse(raw);
                return UniTask.FromResult(DataResponse.WithSuccess(data));
            }
            catch (Exception)
            {
                return UniTask.FromResult(DataResponse.WithError());
            }
        }
    }
}