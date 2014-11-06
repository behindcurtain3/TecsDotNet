using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TecsDotNet.Json.Converters;
using TecsDotNet.Managers;

namespace TecsDotNet.Json
{
    public class JsonEntityLoader : JsonPrototypeLoader
    {
        public ComponentConverter Converter { get; set; }

        public JsonEntityLoader()
            : base()
        {
            Identifier = "Prototypes";
        }

        public override Prototype LoadPrototype(JObject source)
        {
            Entity e = new Entity();
            e.PrototypeID = (string)source["PrototypeID"];

            // Get the components from the json
            IEnumerable<JObject> components = source["Components"].ToObject<IEnumerable<JObject>>();

            foreach (JObject o in components)
            {
                Component c = JsonConvert.DeserializeObject<Component>(o.ToString(), Converter);

                if (c != null)
                {
                    e.AddComponent(c);
                    ComponentLoaded(e, c, o);
                }
            }

            return e;
        }

        public virtual void ComponentLoaded(Entity e, Component c, JObject o)
        {
        }
    }
}
