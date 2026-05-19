using System.Threading;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace UseCases.Repository
{
    public interface IRepository
    {
        UniTask<SaveResult> Save(JObject data, CancellationToken cancellationToken = default);
        UniTask<LoadResult> Load(int version, CancellationToken cancellationToken = default);
    }
}