using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using MFramework.EF.Core;
using MFramework.EF.Migrations;

namespace MFramewoek.EF.Tests.Target
{
    public class ProductsContext : ExtendedDbContext
    {
        public ProductsContext(string connectionString)
            : base(connectionString)
        {

        }

        public DbSet<Product> Products { get; set; }

        public DbSet<DbVersion> Version { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
            modelBuilder.Configurations.Add(new ProductMap());
        }

        protected override void AddConventions()
        {
            
        }
    }
}

