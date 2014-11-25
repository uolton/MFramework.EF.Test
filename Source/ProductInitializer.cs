using MFramework.EF.Migrations;
using MFramework.EF.Migrations.SqlServer;

namespace MFramewoek.EF.Tests.Source
{
    public class ProductInitializer : MigratingInitializer<ProductsContext>
    {
        public ProductInitializer(INewVersionProvider<ProductsContext> versionProivider)
            : base(versionProivider)
        {

        }

        protected override void BeforeMigration(ProductsContext context)
        {

        }

        protected override void AfterMigration(ProductsContext context)
        {

        }
    }
}
