using System;
using Modules.Entities;
using Newtonsoft.Json;
using SampleGame.Gameplay;

namespace Game.Gameplay.SaveLoad
{
    public sealed class TargetObjectSerializer : ComponentSerializer<TargetObject, TargetObjectSerializer.Data>
    {
        [Serializable]
        public record Data(
            [property: JsonProperty("id")] int EntityId
        );

        protected override string NodeKey => "targetObject";

        private readonly EntityWorld _entityWorld;

        public TargetObjectSerializer(EntityWorld entityWorld) => _entityWorld = entityWorld;

        protected override Data DoOnSerialize(TargetObject component) => new(component.Value?.Id ?? -1);

        protected override void DoOnDeserialize(TargetObject component, Data data)
        {
            if (_entityWorld.TryGet(data.EntityId, out var entity))
                component.Value = entity;
        }
    }
}