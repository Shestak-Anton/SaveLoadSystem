using System.Threading;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UseCases.Repository;

namespace SaveSystem
{
    public sealed class SaveManager
    {
        private readonly ISaveSerializer[] _saveSerializers;
        private readonly IRepository _repository;

        public SaveManager(
            ISaveSerializer[] saveSerializers,
            IRepository repository
        )
        {
            _saveSerializers = saveSerializers;
            _repository = repository;
        }

        public async UniTask<SaveResult> Save(CancellationToken ct = default)
        {
            var gameData = new JObject();
            foreach (var saveSerializer in _saveSerializers)
                gameData.Add(saveSerializer.Key, saveSerializer.Serialize());
            return await _repository.Save(gameData, ct);
        }

        public async UniTask<LoadResult> Load(int version, CancellationToken ct = default)
        {
            var loadResult = await _repository.Load(version, ct);
            if (!loadResult.Success) return loadResult;

            foreach (var saveSerializer in _saveSerializers)
            {
                if (loadResult.Data.TryGetValue(saveSerializer.Key, out var value))
                    saveSerializer.Deserialize(value);
            }

            return loadResult;
        }
    }
}