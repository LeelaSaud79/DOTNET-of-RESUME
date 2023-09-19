using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resume.Data;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Resume.Repositories
{
    public class GenericRepos : IGenericRepos
    {
        private readonly ResumeContext _context;
        public GenericRepos(ResumeContext context) {
            _context = context;
        }

        //public async Task<T> Login<T>(Expression<Func<T, bool>> ForUser) where T : class
        //{
        //    return await _context.Set<T>().Where(ForUser).FirstOrDefaultAsync();
        //}
        public async Task<T> Login<T>(Expression<Func<T, bool>> ForUser) where T : class
        {
            if (_context.Set<T>() == null)
            {
                throw new Exception("Not Found");
            }

            return await this._context.Set<T>().Where(ForUser).FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAll<T>() where T : class {
            return await this._context.Set<T>().ToListAsync();
        }
        public async Task<List<T>> GetByUserId<T>(Expression<Func<T, bool>> forUser) where T : class
        {
            return await this._context.Set<T>().Where(forUser).ToListAsync();
        }
        public async Task<T> GetById<T>(int id) where T : class
        {
            return await this._context.Set<T>().FindAsync(id);
        }
        public async Task<T> Create<T>(T data) where T : class
        {
            this._context.Set<T>().Add(data);
            await this._context.SaveChangesAsync();
            return null;

        }
        public async Task<T> Update<T>(int id, T tObj) where T : class
        {
            var info = await _context.Set<T>().FindAsync(id);
            if (info == null)
            {
                throw new Exception();
            }
            _context.Set<T>().Update(info);
            await _context.SaveChangesAsync();
            return tObj;
       
        }

        public async Task<T> Delete<T>(int id) where T : class
        {
            var skill = await _context.Set<T>().FindAsync(id);
            if (skill == null)
            {
                return null;
            }

            _context.Remove(skill);
            await _context.SaveChangesAsync();

            return skill;
        }
    }
}
