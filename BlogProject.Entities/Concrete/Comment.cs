using BlogProject.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Entities.Concrete
{
    public class Comment : ITable
    {
        public int Id { get; set; }
        public string  AuthorName { get; set; }
        public string  AuthorEmail { get; set; }
        public string Description { get; set; }
        public DateTime PostedTime { get; set; } = DateTime.Now;
        public int BlogId { get; set; }
        public Blog Blog { get; set; }

        public int? ParentCommentId { get; set; } // parentıd nullsa bu yorum direk bloğa yapılmıştır, null değilse bi yoruma yapılmıştır.
        public Comment ParentComment { get; set; }
        public List<Comment> SubComments { get; set; }
    }
}
