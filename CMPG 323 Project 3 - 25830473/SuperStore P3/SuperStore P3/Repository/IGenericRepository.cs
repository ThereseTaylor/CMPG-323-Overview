using System.Linq.Expressions;

namespace EcoPower_Logistics.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(int? id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(int id);
        void Save();
    }

}
