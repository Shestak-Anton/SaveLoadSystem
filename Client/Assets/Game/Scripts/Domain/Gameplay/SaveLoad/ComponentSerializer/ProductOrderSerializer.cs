using System.Collections.Generic;
using Modules.Entities;
using Newtonsoft.Json.Linq;
using SampleGame.Gameplay;

namespace Game.Gameplay.SaveLoad
{
    public sealed class ProductOrderSerializer : ComponentSerializer<ProductionOrder>
    {
        protected override string NodeKey => "ProductOrder";
        
        private readonly EntityCatalog _entityCatalog;

        public ProductOrderSerializer(EntityCatalog entityCatalog)
        {
            _entityCatalog = entityCatalog;
        }

        protected override JObject DoOnSerialize(ProductionOrder component)
        {
            var result = new  JObject();
            var list = new JArray();
            result["productionOrders"] = list;
            foreach (var entityConfig in component.Queue)
            {
                var item = new JObject
                {
                    ["name"] = entityConfig.Name,
                    ["type"] = (int)entityConfig.Type,
                    ["size"] = entityConfig.Size
                };
                list.Add(item);
            }
            return result;
        }

        protected override void DoOnDeserialize(ProductionOrder component, JObject data)
        {
            if (!data.TryGetValue("productionOrders", out var array)) return;
            if (!array.HasValues) return;
            var configs = new List<EntityConfig>();
            foreach (var item in array.Values())
            {
                var name = item["name"];
                if (_entityCatalog.FindConfig(name.ToString(), out var config))
                {
                    configs.Add(config);
                }
            }
            component.Queue = configs;
        }
    }
}