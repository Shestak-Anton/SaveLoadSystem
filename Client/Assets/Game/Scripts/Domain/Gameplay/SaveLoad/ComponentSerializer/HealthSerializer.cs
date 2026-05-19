using Newtonsoft.Json.Linq;
using SampleGame.Gameplay;

namespace Game.Gameplay.SaveLoad
{
    public sealed class HealthSerializer : ComponentSerializer<Health>
    {
        protected override string NodeKey => "Health";

        protected override JObject DoOnSerialize(Health component) => new() { { "value", component.Current } };

        protected override void DoOnDeserialize(Health component, JObject data) =>
            component.Current = data.TryGetValue("value", out var value) ? int.Parse(value.ToString()) : 0;
    }
}