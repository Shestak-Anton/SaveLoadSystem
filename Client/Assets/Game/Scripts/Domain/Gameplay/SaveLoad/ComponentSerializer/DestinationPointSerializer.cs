using System;
using Newtonsoft.Json;
using SampleGame.Common;
using SampleGame.Gameplay;

namespace Game.Gameplay.SaveLoad
{
    public sealed class
        DestinationPointSerializer : ComponentSerializer<DestinationPoint, DestinationPointSerializer.Data>
    {
        [Serializable]
        public record Data(
            [property: JsonProperty("position")] SerializedVector3 Position
        );

        protected override string NodeKey => "destinationPoint";

        protected override Data DoOnSerialize(DestinationPoint component) =>
            new(new SerializedVector3(component.Value));

        protected override void DoOnDeserialize(DestinationPoint component, Data data) =>
            component.Value = data.Position;
    }
}