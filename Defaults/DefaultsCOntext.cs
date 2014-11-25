using System.Collections.Generic;
using System.Data.Entity;
using MFramework.EF.Core;
using MFramework.EF.Core.Defaults;
using MFramework.EF.Migrations;

namespace MFramewoek.EF.Tests.Defaults
{
    public class DefaultsCOntext : ExtendedDbContext
    {
        public DefaultsCOntext(string connectionString)
            : base(connectionString)
        {

        }

        public DbSet<Person> People { get; set; }

        public DbSet<Chair> Chairs { get; set; }

        public DbSet<DbVersion> Version { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            MyDefaults = GetDefaults();
        }

        protected override void AddConventions()
        {
            
        }

        public IEnumerable<DefaultInfo> MyDefaults { get; set; }
    }
}
