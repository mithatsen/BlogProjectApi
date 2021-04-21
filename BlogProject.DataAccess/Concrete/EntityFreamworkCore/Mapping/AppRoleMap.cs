using BlogProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogProject.DataAccess.Concrete.EntityFreamworkCore.Mapping
{
    public class AppRoleMap : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.HasKey(I => I.Id);
            builder.Property(I => I.Id).UseIdentityColumn(); // 1den başla 1 1 art

            builder.Property(I => I.Name).HasMaxLength(100).IsRequired();

            builder.HasMany   //öNCE LİST TUTULANI AL
                (p => p.AppUserRoles).WithOne // DAHA SONRA DİĞER TABLODAKİ TEK OLANI AL
                (p => p.AppRole).HasForeignKey // DAHA SONRA DİĞER ABLODAKİ FOREIGN KEYI BELIRT
                (p => p.AppRoleId).OnDelete //SİLİNCE BÜTÜN GÖREVLERİ SİLMEMESİ İÇİN DEĞER ATA
                (DeleteBehavior.Cascade);
        }
    }



}
