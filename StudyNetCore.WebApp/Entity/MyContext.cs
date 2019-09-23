using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyNetCore.WebApp.Entity
{
    public class MyContext:DbContext
    {
        public MyContext(DbContextOptions<MyContext>options):base(options)
        {

        }
        public DbSet<TProduct> TProducts { get; set; }
        public DbSet<TMaterial> TMaterials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new MaterialConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
