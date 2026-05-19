using Newtonsoft.Json.Linq;
using SampleGame.Common;
using SampleGame.Gameplay;

namespace Game.Gameplay.SaveLoad
{
    public sealed class ResourceBagSerializer : ComponentSerializer<ResourceBag>
    {
        protected override string NodeKey => "ResourceBag";

        protected override JObject DoOnSerialize(ResourceBag component) => new()
        {
            { "current", component.Current },
            { "type", (int)component.Type }
        };

        protected override void DoOnDeserialize(ResourceBag component, JObject data)
        {
            component.Current = int.Parse(data["current"].ToString());
            component.Type = (ResourceType)int.Parse(data["type"].ToString());
        }
    }
}