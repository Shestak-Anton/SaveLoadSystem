using System;
using Newtonsoft.Json;
using SampleGame.Gameplay;

namespace Game.Gameplay.SaveLoad
{
    public sealed class CountdownSerializer : ComponentSerializer<Countdown, CountdownSerializer.Data>
    {
        [Serializable]
        public record Data(
            [property: JsonProperty("value")] float Current
        );

        protected override string NodeKey => "countdown";

        protected override Data DoOnSerialize(Countdown component) => new(component.Current);

        protected override void DoOnDeserialize(Countdown component, Data data) => component.Current = data.Current;
    }
}