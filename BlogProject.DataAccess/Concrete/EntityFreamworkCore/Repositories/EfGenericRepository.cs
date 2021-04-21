using BlogProject.DataAccess.Concrete.EntityFreamworkCore.Context;
using BlogProject.DataAccess.Interfaces;
using BlogProject.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.DataAccess.Concrete.EntityFreamworkCore.Repositories
{
    public class EfGenericRepository<T> : IGenericDal<T> where T : class, ITable, new()
    {
        public async Task AddAsync(T entity)
        {
            using var context = new BlogProjectContext();
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task<T> FindByIdAsync(int id)
        {
            using var context = new BlogProjectContext();
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            using var context = new BlogProjectContext();
            return await context.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetAllAsync<TKey>(Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> keySelector)
        { 
            using var context = new BlogProjectContext();
            return await context.Set<T>().Where(filter).OrderByDescending(keySelector).ToListAsync();
        }
        public async Task<List<T>> GetAllAsync<TKey>(Expression<Func<T, TKey>> keySelector) 
        {
            using var context = new BlogProjectContext();
            return await context.Set<T>().OrderByDescending(keySelector).ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T,bool>> filter)//ok
        {
            using var context = new BlogProjectContext();
            return await context.Set<T>().Where(filter).ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            using var context = new BlogProjectContext();
            return await context.Set<T>().FirstOrDefaultAsync(filter);
        }

        public async Task RemoveAsync(int id)
        {
            using var context = new BlogProjectContext();
            var temp = await context.Set<T>().FindAsync(id);
            context.Remove(temp);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {

            using var context = new BlogProjectContext();
            context.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
