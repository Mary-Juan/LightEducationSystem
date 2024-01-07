using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightEducationSystem.DataLayer.Context
{
    public interface ISerialization<T>
    {
        public bool SerializeList(string filePath, List<T> objects);
        public List<T> Deserializelist(string filePath);
        public string SerializeObject(T obj);
        public T DeSerializeToObject(string json);




    }
}
