using Newtonsoft.Json.Linq;
using SampleGame.Gameplay;

namespace Game.Gameplay.SaveLoad
{
    public sealed class HealthSerializer : ComponentSerializer<Health>
    {
        protected override string NodeKey => "health";

        protected override JObject DoOnSerialize(Health component) => 
            new()
            {
                { "value", component.Current }
            };

        protected override void DoOnDeserialize(Health component, JObject data) =>
            component.Current = data.Value<int>("value");
    }
}