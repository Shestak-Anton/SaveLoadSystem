using Newtonsoft.Json.Linq;
using SampleGame.Gameplay;

namespace Game.Gameplay.SaveLoad
{
    public sealed class CountdownSerializer : ComponentSerializer<Countdown>
    {
        protected override string NodeKey => "countdown";

        protected override JObject DoOnSerialize(Countdown component) =>
            new()
            {
                { "value", component.Current }
            };

        protected override void DoOnDeserialize(Countdown component, JObject data) =>
            component.Current = data.Value<float>("value");
    }
}