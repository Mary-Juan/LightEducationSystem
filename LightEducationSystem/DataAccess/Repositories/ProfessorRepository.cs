using LightEducationSystem.DataAccess.Repositories.Interfaces;
using LightEducationSystem.Entities;
using LightEducationSystem.Services;
using LightEducationSystem.Services.Interfaces;
using LightEducationSystem.Utilities;
using Microsoft.Win32;
using System.Data;

namespace LightEducationSystem.DataAccess.Repositories
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly ISerializerService<Professor> _serializerService;
        private readonly List<Professor> _objects;
        private readonly string _filePath;

        public ProfessorRepository(string filePath)
        {
            _filePath = filePath;
            _serializerService = new JsonSerilizerService<Professor>();
            string[] lines = FileHandler.ReadFile(_filePath);
            _objects = _serializerService.Deserializelist(lines);
        }

        public Professor Create(Professor obj)
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
                Professor obj = GetByID(id);
                obj.IsDeleted = true;
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Professor> GetAll()
        {
            return _objects;
        }

        public Professor GetByID(int id)
        {
            return _objects.FirstOrDefault(o => o.Id == id && o.IsDeleted == false);
        }

        public void SaveChanges()
        {
            string[] lines = _serializerService.SerializeList(_objects);
            FileHandler.SaveToFile(lines, _filePath);
        }

        public Professor Update(Professor obj)
        {
            try
            {
                Professor professor = GetByID(obj.Id);
                professor.UserName = obj.UserName;
                professor.Password = obj.Password;
                professor.Email = obj.Email;
                professor.role = new PersonRole()
                {
                    Id = 1,
                    Title = "Professor"
                };

                SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
