using System.Collections.Generic;
using System.Linq;
using Modules.Entities;
using Newtonsoft.Json.Linq;
using SampleGame.Common;
using SaveSystem;

namespace Game.Gameplay.SaveLoad
{
    public sealed class EntitySerializer : ISaveSerializer
    {
        private readonly EntityWorld _entityWorld;
        private readonly IComponentSerializer[] _componentSerializers;

        public string Key => "data";

        public EntitySerializer(
            EntityWorld entityWorld,
            IComponentSerializer[] componentSerializers)
        {
            _entityWorld = entityWorld;
            _componentSerializers = componentSerializers;
        }

        public JToken Serialize()
        {
            var result = new JArray();
            foreach (var entity in _entityWorld.GetAll())
            {
                var entityJObject = Serialize(entity);
                foreach (var componentSerializer in _componentSerializers)
                {
                    if (!componentSerializer.TrySerialize(entity, out var key, out var data)) continue;
                    entityJObject.Add(key, data);
                }

                if (entityJObject.Count == 0) continue;
                result.Add(entityJObject);
            }

            return result;
        }

        public void Deserialize(JToken data)
        {
            _entityWorld.DestroyAll();

            var result = new Dictionary<Entity, JObject>();

            foreach (var root in data.OfType<JObject>())
            {
                var rootData = Deserialize(root);
                var entity = SpawnEntity(rootData);
                result.Add(entity, root);
            }

            foreach (var (entity, rootData) in result)
            {
                foreach (var componentSerializer in _componentSerializers)
                {
                    componentSerializer.Deserialize(entity, rootData);
                }
            }
        }

        private static JObject Serialize(Entity entity)
        {
            return new JObject
            {
                ["id"] = entity.Id,
                ["name"] = entity.Name,
                ["type"] = (int)entity.Type,
                ["position"] = JObject.FromObject(new SerializedVector3(entity.transform.position)),
                ["rotation"] = JObject.FromObject(new SerializedVector3(entity.transform.rotation.eulerAngles)),
            };
        }

        private EntityRootData Deserialize(JObject root)
        {
            return new EntityRootData(
                Id: root.Value<int>("id"),
                Name: root.Value<string>("name"),
                Position: root["position"].ToObject<SerializedVector3>(),
                Rotation: root["rotation"].ToObject<SerializedVector3>()
            );
        }

        private Entity SpawnEntity(EntityRootData entity) =>
            _entityWorld.Spawn(entity.Name, entity.Position, entity.Rotation, entity.Id);

        private record EntityRootData(
            int Id,
            string Name,
            SerializedVector3 Position,
            SerializedVector3 Rotation
        );
    }
}