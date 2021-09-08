using System.Collections.Generic;
using System.Threading.Tasks;
using sius_server.Models;

namespace sius_server.Data.Interfaces
{
    public interface IGenericRep<T> where T : BaseEntity
    {
        Task<T> CreateOne(T model);
        Task<List<T>> CreateMany(List<T> models);
        Task<T> GetOneById(int id);
        Task<ICollection<T>> GetAll();
        Task<T> EditOne(T model);
        Task<List<T>> EditMany(List<T> models);
        Task<T> DeleteOne(int id);
    }
}