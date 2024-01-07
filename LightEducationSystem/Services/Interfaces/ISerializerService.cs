namespace LightEducationSystem.Services.Interfaces
{
    public interface ISerializerService<T>
    {
        public string[] SerializeList( List<T> objects);
        public List<T> Deserializelist(string[] content);
        public string SerializeObject(T obj);
        public T DeSerializeToObject(string jsonObj);
    }
}
