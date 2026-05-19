using System.Threading;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace UseCases.Repository
{
    public interface IRepository
    {
        UniTask<bool> Save(JObject data, CancellationToken cancellationToken = default);
        UniTask<DataResponse> Load(CancellationToken cancellationToken = default);
    }
}