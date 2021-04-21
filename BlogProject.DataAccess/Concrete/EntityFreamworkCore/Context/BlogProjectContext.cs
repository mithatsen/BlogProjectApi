

using BlogProject.DataAccess.Concrete.EntityFreamworkCore.Mapping;
using BlogProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BlogProject.DataAccess.Concrete.EntityFreamworkCore.Context
{
    public class BlogProjectContext :DbContext
    {
        //private readonly IConfiguration _configuration;
        //public BlogProjectContext(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}
        //bu üsteeki kısım da 27. klasör 5. video

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(_configuration.GetConnectionString("db1"));      //27. klaasör 5. video sqlserver bağımlılığını kaldırıyor string oalrak
            

            optionsBuilder.UseSqlServer("server = DESKTOP-3KEFFM2\\SQLEXPRESS; database = dbBlogProject; integrated security = true; ");
            //  optionsBuilder.UseSqlServer("server=DESKTOP-3KEFFM2\\SQLEXPRESS; database=dbBlogProject; user id=sa; password=1;");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) // fluent api için gerekli 
        {

            modelBuilder.ApplyConfiguration(new AppUserMap());
            modelBuilder.ApplyConfiguration(new AppRoleMap());
            modelBuilder.ApplyConfiguration(new AppUserRoleMap());
            modelBuilder.ApplyConfiguration(new BlogMap());
            modelBuilder.ApplyConfiguration(new CategoryBlogMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new CommentMap());
            base.OnModelCreating(modelBuilder); // ıdentityden dolayı böyle bir durum söz konusu
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; } 
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<AppUserRole> AppUserRoles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryBlog> CategoryBlogs { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
    
}
