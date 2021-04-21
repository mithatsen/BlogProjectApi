using BlogProject.DataAccess.Concrete.EntityFreamworkCore.Context;
using BlogProject.DataAccess.Interfaces;
using BlogProject.DTO.DTOs.BlogDtos;
using BlogProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.DataAccess.Concrete.EntityFreamworkCore.Repositories
{
    public class EfBlogRepository : EfGenericRepository<Blog>, IBlogDal
    {
        public async Task<List<Blog>> GetAllByCategoryIdAsync(int categoryId) // category ıdsine göre blogları listeler
        {
            using var context = new BlogProjectContext();
            var blogs = await context.Blogs.FromSqlRaw("select  Blogs.Id as Id, Blogs.Title as Title ," +
            "Blogs.ShortDescription as ShortDescription, Blogs.Description as 'Description', Blogs.ImagePath as ImagePath,"+
            "Blogs.PostedTime as PostedTime,Blogs.AppUserId as AppUserId,Blogs.Author as Author " +
            "from CategoryBlogs " +
            "inner join Blogs on CategoryBlogs.BlogId = Blogs.Id " +
            $"where CategoryBlogs.CategoryId ={categoryId}").ToListAsync();

            return blogs;
        }
        public async Task<List<Category>> GetCategoriesAsync(int blogId)
        {
            using var context = new BlogProjectContext();
            var categories = await context.Categories.FromSqlRaw($"Select Categories.*from Categories,CategoryBlogs where Categories.Id = CategoryBlogs.CategoryId and CategoryBlogs.BlogId = {blogId}").ToListAsync();
            return categories;
        }

        public async Task<List<Blog>> GetLastFiveBlogs()
        {
            using var context = new BlogProjectContext();
            var blogs = await context.Blogs.OrderByDescending(p => p.PostedTime).Take(5).ToListAsync();
            return blogs;
        }

        public List<Blog> Pagination(string searchWord, int size, int activePage = 1)
        {
            using var context = new BlogProjectContext();

            var result = context.Blogs.OrderByDescending(p => p.PostedTime).ToList();


            if (!string.IsNullOrWhiteSpace(searchWord))  //sayfada find işlemi işçüin gerekli
            {
                result= context.Blogs.Where(I => I.Title.ToLower().Contains(searchWord.ToLower()) || I.ShortDescription.ToLower().Contains(searchWord.ToLower()) || I.Description.ToLower().Contains(searchWord.ToLower())).OrderByDescending(p=>p.PostedTime).ToList();
             
            }
            result = result.Skip((activePage - 1) * size).Take(size).ToList();

            return result;

        }

        public async Task<List<Blog>> SearchAsync(string searchString)
        {
            using var context = new BlogProjectContext();
            var blogs = await context.Blogs.Where(p => p.Description.Contains(searchString) || p.ShortDescription.Contains(searchString) || p.Title.Contains(searchString)).OrderByDescending(p => p.PostedTime).ToListAsync();
            return blogs;
        }
    }
}
