using Newtonsoft.Json.Linq;
using SampleGame.Common;
using SampleGame.Gameplay;

namespace Game.Gameplay.SaveLoad
{
    public sealed class ResourceBagSerializer : ComponentSerializer<ResourceBag>
    {
        protected override string NodeKey => "resourceBag";

        protected override JObject DoOnSerialize(ResourceBag component) => new()
        {
            { "current", component.Current },
            { "type", (int)component.Type }
        };

        protected override void DoOnDeserialize(ResourceBag component, JObject data)
        {
            component.Current = data.Value<int>("current");
            component.Type = (ResourceType)data.Value<int>("type");
        }
    }
}