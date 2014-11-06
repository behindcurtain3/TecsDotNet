using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TecsDotNet.Managers;

namespace TecsDotNet.Json
{
    public class JsonPrototypeLoader
    {
        #region Properties

        public PrototypeManager Library { get; set; }
        public string Identifier { get; set; }
        public List<Prototype> Prototypes { get; private set; }

        #endregion

        #region Methods

        public JsonPrototypeLoader()
        {
            Identifier = "Prototypes";
            Prototypes = new List<Prototype>();
        }

        public void LoadFile(string path, bool list = false)
        {
            string json = File.ReadAllText(path);

            if (list)
            {
                JsonFileList jsonFiles = JsonConvert.DeserializeObject<JsonFileList>(json);
                foreach (string str in jsonFiles.Files)
                {
                    LoadFile(str);
                }
            }
            else
            {
                JObject file = JObject.Parse(json);
                foreach (JObject o in file[Identifier].ToObject<IEnumerable<JObject>>())
                {
                    Prototypes.Add(LoadPrototype(o));
                }
            }
        }

        public virtual Prototype LoadPrototype(JObject source)
        {
            Prototype p = new Prototype();
            p.PrototypeID = (string)source["PrototypeID"];

            return p;
        }

        #endregion
    }
}
