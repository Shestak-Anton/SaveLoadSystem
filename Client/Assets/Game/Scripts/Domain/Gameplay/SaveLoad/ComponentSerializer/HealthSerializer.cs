using System;
using Newtonsoft.Json;
using SampleGame.Gameplay;

namespace Game.Gameplay.SaveLoad
{
    public sealed class HealthSerializer : ComponentSerializer<Health, HealthSerializer.Data>
    {
        [Serializable]
        public record Data(
            [property: JsonProperty("value")] int Heath
        );

        protected override string NodeKey => "health";

        protected override Data DoOnSerialize(Health component) => new(component.Current);

        protected override void DoOnDeserialize(Health component, Data data) => component.Current = data.Heath;
    }
}