using BlogProject.DataAccess.Concrete.EntityFreamworkCore.Context;
using BlogProject.DataAccess.Interfaces;
using BlogProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.DataAccess.Concrete.EntityFreamworkCore.Repositories
{
    public class EfCommentRepository : EfGenericRepository<Comment>, ICommentDal
    {
        public async  Task<List<Comment>> GetAllWithSubCommentsAsync(int blogId, int? parentId)
        {
            List<Comment> result = new List<Comment>();
            await GetComments(blogId, parentId, result);
            return result;
        }

        private async Task GetComments(int blogId, int? parentId, List<Comment> result)
        {
            using var context = new BlogProjectContext();
            var comments = await context.Comments.Where(p => p.BlogId == blogId && p.ParentCommentId == parentId).OrderByDescending(p => p.PostedTime).ToListAsync();
            if (comments.Count > 0)
            {
                foreach(var comment in comments)
                {
                    if (comment.SubComments == null)
                        comment.SubComments = new List<Comment>();

                    await (GetComments(comment.BlogId, comment.Id, comment.SubComments));

                    if (!result.Contains(comment))
                        result.Add(comment);
                }
            }
        }
    }
}
