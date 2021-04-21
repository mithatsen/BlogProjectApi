using BlogProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.DataAccess.Concrete.EntityFreamworkCore.Mapping
{
    class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(I => I.Id);
            builder.Property(I => I.Id).UseIdentityColumn(); // 1den başla 1 1 art


            builder.Property(I => I.AuthorName).HasMaxLength(100).IsRequired();
            builder.Property(I => I.Description).HasColumnType("ntext").IsRequired();
            builder.Property(I => I.AuthorEmail).HasMaxLength(100).IsRequired();

            builder.HasMany   //öNCE LİST TUTULANI AL
                (p => p.SubComments).WithOne // DAHA SONRA DİĞER TABLODAKİ TEK OLANI AL
                (p => p.ParentComment).HasForeignKey // DAHA SONRA DİĞER ABLODAKİ FOREIGN KEYI BELIRT
                (p => p.ParentCommentId);

        }
    }
}
