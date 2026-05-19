using Newtonsoft.Json.Linq;

namespace SaveSystem
{
    public interface ISaveSerializer
    {
        string Key { get; }
        JToken Serialize();
        void Deserialize(JToken data);
    }
}