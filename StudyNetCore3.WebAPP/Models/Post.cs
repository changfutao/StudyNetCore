using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyNetCore3.WebAPP.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string PostName { get; set; }
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
    }

    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Post");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.PostName).HasColumnType("VARCHAR(50)").IsRequired();
            builder.HasOne(p => p.Blog).WithMany(p => p.Posts).HasForeignKey(p => p.BlogId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
