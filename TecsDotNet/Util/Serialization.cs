using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TecsDotNet.Util
{
    public class Serialization
    {
        public static T DeepCopy<T>(T obj)
        {
            MemoryStream m = new MemoryStream();
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(m, obj);
            m.Position = 0;

            return (T)b.Deserialize(m);
        }
    }
}
