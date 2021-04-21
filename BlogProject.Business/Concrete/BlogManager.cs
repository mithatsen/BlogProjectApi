using BlogProject.Business.Interfaces;
using BlogProject.DataAccess.Interfaces;
using BlogProject.DTO.DTOs.BlogDtos;
using BlogProject.DTO.DTOs.CategoryBlogDtos;
using BlogProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Business.Concrete
{
    public class BlogManager : GenericManager<Blog>, IBlogService
    {
        private readonly IGenericDal<Blog> _genericDal;
        private readonly IBlogDal _blogDal;
        private readonly IGenericDal<CategoryBlog> _categoryBlogService;
        public BlogManager(IGenericDal<Blog> genericDal, IGenericDal<CategoryBlog> categoryBlogService, IBlogDal blogDal) : base(genericDal)
        {
            _genericDal = genericDal;
            _categoryBlogService = categoryBlogService;
            _blogDal = blogDal;
        }

        public async Task AddToCategoryAsync(CategoryBlogDto categoryBlogDto)
        {
            var temp = await _categoryBlogService.GetAsync(p => p.CategoryId == categoryBlogDto.CategoryId && p.BlogId == categoryBlogDto.BlogId);
            if (temp == null)
            {
                await _categoryBlogService.AddAsync(new CategoryBlog { BlogId = categoryBlogDto.BlogId, CategoryId = categoryBlogDto.CategoryId });
            }
            
        }

        public Task<List<Blog>> GetAllByCategoryIdAsync(int categoryId)
        {
            return _blogDal.GetAllByCategoryIdAsync(categoryId);
        }

        public async Task<List<Blog>> GetAllSortedByPostedTimeAsync()
        {
            return await _genericDal.GetAllAsync(I => I.PostedTime);
        }

        public async Task<List<Category>> GetCategoriesAsync(int blogId)
        {
            return await _blogDal.GetCategoriesAsync(blogId);
        }

        public Task<List<Blog>> GetLastFiveBlogs()
        {
            return _blogDal.GetLastFiveBlogs();
        }

        public List<Blog> Pagination( string searchWord, int size, int activePage = 1)
        {
            
            return _blogDal.Pagination(searchWord,size,activePage);
        }

        public async Task RemoveFromCategoryAsync(CategoryBlogDto categoryBlogDto)
        {

            var temp = await _categoryBlogService.GetAsync(p => p.CategoryId == categoryBlogDto.CategoryId && p.BlogId == categoryBlogDto.BlogId);
            if (temp != null)
            {
                await _categoryBlogService.RemoveAsync(temp.Id);
            }
            
        }

        public async  Task<List<Blog>> SearchAsync(string searchString)
        {
            return await _blogDal.SearchAsync(searchString);
        }
    }
}
