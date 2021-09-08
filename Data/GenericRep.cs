using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using sius_server.Data.Interfaces;
using sius_server.Models;

namespace sius_server.Data
{
    public class GenericRep<T> : IGenericRep<T> where T : BaseEntity
    {
        private readonly DataContext _context;
        
        public GenericRep(DataContext context)
        {
            _context = context;
        }
        
        public async Task<T> CreateOne(T model)
        {
            await _context.Set<T>().AddAsync(model);
            await _context.SaveChangesAsync();
            
            return model;
        }    

        public async Task<List<T>> CreateMany(List<T> models)
        {
            await _context.Set<T>().AddRangeAsync(models);
            await _context.SaveChangesAsync();

            return models;
        }

        public Task<List<T>> EditMany(List<T> models)
        {
            throw new System.NotImplementedException();
        }

        public async Task<T> DeleteOne(int id)
        {
            var model = await _context.Set<T>().FindAsync(id);
            
            if(model == null)
                return null;

            _context.Set<T>().Remove(model);
            await _context.SaveChangesAsync();

            return model;
        }           
        public async Task<T> EditOne(T model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return model;
        }
        
        public async Task<ICollection<T>> GetAll()
        {
            var model = await _context.Set<T>().ToListAsync();

            return model;
        }

        Task<T> IGenericRep<T>.EditOne(T model)
        {
            throw new System.NotImplementedException();
        }

        public async Task<T> GetOneById(int id)
        {
            var model = await _context.Set<T>().FindAsync(id);

            return model;
        }  
    }
}