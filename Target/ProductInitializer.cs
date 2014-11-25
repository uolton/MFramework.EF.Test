using MFramework.EF.Migrations;
using MFramework.EF.Migrations.SqlServer;

namespace MFramewoek.EF.Tests.Target
{
    public class ProductInitializer : MigratingInitializer<ProductsContext>
    {
        public  ProductInitializer(INewVersionProvider<ProductsContext> versionProivider)
            :base(versionProivider)
        {
            
        }

        protected override void BeforeMigration(ProductsContext context)
        {
            context.Database.ExecuteSqlCommand("Update Products Set IsActive=0", new object[] { });
        }

        protected override void AfterMigration(ProductsContext context)
        {
            context.Products.Add(new Product() { IsActive = true, ProductName = "After Migration", ProductNumber = "123" });
            context.SaveChanges();
        }
    }
}
