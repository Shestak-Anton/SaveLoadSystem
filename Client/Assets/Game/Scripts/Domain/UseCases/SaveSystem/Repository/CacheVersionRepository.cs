using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace UseCases.Repository
{
    public sealed class CacheVersionRepository : IRepository
    {
        private const string PreferencesKey = "LastGameVersion";

        private readonly IRepository _repository;

        public CacheVersionRepository(IRepository repository)
        {
            _repository = repository;
        }

        async UniTask<SaveResult> IRepository.Save(JObject data, CancellationToken cancellationToken)
        {
            if (data == null)
                return SaveResult.WithError();
            data["version"] = PlayerPrefs.GetInt(PreferencesKey, 0) + 1;
            var saveResult = await _repository.Save(data, cancellationToken);
            PlayerPrefs.SetInt(PreferencesKey, saveResult.Version);
            return saveResult;
        }

        UniTask<LoadResult> IRepository.Load(int version, CancellationToken cancellationToken)
        {
            return _repository.Load(version, cancellationToken);
        }
    }
}