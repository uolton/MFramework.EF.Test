using MFramework.EF.Migrations;
using MFramework.EF.Migrations.SqlServer;

namespace MFramewoek.EF.Tests.Defaults
{
    public class DefaultInitializer : MigratingInitializer<DefaultsCOntext>
    {
        public DefaultInitializer(INewVersionProvider<DefaultsCOntext> versionProivider)
            : base(versionProivider)
        {

        }



        protected override void BeforeMigration(DefaultsCOntext context)
        {
            
        }

        protected override void AfterMigration(DefaultsCOntext context)
        {
            
        }
    }
}
