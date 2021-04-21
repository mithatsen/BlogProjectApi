using BlogProject.DataAccess.Interfaces;
using BlogProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.DataAccess.Concrete.EntityFreamworkCore.Repositories
{
    public class EfCategoryBlogRepository : EfGenericRepository<CategoryBlog>, ICategoryBlogDal
    {
    }
}
