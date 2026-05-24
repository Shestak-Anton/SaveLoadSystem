using System;
using Newtonsoft.Json;
using SampleGame.Common;
using SampleGame.Gameplay;

namespace Game.Gameplay.SaveLoad
{
    public sealed class TeamSerializer : ComponentSerializer<Team, TeamSerializer.Data>
    {
        [Serializable]
        public record Data(
            [property: JsonProperty("value")] TeamType Type
        );

        protected override string NodeKey => "team";

        protected override Data DoOnSerialize(Team component) => new(component.Type);

        protected override void DoOnDeserialize(Team component, Data data) => component.Type = data.Type;
    }
}