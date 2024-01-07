namespace LightEducationSystem.DataAccess.Repositories.Interfaces
{
    public interface IGenericRepository<T>
    {
        public T Create(T obj);
        public T Update(T obj);
        public bool Delete(int id);
        public List<T> GetAll();
        public T GetByID(int id);
        public void SaveChanges();
    }
}
