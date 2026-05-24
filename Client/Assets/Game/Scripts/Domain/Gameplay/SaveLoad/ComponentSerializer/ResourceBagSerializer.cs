using System;
using Newtonsoft.Json;
using SampleGame.Common;
using SampleGame.Gameplay;

namespace Game.Gameplay.SaveLoad
{
    public sealed class ResourceBagSerializer : ComponentSerializer<ResourceBag, ResourceBagSerializer.Data>
    {
        [Serializable]
        public record Data(
            [property: JsonProperty("current")] int Current,
            [property: JsonProperty("type")] ResourceType Type
        );

        protected override string NodeKey => "resourceBag";

        protected override Data DoOnSerialize(ResourceBag component) => new(component.Current, component.Type);

        protected override void DoOnDeserialize(ResourceBag component, Data data)
        {
            component.Current = data.Current;
            component.Type = data.Type;
        }
    }
}