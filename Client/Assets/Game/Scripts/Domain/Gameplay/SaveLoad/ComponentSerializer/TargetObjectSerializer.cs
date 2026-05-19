using Modules.Entities;
using Newtonsoft.Json.Linq;
using SampleGame.Gameplay;

namespace Game.Gameplay.SaveLoad
{
    public sealed class TargetObjectSerializer : ComponentSerializer<TargetObject>
    {
        protected override string NodeKey => "TargetObject";
        
        private readonly EntityWorld _entityWorld;

        public TargetObjectSerializer(EntityWorld entityWorld)
        {
            _entityWorld = entityWorld;
        }

        protected override JObject DoOnSerialize(TargetObject component) => new()
        {
            { "id", component.Value?.Id ?? -1 }
        };

        protected override void DoOnDeserialize(TargetObject component, JObject data)
        {
            var id = int.Parse(data["id"].ToString());
            if (_entityWorld.TryGet(id, out var entity))
            {
                component.Value = entity;
            }
        }
    }
}