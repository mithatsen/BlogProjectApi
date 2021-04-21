using BlogProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.DataAccess.Concrete.EntityFreamworkCore.Mapping
{
    public class BlogMap : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasKey(I => I.Id);
            builder.Property(I => I.Id).UseIdentityColumn(); // 1den başla 1 1 art

            builder.Property(I => I.Author).HasMaxLength(100);
            builder.Property(I => I.Description).HasColumnType("nvarchar(MAX)").IsRequired();
            builder.Property(I => I.ShortDescription).HasMaxLength(300).IsRequired();
            builder.Property(I => I.Title).HasMaxLength(50).IsRequired();
            builder.Property(I => I.ImagePath).HasMaxLength(300);

            builder.HasMany   //öNCE LİST TUTULANI AL
                (p => p.CategoryBlogs).WithOne // DAHA SONRA DİĞER TABLODAKİ TEK OLANI AL
                (p => p.Blog).HasForeignKey // DAHA SONRA DİĞER ABLODAKİ FOREIGN KEYI BELIRT
                (p => p.BlogId).OnDelete //SİLİNCE BÜTÜN GÖREVLERİ SİLMEMESİ İÇİN DEĞER ATA
                (DeleteBehavior.Cascade);

            builder.HasMany   //öNCE LİST TUTULANI AL
               (p => p.Comments).WithOne // DAHA SONRA DİĞER TABLODAKİ TEK OLANI AL
               (p => p.Blog).HasForeignKey // DAHA SONRA DİĞER ABLODAKİ FOREIGN KEYI BELIRT
               (p => p.BlogId).OnDelete //SİLİNCE BÜTÜN GÖREVLERİ SİLMEMESİ İÇİN DEĞER ATA
               (DeleteBehavior.Cascade);
        }
    }
}
