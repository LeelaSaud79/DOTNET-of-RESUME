using Resume.Data;
using System.Linq.Expressions;

namespace Resume.Repositories
{
    public interface IGenericRepos
    {
        //Task<T> Login<T>(Expression<Func<T, bool>> validEmail) where T : class;
        Task<T> Login<T>(Expression<Func<T, bool>> ForUser) where T : class;
        Task<List<T>> GetAll<T>() where T : class;
        Task<T> GetById<T>(int id) where T : class;
        Task<List<T>> GetByUserId<T>(Expression<Func<T, bool>> forUser) where T : class;
        Task<T> Create<T>(T data) where T : class;
        Task<T> Update<T>(int id, T tobj) where T : class;
        Task<T> Delete<T>(int id) where T : class;
    }
}
 