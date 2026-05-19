using Newtonsoft.Json.Linq;
using SampleGame.Gameplay;

namespace Game.Gameplay.SaveLoad
{
    public sealed class CountdownSerializer : ComponentSerializer<Countdown>
    {
        protected override string NodeKey => "Countdown";

        protected override JObject DoOnSerialize(Countdown component) => new() { { "value", component.Current } };

        protected override void DoOnDeserialize(Countdown component, JObject data) =>
            component.Current = float.TryParse(data["value"].ToString(), out var result) ? result : 0f;
    }
}