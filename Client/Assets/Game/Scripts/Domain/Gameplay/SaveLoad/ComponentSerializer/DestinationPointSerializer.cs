using System;
using Newtonsoft.Json.Linq;
using SampleGame.Common;
using SampleGame.Gameplay;
using UnityEngine;

namespace Game.Gameplay.SaveLoad
{
    public sealed class DestinationPointSerializer : ComponentSerializer<DestinationPoint>
    {
        protected override string NodeKey => "DestinationPoint";

        protected override JObject DoOnSerialize(DestinationPoint component) =>
            new() { { "value", JToken.FromObject(new SerializedVector3(component.Value)) } };

        protected override void DoOnDeserialize(DestinationPoint component, JObject data)
        {
            try
            {
                var destination = data["value"];
                var x = float.TryParse(destination["x"].ToString(), out var xx) ? xx : 0f;
                var y = float.TryParse(destination["y"].ToString(), out var yy) ? yy : 0f;
                var z = float.TryParse(destination["z"].ToString(), out var zz) ? zz : 0f;
                component.Value = new Vector3(x, y, z);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}