using System.Configuration;
using System.Data.Entity;
using System.Linq;
using MFramewoek.EF.Tests.Source;
using MFramework.EF.Migrations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MFramewoek.EF.Tests
{
    [TestClass]
    public class SqlServerMigrationsTest
    {

        [TestMethod]
        public void CreateDatabaseTest()
        {
            SqlUtil.DropDatabase(SqlUtil.DatabaseName);

            Database.SetInitializer<ProductsContext>(
                new ProductInitializer(new AssemblyBasedNewVersionProvider<ProductsContext>()));

            using (var ctx = new ProductsContext(ConfigurationManager.ConnectionStrings["EFContribConnection"].ConnectionString))
            {
                var thing = ctx.Products.FirstOrDefault();
                ctx.Products.Add(new Product() { IsActive = true, Notes = "some note", ProductName = "Some name", ProductNumber = "123"});
                ctx.SaveChanges();
            }
            Assert.IsTrue(SqlUtil.DatatabaseExists(SqlUtil.DatabaseName));

            Assert.AreEqual((int)SqlUtil.ExecuteScalar(SqlUtil.DatabaseName,
                "SELECT Count(1) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='Products' AND COLUMN_NAME='ProductNumber' AND CHARACTER_MAXIMUM_LENGTH='51'", false), (int)1);

            Assert.AreEqual((int)SqlUtil.ExecuteScalar(SqlUtil.DatabaseName,
               "SELECT Count(1) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='Products' AND COLUMN_NAME='ProductName' AND CHARACTER_MAXIMUM_LENGTH='50'", false), (int)1);

            Assert.AreEqual((int)SqlUtil.ExecuteScalar(SqlUtil.DatabaseName,
              "SELECT Count(1) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='Products' AND COLUMN_NAME='Salary' AND NUMERIC_PRECISION=8 AND NUMERIC_SCALE=2", false), (int)1);

        }

        [TestMethod]
        public void MigrateDatabaseTest()
        {
            SqlUtil.DropDatabase(SqlUtil.DatabaseName);

            Database.SetInitializer<ProductsContext>(
                new ProductInitializer(new AssemblyBasedNewVersionProvider<ProductsContext>()));
            Database.SetInitializer<Target.ProductsContext>(
                new Target.ProductInitializer(new AssemblyBasedNewVersionProvider<Target.ProductsContext>()));

            using (
                var ctx =
                    new ProductsContext(
                        ConfigurationManager.ConnectionStrings["EFContribConnection"].ConnectionString))
            {
                var thing = ctx.Products.FirstOrDefault();
            }

            using (
              var ctx =
                  new ProductsContext(
                      ConfigurationManager.ConnectionStrings["EFContribConnection"].ConnectionString))
            {

                ctx.Products.Add(new Product() { IsActive = true, Notes = "some note", ProductName = "Some name", ProductNumber = "123" });

                ctx.Version.First().CurrentVersion = "0.1";

                ctx.SaveChanges();

            }

            Assert.IsTrue(SqlUtil.DatatabaseExists(SqlUtil.DatabaseName));
            Assert.IsTrue(SqlUtil.ColumnExists(SqlUtil.DatabaseName, "Products", "Notes"));
            Assert.IsFalse(SqlUtil.ColumnExists(SqlUtil.DatabaseName, "Products", "SpecialInstructions"));

            using (
                var ctx =
                    new Target.ProductsContext(
                        ConfigurationManager.ConnectionStrings["EFContribConnection"].ConnectionString))
            {
                var thing = ctx.Products.FirstOrDefault();
            }

            Assert.IsTrue(SqlUtil.DatatabaseExists(SqlUtil.DatabaseName));

            Assert.IsTrue(SqlUtil.ColumnExists(SqlUtil.DatabaseName, "Products", "SpecialInstructions"));
            Assert.IsFalse(SqlUtil.ColumnExists(SqlUtil.DatabaseName, "Products", "Notes"));
        }
    }
}
