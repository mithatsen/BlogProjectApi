using BlogProject.DataAccess.Concrete.EntityFreamworkCore.Context;
using BlogProject.DataAccess.Interfaces;
using BlogProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.DataAccess.Concrete.EntityFreamworkCore.Repositories
{
    public class EfCategoryRepository : EfGenericRepository<Category>, ICategoryDal
    {
        public async Task<List<Category>> GetAllWithCategoryBlogsAsync()
        {
            using var context = new BlogProjectContext();
            return await context.Categories.OrderByDescending(p=>p.Id).Include(p => p.CategoryBlogs).ToListAsync();
          
            
            //var blogs = await context.Categories
            //.FromSqlRaw("select Categories.Name,COUNT(CategoryBlogs.CategoryId) from CategoryBlogs, Categories where CategoryBlogs.CategoryId = Categories.Id GROUP BY Categories.Name")
            //.ToListAsync();
            
        }
    }
}
