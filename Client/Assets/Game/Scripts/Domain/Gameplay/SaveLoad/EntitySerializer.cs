using System.Linq;
using Modules.Entities;
using Newtonsoft.Json.Linq;
using SaveSystem;
using Unity.VisualScripting;
using UnityEngine;

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
                var converted = Convert(entity);
                if (converted.Count == 0) continue;
                result.Add(converted);
            }

            return result;
        }

        public void Deserialize(JToken data)
        {
            foreach (var root in data.OfType<JObject>())
            {
                var entity = Convert(root);
            }
        }

        private JObject Convert(Entity entity)
        {
            var result = new JObject
            {
                ["Id"] = entity.Id,
                ["Name"] = entity.Name,
                ["Type"] = (int)entity.Type
            };

            foreach (var componentSerializer in _componentSerializers)
            {
                if (!componentSerializer.TrySerialize(entity, out var data)) continue;
                result.Merge(data);
            }

            return result;
        }

        private Entity Convert(JObject root)
        {
            var id = root.Value<int>("Id");
            var name = root.Value<string>("Name");

            Entity entity;
            if (_entityWorld.TryGet(id, out var e))
            {
                entity = e;
            }
            else
            {
                entity = _entityWorld.Spawn(name, Vector3.zero, Quaternion.Euler(Vector3.zero), id);
                _entityWorld.Add(entity, entity.Id);
            }

            foreach (var componentSerializer in _componentSerializers)
            {
                componentSerializer.Deserialize(entity, root);
            }

            return entity;
        }
    }
}