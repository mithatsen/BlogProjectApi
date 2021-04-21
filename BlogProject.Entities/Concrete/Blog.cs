using BlogProject.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogProject.Entities.Concrete
{
    public class Blog : ITable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ShortDescription { get; set; }

        public string Description { get; set; }
        public string ImagePath { get; set; }
        public DateTime PostedTime { get; set; } = DateTime.Now;

        public List<Comment> Comments { get; set; }

        public virtual List<CategoryBlog> CategoryBlogs { get; set; }
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }


    }
}
