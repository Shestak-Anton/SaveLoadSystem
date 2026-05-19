using Newtonsoft.Json.Linq;

namespace UseCases.Repository
{
    public record LoadResult(bool Success, JObject Data)
    {
        public static LoadResult WithError() => new(false, null);
        public static LoadResult WithSuccess(JObject data) => new(true, data);
    }
}