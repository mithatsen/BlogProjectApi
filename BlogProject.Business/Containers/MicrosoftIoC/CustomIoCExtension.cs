using BlogProject.Business.Concrete;
using BlogProject.Business.Interfaces;
using BlogProject.Business.Utilities.JWTUtility;
using BlogProject.Business.Utilities.LogTool;
using BlogProject.Business.ValidationRules.FluentValidation;
using BlogProject.DataAccess.Concrete.EntityFreamworkCore.Repositories;
using BlogProject.DataAccess.Interfaces;
using BlogProject.DTO.DTOs.AppUserDtos;
using BlogProject.DTO.DTOs.CategoryBlogDtos;
using BlogProject.DTO.DTOs.CategoryDtos;
using BlogProject.DTO.DTOs.CommentDtos;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Business.Containers.MicrosoftIoC
{
    public static class CustomIoCExtension
    {
        public static void AddContainerWithDependencies(this IServiceCollection services)
        {

            services.AddScoped(typeof(IGenericDal<>),typeof(EfGenericRepository<>));
            services.AddScoped(typeof(IGenericService<>),typeof(GenericManager<>));


            services.AddScoped<IAppUserService,AppUserManager>(); //dependency injection için gerekli
            services.AddScoped<IBlogService, BlogManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICommentService, CommentManager>();
            services.AddScoped<IJwtService, JWTManager>();

            services.AddScoped<IAppUserDal, EfAppUserRepository>(); //dependency injection için gerekli
            services.AddScoped<IBlogDal, EfBlogRepository>();
            services.AddScoped<ICategoryDal, EfCategoryRepository>();
            services.AddScoped<ICommentDal, EfCommentRepository>();


            services.AddScoped<ICustomLogger, NLogAdapter>();
            // services.AddContainerWithDependencies eklenecek startupa

            services.AddTransient<IValidator<AppUserLoginDto>, AppUserLoginDtoValidator>();
            services.AddTransient<IValidator<CategoryAddDto>, CategoryAddDtoValidator>();
            services.AddTransient<IValidator<CategoryBlogDto>,CategoryBlogDtoValidator>(); 
            services.AddTransient<IValidator<CategoryUpdateDto>, CategoryUpdateDtoValidator>();
            services.AddTransient<IValidator<CommentAddDto>, CommentAddValidator>();


        }
    }
}
