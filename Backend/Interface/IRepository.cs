namespace Backend.Interface;

public interface IRepository<T> where T : class
{
 T GetById(int id);
 IQueryable<T> GetAll();
 void Add(T entity);
 void Update(T entity);
 void Delete(T entity);
}