using System;
using Newtonsoft.Json.Linq;
using SampleGame.Common;
using SampleGame.Gameplay;
using UnityEngine;

namespace Game.Gameplay.SaveLoad
{
    public sealed class DestinationPointSerializer : ComponentSerializer<DestinationPoint>
    {
        protected override string NodeKey => "destinationPoint";

        protected override JObject DoOnSerialize(DestinationPoint component) =>
            new()
            {
                { "value", JToken.FromObject(new SerializedVector3(component.Value)) }
            };

        protected override void DoOnDeserialize(DestinationPoint component, JObject data)
        {
            var position = data["value"].ToObject<SerializedVector3>();
            component.Value = position;
        }
    }
}