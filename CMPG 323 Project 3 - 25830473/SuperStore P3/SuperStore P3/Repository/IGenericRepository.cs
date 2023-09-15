using System.Linq.Expressions;

namespace EcoPower_Logistics.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        //IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        //void AddRange(IEnumerable<T> entities);
        //void Remove(T entity);
        //void RemoveRange(IEnumerable<T> entities);

        IQueryable<T> GetAll();
        T GetById(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(int id);
        //bool Exists(T id);
        void Save();
    }

}
