using AutoMapper;
using BlogProject.DTO.DTOs.AppUserDtos;
using BlogProject.DTO.DTOs.BlogDtos;
using BlogProject.DTO.DTOs.CategoryDtos;
using BlogProject.DTO.DTOs.CommentDtos;
using BlogProject.Entities.Concrete;
using BlogProject.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.WebApi.Mapping.AutoMapperProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            #region Category-CategoryAddDto
            CreateMap<CategoryAddDto, Category>();
            CreateMap<Category, CategoryAddDto>();
            #endregion

            #region Category-CategoryListDto
            CreateMap<CategoryListDto, Category>();
            CreateMap<Category, CategoryListDto>();
            #endregion

            #region Category-CategoryUpdateDto
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<Category, CategoryUpdateDto>();
            #endregion

            #region Blog-BlogListDto
            CreateMap<Blog, BlogListDto>();
            CreateMap<BlogListDto, Blog>();
            #endregion

            #region Blog-BlogUpdateModal
            CreateMap<Blog, BlogUpdateModel>();
            CreateMap<BlogUpdateModel, Blog>();
            #endregion

            #region Blog-BlogAddModel
            CreateMap<Blog, BlogAddModel>();
            CreateMap<BlogAddModel, Blog>();
            #endregion

            #region AppUser-AppUserLoginDto
            CreateMap<AppUser, AppUserLoginDto>();
            CreateMap<AppUserLoginDto, AppUser>();
            #endregion
            #region Comment-CommentListDto
            CreateMap<Comment, CommentListDto>();
            CreateMap<CommentListDto, Comment>();
            #endregion
            #region Comment-CommentAddDto
            CreateMap<Comment, CommentAddDto>();
            CreateMap<CommentAddDto, Comment>();
            #endregion

        }


    }
}
