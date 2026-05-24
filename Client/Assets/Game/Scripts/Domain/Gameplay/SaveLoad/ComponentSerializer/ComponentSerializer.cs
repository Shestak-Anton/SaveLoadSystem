using System;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Game.Gameplay.SaveLoad
{
    public interface IComponentSerializer
    {
        bool TrySerialize(Component parent, out string key, out JToken result);
        void Deserialize(Component parent, JObject data);
    }

    public abstract class ComponentSerializer<TComponent, TSerializedData> : IComponentSerializer
        where TComponent : Component
    {
        bool IComponentSerializer.TrySerialize(Component parent, out string key, out JToken result)
        {
            result = null;
            key = NodeKey;
            if (!parent.TryGetComponent<TComponent>(out var component)) return false;
            result = JToken.FromObject(DoOnSerialize(component));
            return result != null;
        }

        void IComponentSerializer.Deserialize(Component parent, JObject data)
        {
            if (!parent.TryGetComponent<TComponent>(out var component)) return;
            if (!data.TryGetValue(NodeKey, out var nodeValue)) return;
            if (nodeValue is not JObject node) return;

            try
            {
                DoOnDeserialize(component, node.ToObject<TSerializedData>());
            }
            catch (Exception exception)
            {
                Debug.Log($"Exception while reading node for component: {parent.name} - {component.GetType().Name}");
                Debug.LogError(exception.Message);
            }
        }

        protected abstract string NodeKey { get; }
        protected abstract TSerializedData DoOnSerialize(TComponent component);
        protected abstract void DoOnDeserialize(TComponent component, TSerializedData data);
    }
}