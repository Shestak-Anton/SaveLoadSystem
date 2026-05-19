using Newtonsoft.Json.Linq;
using SampleGame.Common;
using SampleGame.Gameplay;

namespace Game.Gameplay.SaveLoad
{
    public sealed class TeamSerializer : ComponentSerializer<Team>
    {
        protected override string NodeKey => "Team";

        protected override JObject DoOnSerialize(Team component) => new()
        {
            { "value", (int)component.Type }
        };

        protected override void DoOnDeserialize(Team component, JObject data)
        {
            component.Type = (TeamType)int.Parse(data["value"].ToString());
        }
    }
}