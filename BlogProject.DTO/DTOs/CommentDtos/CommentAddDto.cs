using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.DTO.DTOs.CommentDtos
{
    public class CommentAddDto
    {
    
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string Description { get; set; }
        public DateTime PostedTime { get; set; } = DateTime.Now;
        public int BlogId { get; set; }
        public int? ParentCommentId { get; set; } // parentıd nullsa bu yorum direk bloğa yapılmıştır, null değilse bi yoruma yapılmıştır.     
    }
}
