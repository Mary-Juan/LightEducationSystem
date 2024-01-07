using LightEducationSystem.DataAccess.Repositories.Interfaces;
using LightEducationSystem.Entities;
using LightEducationSystem.Services;
using LightEducationSystem.Services.Interfaces;
using LightEducationSystem.Utilities;

namespace LightEducationSystem.DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ISerializerService<T> _serializerService;
        private readonly List<T> _objects;
        private readonly string _filePath;

        public GenericRepository(string filePath)
        {
            _filePath = filePath;
            _serializerService = new JsonSerilizerService<T>();
            string[] lines = FileHandler.ReadFile(_filePath);
            _objects = _serializerService.Deserializelist(lines);
        }
        public T Create(T obj)
        {
            try
            {
                _objects.Add(obj);
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                T obj = GetByID(id);
                obj.IsDeleted = true;
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<T> GetAll()
        {
            return _objects;
        }

        public T GetByID(int id)
        {
            return _objects.FirstOrDefault(o => o.Id == id);
        }

        public void SaveChanges()
        {
            string[] lines = _serializerService.SerializeList(_objects);
            FileHandler.SaveToFile(lines,_filePath);
        }

        public T Update(T obj)
        {
            try
            {
                T target = GetByID(obj.Id);
                _objects.Remove(target);
                _objects.Add(obj);
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
