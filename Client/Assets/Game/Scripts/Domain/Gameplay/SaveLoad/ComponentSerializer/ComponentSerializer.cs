using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Game.Gameplay.SaveLoad
{
    public interface IComponentSerializer
    {
        bool TrySerialize(Component parent, out JObject result);
        void Deserialize(Component parent, JObject data);
    }

    public abstract class ComponentSerializer<T> : IComponentSerializer
        where T : Component
    {
        bool IComponentSerializer.TrySerialize(Component parent, out JObject result)
        {
            result = null;
            if (!parent.TryGetComponent<T>(out var component)) return false;
            result = new JObject { { NodeKey, DoOnSerialize(component) } };
            return true;
        }

        void IComponentSerializer.Deserialize(Component parent, JObject data)
        {
            if (!parent.TryGetComponent<T>(out var component)) return;
            if (data.TryGetValue(NodeKey, out var nodeValue) && nodeValue is JObject node)
            {
                DoOnDeserialize(component, node);
            }
        }

        protected abstract string NodeKey { get; }
        protected abstract JObject DoOnSerialize(T component);
        protected abstract void DoOnDeserialize(T component, JObject data);
    }
}