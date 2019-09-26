using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyNetCore3.WebAPP.Models
{
    public class Blog
    {
        public Blog()
        {
            Posts = new List<Post>();
        }
        public int Id { get; set; }
        public string BlogName { get; set; }
        public DateTime CreateTime { get; set; }
        public string Author { get; set; }
        public virtual ICollection<Post> Posts { get; set; }

    }

    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.ToTable("Blog");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.BlogName).HasColumnType("VARCHAR(50)").IsRequired();
            builder.Property(p => p.Author).HasColumnType("VARCHAR(50)");
            builder.Property(p => p.CreateTime).HasColumnType("DATETIME").IsRequired();
        }
    }
}
