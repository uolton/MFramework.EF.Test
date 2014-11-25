using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using MFramework.EF.Core;
using MFramework.EF.Core.Conventions.Common;
using MFramework.EF.Migrations;

namespace MFramewoek.EF.Tests.Source
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
            base.OnModelCreating(modelBuilder);
            
        }

        protected override void AddConventions()
        {
            AddConvention(new GlobalDecimalConvention());
            AddConvention(new StringConvention());
            AddConvention(new GlobalStringConvention());
        }
    }
}

