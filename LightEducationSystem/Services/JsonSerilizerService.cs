using LightEducationSystem.Services.Interfaces;
using Newtonsoft.Json;

namespace LightEducationSystem.Services
{
    public class JsonSerilizerService<T> : ISerializerService<T>
    {
        public List<T> Deserializelist(string[] content)
        {
            List<T> objects = new List<T>();

            foreach(string line in content)
            {
                objects.Add(DeSerializeToObject(line));
            }

            return objects;
        }

        public T DeSerializeToObject(string jsonObj)
        {
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects
            };

            return JsonConvert.DeserializeObject<T>(jsonObj, settings);
        }

        public string[] SerializeList(List<T> objects)
        {
            string[] content = new string[0]; ;

            foreach(T obj in objects)
            {
                content =content.Append(SerializeObject(obj)).ToArray();
            }

            return content;
        }

        public string SerializeObject(T obj)
        {
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects
            };

            return JsonConvert.SerializeObject(obj, settings);
        }
    }
}
