using System;
using System.Collections.Generic;

namespace TecsDotNet
{
    [Serializable]
    public class Entity : Prototype
    {
        #region Fields
            
        public static uint ID_COUNTER = 0;
        private Dictionary<Type, Component> components;

        #endregion

        #region Properties
            
        public uint ID { get; internal set; }
        public Dictionary<Type, Component> Components
        {
            get { return components; }
        }

        #endregion

        #region Methods

        public Entity()
        {
            components = new Dictionary<Type, Component>();
        }

        public void AddComponent(Component component)
        {
            components.Add(component.GetType(), component);
        }

        public bool RemoveComponent(Component component)
        {
            return components.Remove(component.GetType());
        }

        public T Get<T>()
        {
            return (T)(object)components[typeof(T)];
        }

        public bool HasComponent<T>()
        {
            return components.ContainsKey(typeof(T));
        }

        #endregion
    }
}
