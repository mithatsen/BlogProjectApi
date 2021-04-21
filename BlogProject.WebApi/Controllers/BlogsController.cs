using AutoMapper;
using BlogProject.Business.Interfaces;
using BlogProject.DTO.DTOs.BlogDtos;
using BlogProject.DTO.DTOs.CategoryBlogDtos;
using BlogProject.DTO.DTOs.CategoryDtos;
using BlogProject.DTO.DTOs.CommentDtos;
using BlogProject.Entities.Concrete;
using BlogProject.WebApi.CustomFilters;
using BlogProject.WebApi.Enums;
using BlogProject.WebApi.Models;
using JWTProject.Api.CustomFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlogProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : BaseController
    {
        private readonly IBlogService _blogService;
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        private readonly IMemoryCache _memoryCache;

        public BlogsController(IBlogService blogService, IMapper mapper, ICommentService commentService, IMemoryCache memoryCache)
        {
            _blogService = blogService;
            _mapper = mapper;
            _commentService = commentService;
            _memoryCache = memoryCache;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() //sürekli çekileceğine memory cavhe e at
        {
            //if(_memoryCache.TryGetValue("blogList",out List<BlogListDto> blogs))
            //{
            //    return Ok(blogs);
            //}
            
            var blogs = _mapper.Map<List<BlogListDto>>(await _blogService.GetAllSortedByPostedTimeAsync());
            //Thread.Sleep(10000);

            //_memoryCache.Set("blogList", blogs, new MemoryCacheEntryOptions
            //{
            //    AbsoluteExpiration = DateTime.Now.AddDays(1),
            //    Priority=CacheItemPriority.Normal
            //}) ; // CACHELENDİ

            return Ok(blogs);
        }

        
        [HttpGet("[action]")]
        public  IActionResult Pagination([FromQuery] string searchWord, [FromQuery] int size, [FromQuery] int activePage = 1) //sürekli çekileceğine memory cavhe e at
        {

            var blogs = _mapper.Map<List<BlogListDto>>(_blogService.Pagination( searchWord, size, activePage));

            return Ok(blogs);

        }

        [HttpGet("{id}")]
        [ServiceFilter(typeof(ValidId<Blog>))]
        public async Task<IActionResult> GetById(int id)
        {
            var blog = _mapper.Map<BlogListDto>(await _blogService.FindByIdAsync(id));
            return Ok(blog);
        }
        [HttpPost]
        [Authorize]
        [ValidModel]

        public async Task<IActionResult> Create([FromForm] BlogAddModel blogAddModel)
        {
            var contentType = "";
            if(blogAddModel.Image != null)
            {
                if (blogAddModel.Image.ContentType.Contains("image"))
                {
                    contentType = blogAddModel.Image.ContentType;
                }
                else { return BadRequest(); }
            }
            

            var uploadModel = await UploadFileAsync(blogAddModel.Image, contentType);
            if (uploadModel.uploadState == UploadState.Success)
            {
                blogAddModel.ImagePath = uploadModel.NewName;
                await _blogService.AddAsync(_mapper.Map<Blog>(blogAddModel));
                return Created("", blogAddModel);
            }
            else if (uploadModel.uploadState == UploadState.NotExist)
            {

                await _blogService.AddAsync(_mapper.Map<Blog>(blogAddModel));
                return Created("", blogAddModel);
            }
            else
            {
                return BadRequest(uploadModel.ErrorMessage);
            }




        }
        [Authorize]
        [HttpPut("{id}")]
        [ValidModel]
        [ServiceFilter(typeof(ValidId<Blog>))]
        public async Task<IActionResult> Update(int id, [FromForm] BlogUpdateModel blogUpdateModel)
        {
            if (id != blogUpdateModel.Id)
            {
                return BadRequest("gecersiz id");
            }
            
            var contentType = "";
            if (blogUpdateModel.Image != null)
            {
                if (blogUpdateModel.Image.ContentType.Contains("image"))
                {
                    contentType = blogUpdateModel.Image.ContentType;
                }
                else { return BadRequest(); }
             
            }
            else { contentType = ""; }


            var uploadModel = await UploadFileAsync(blogUpdateModel.Image, contentType);

            if (uploadModel.uploadState == UploadState.Success)
            {
                var updatedBlog = await _blogService.FindByIdAsync(blogUpdateModel.Id);
                updatedBlog.ShortDescription = blogUpdateModel.ShortDescription;
                updatedBlog.Title = blogUpdateModel.Title;
                updatedBlog.Description = blogUpdateModel.Description;
                updatedBlog.AppUserId = blogUpdateModel.AppUserId;
                updatedBlog.ImagePath = uploadModel.NewName;

                await _blogService.UpdateAsync(updatedBlog);
                return NoContent();
            }
            else if (uploadModel.uploadState == UploadState.NotExist)
            {
                var updatedBlog = await _blogService.FindByIdAsync(blogUpdateModel.Id);
                updatedBlog.ShortDescription = blogUpdateModel.ShortDescription;
                updatedBlog.Title = blogUpdateModel.Title;
                updatedBlog.Description = blogUpdateModel.Description;
                updatedBlog.AppUserId = blogUpdateModel.AppUserId;

                await _blogService.UpdateAsync(updatedBlog);
                return NoContent();
            }
            else
            {
                return BadRequest(uploadModel.ErrorMessage);
            }
        }
        [HttpDelete("{id}")]
        [Authorize]
        [ServiceFilter(typeof(ValidId<Blog>))]
        public async Task<IActionResult> Delete(int id)
        {
            await _blogService.RemoveAsync(id);
            return NoContent();
        }
        [HttpPost("[action]")]
        [Authorize]
        [ValidModel]
        public async Task<IActionResult> AddToCategory(CategoryBlogDto categoryBlogDto)
        {
            await _blogService.AddToCategoryAsync(categoryBlogDto);
            return Created("", categoryBlogDto);
        }
        [HttpDelete("[action]")]
        [Authorize]
        [ValidModel]
        public async Task<IActionResult> RemoveFromCategory([FromQuery] CategoryBlogDto categoryBlogDto)
        {
            await _blogService.RemoveFromCategoryAsync(categoryBlogDto);
            return NoContent();
        }
        [HttpGet("[action]/{id}")]
        [ServiceFilter(typeof(ValidId<Category>))]
        public async Task<IActionResult> GetAllByCategoryId(int id)
        {
            var temp = await _blogService.GetAllByCategoryIdAsync(id);
            return Ok(temp);
        }

        [HttpGet("{id}/[action]")]
        [ServiceFilter(typeof(ValidId<Blog>))]
        public async Task<IActionResult> GetCategories(int id)
        {
            var temp = _mapper.Map<List<CategoryListDto>>(await _blogService.GetCategoriesAsync(id));

            return Ok(temp);
        }

        [HttpGet("[action]")]

        public async Task<IActionResult> GetLastFiveBlogs()
        {
            var temp = _mapper.Map<List<BlogListDto>>(await _blogService.GetLastFiveBlogs());

            return Ok(temp);
        }
        [HttpGet("{id}/[action]")]

        public async Task<IActionResult> GetComments([FromRoute] int id, [FromQuery] int? parentCommentId)
        {
            return Ok(_mapper.Map<List<CommentListDto>>(await _commentService.GetAllWithSubCommentsAsync(id, parentCommentId)));
        }
        [HttpGet("[action]")]

        public async Task<IActionResult> Search([FromQuery] string s)
        {
            var temp = _mapper.Map<List<BlogListDto>>(await _blogService.SearchAsync(s));
            if (temp.Count == 0)
            {
                return NotFound();
            }
            return Ok(temp);
        }
        [HttpPost("[action]")]
        [ValidModel]

        public async Task<IActionResult> AddComment(CommentAddDto commentAddDto)
        {
            commentAddDto.PostedTime = DateTime.Now;
            await _commentService.AddAsync(_mapper.Map<Comment>(commentAddDto));
            return Created("", commentAddDto);
        }
    }
}
