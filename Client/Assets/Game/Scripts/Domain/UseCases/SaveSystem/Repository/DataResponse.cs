using Newtonsoft.Json.Linq;

namespace UseCases.Repository
{
    public record DataResponse(bool Success, JObject Data)
    {
        public static DataResponse WithError() => new(false, null);
        public static DataResponse WithSuccess(JObject data) => new(true, data);
    }
}