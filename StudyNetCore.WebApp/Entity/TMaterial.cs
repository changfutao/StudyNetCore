using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyNetCore.WebApp.Entity
{
    public class TMaterial
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public TProduct TProduct { get; set; }
    }

    public class MaterialConfiguration : IEntityTypeConfiguration<TMaterial>
    {
        public void Configure(EntityTypeBuilder<TMaterial> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.HasOne(x => x.TProduct).WithMany(x => x.TMaterials).HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
