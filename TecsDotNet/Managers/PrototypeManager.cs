using System;
using System.Collections.Generic;
using System.Linq;
using TecsDotNet.Util;
using TecsDotNet.Json;

namespace TecsDotNet.Managers
{
    public class PrototypeManager : Dictionary<string, Prototype>, IManager
    {
        #region Properties

        public World World { get; private set; }

        public new Prototype this[string key]
        {
            get { return Serialization.DeepCopy<Prototype>(base[key]); }
        }
        
        #endregion

        #region Methods

        public PrototypeManager(World world)
        {
            World = world;
        }

        public new void Add(string key, Prototype p)
        {
            if (p == null)
                throw new ArgumentNullException("p", "The Prototype added must not be null");

            if (String.IsNullOrWhiteSpace(p.PrototypeID))
            {
                p.PrototypeID = key;
            }

            base.Add(key, p);
        }

        public List<T> GetAll<T>()
        {
            List<Prototype> matches = Values.ToList().FindAll(delegate(Prototype p) { return p.GetType().Equals(typeof(T)); });
            List<T> list = new List<T>();

            foreach (Prototype p in matches)
                list.Add((T)(object)p);

            return list;
        }

        public void LoadFromFile(JsonPrototypeLoader loader, string path, bool list = false)
        {
            loader.Library = this;
            loader.LoadFile(path, list);

            foreach (Prototype p in loader.Prototypes)
                Add(p.PrototypeID, p);
        }

        #endregion
    }
}
