using System;
using Newtonsoft.Json.Linq;

namespace TecsDotNet.Json.Converters
{
    public class ComponentConverter : JsonCreationConverter<Component>
    {
        protected override Component Create(Type objectType, JObject jObject)
        {
            return new Component();
        }
    }
}
