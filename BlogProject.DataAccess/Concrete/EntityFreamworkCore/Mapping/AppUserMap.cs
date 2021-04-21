using BlogProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.DataAccess.Concrete.EntityFreamworkCore.Mapping
{
    public class AppUserMap : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(I => I.Id);
            builder.Property(I => I.Id).UseIdentityColumn(); // 1den başla 1 1 art

            builder.Property(I => I.UserName).HasMaxLength(100).IsRequired();
            builder.HasIndex(I => I.UserName).IsUnique();

            builder.Property(I => I.Password).HasMaxLength(100).IsRequired();
            builder.Property(I => I.Name).HasMaxLength(50);
            builder.Property(I => I.Surname).HasMaxLength(50);
            builder.Property(x => x.Email).HasMaxLength(100);

            builder.HasMany   //öNCE LİST TUTULANI AL
                (p => p.AppUserRoles).WithOne // DAHA SONRA DİĞER TABLODAKİ TEK OLANI AL
                (p => p.AppUser).HasForeignKey // DAHA SONRA DİĞER ABLODAKİ FOREIGN KEYI BELIRT
                (p => p.AppUserId).OnDelete //SİLİNCE BÜTÜN GÖREVLERİ SİLMEMESİ İÇİN DEĞER ATA
                (DeleteBehavior.Cascade);
            
            builder.HasMany   //öNCE LİST TUTULANI AL
               (p => p.Blogs).WithOne // DAHA SONRA DİĞER TABLODAKİ TEK OLANI AL
               (p => p.AppUser).HasForeignKey // DAHA SONRA DİĞER ABLODAKİ FOREIGN KEYI BELIRT
               (p => p.AppUserId).OnDelete //SİLİNCE BÜTÜN GÖREVLERİ SİLMEMESİ İÇİN DEĞER ATA
               (DeleteBehavior.Cascade);

        }
    }
}
