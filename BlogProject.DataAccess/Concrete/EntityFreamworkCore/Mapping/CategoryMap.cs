using BlogProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.DataAccess.Concrete.EntityFreamworkCore.Mapping
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(I => I.Id);
            builder.Property(I => I.Id).UseIdentityColumn(); // 1den başla 1 1 art

            builder.Property(I => I.Name).HasMaxLength(100).IsRequired();

            builder.HasMany   //öNCE LİST TUTULANI AL
           (p => p.CategoryBlogs).WithOne // DAHA SONRA DİĞER TABLODAKİ TEK OLANI AL
           (p => p.Category).HasForeignKey // DAHA SONRA DİĞER ABLODAKİ FOREIGN KEYI BELIRT
           (p => p.CategoryId).OnDelete //SİLİNCE BÜTÜN GÖREVLERİ SİLMEMESİ İÇİN DEĞER ATA
           (DeleteBehavior.Cascade);
        }
    }
}
