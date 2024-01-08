using LightEducationSystem.DataAccess.Repositories.Interfaces;
using LightEducationSystem.Entities;
using LightEducationSystem.Services;
using LightEducationSystem.Services.Interfaces;
using LightEducationSystem.Utilities;

namespace LightEducationSystem.DataAccess.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ISerializerService<Student> _serializerService;
        private readonly List<Student> _objects;
        private readonly string _filePath;

        public StudentRepository(string filePath)
        {
            _filePath = filePath;
            _serializerService = new JsonSerilizerService<Student>();
            string[] lines = FileHandler.ReadFile(_filePath);
            _objects = _serializerService.Deserializelist(lines);
        }

        public Student Create(Student obj)
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
                Student obj = GetByID(id);
                obj.IsDeleted = true;
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Student> GetAll()
        {
            return _objects;
        }

        public Student GetByID(int id)
        {
            return _objects.FirstOrDefault(o => o.Id == id && o.IsDeleted != false);
        }

        public void SaveChanges()
        {
            string[] lines = _serializerService.SerializeList(_objects);
            FileHandler.SaveToFile(lines, _filePath);
        }

        public Student Update(Student obj)
        {
            try
            {
                Student student = GetByID(obj.Id);
                student.UserName = obj.UserName;
                student.Password = obj.Password;
                student.Email = obj.Email;
                student.role = new PersonRole()
                {
                    Id = 2,
                    Title = "Student"
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
