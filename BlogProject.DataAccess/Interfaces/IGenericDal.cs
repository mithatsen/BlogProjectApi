using BlogProject.Entities.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.DataAccess.Interfaces
{
    public interface IGenericDal<T> where T : class, ITable, new() 
    {
        Task AddAsync(T entity);
        Task RemoveAsync(int id);
        Task UpdateAsync(T entity);

        Task<T> FindByIdAsync(int id);
        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        Task<List<T>> GetAllAsync<TKey>(Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> keySelector);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T,bool>>filter);
        Task<List<T>> GetAllAsync<TKey>(Expression<Func<T, TKey>> keySelector);
    }
}
