using System;
using System.Collections.Generic;
using Modules.Entities;
using Newtonsoft.Json;
using SampleGame.Gameplay;

namespace Game.Gameplay.SaveLoad
{
    public sealed class ProductOrderSerializer : ComponentSerializer<ProductionOrder, ProductOrderSerializer.Data[]>
    {
        [Serializable]
        public record Data(
            [property: JsonProperty("name")] string Name,
            [property: JsonProperty("type")] EntityType Type,
            [property: JsonProperty("size")] float Size
        );

        protected override string NodeKey => "productOrder";

        private readonly EntityCatalog _entityCatalog;

        public ProductOrderSerializer(EntityCatalog entityCatalog)
        {
            _entityCatalog = entityCatalog;
        }

        protected override Data[] DoOnSerialize(ProductionOrder component)
        {
            var configs = component.Queue;
            var data = new Data[configs.Count];
            for (var index = 0; index < configs.Count; index++)
            {
                var config = configs[index];
                data[index] = new Data(
                    Name: config.Name,
                    Type: config.Type,
                    Size: config.Size
                );
            }

            return data;
        }

        protected override void DoOnDeserialize(ProductionOrder component, Data[] data)
        {
            var configs = new List<EntityConfig>();
            foreach (var rawConfig in data)
            {
                if (_entityCatalog.FindConfig(rawConfig.Name, out var config))
                {
                    configs.Add(config);
                }
            }
            
            component.Queue = configs;
        }
    }
}