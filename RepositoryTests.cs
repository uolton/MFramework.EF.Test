using System.Data.Entity;
using System.Linq;
using MFramewoek.EF.Tests.Source;
using MFramework.EF.Migrations;
using MFramework.EF.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MFramewoek.EF.Tests
{
    [TestClass]
    public class RepositoryTests
    {
        [TestMethod]
        public void RepsotiryInsertTest()
        {
            SqlUtil.DropDatabase(SqlUtil.DatabaseName);

            Database.SetInitializer<ProductsContext>(
                 new ProductInitializer(new AssemblyBasedNewVersionProvider<ProductsContext>()));

            using (var repo = new ProductRepository())
            {
                var products = repo.Select<Product>();

                repo.Insert(new Product() { ProductNumber = "111", ProductName = "Name 1111", IsActive = true });
            }

            using (var repo = new ProductRepository())
            {
                var products = repo.Select<Product>();

                Assert.AreEqual(1, products.Count(), "Should have one product");
            }
        }

        [TestMethod]
        public void RepsotiryUpdateTest()
        {
            SqlUtil.DropDatabase(SqlUtil.DatabaseName);

            Database.SetInitializer<ProductsContext>(
                 new ProductInitializer(new AssemblyBasedNewVersionProvider<ProductsContext>()));

            using (var repo = new ProductRepository())
            {
                var products = repo.Select<Product>();

                repo.Insert(new Product() { ProductNumber = "111", ProductName = "Name 1111", IsActive = true });
            }

            using (var repo = new ProductRepository())
            {
                var product = repo.Select<Product>().First();
                product.ProductName = "Updated name";
                repo.Update(product);
            }

            using (var repo = new ProductRepository())
            {
                var product = repo.Select<Product>().First();
                Assert.AreEqual("Updated name", product.ProductName, "Should have updated one product");
            }
        }

        [TestMethod]
        public void RepsotiryDeleteTest()
        {
            SqlUtil.DropDatabase(SqlUtil.DatabaseName);

            Database.SetInitializer<ProductsContext>(
                 new ProductInitializer(new AssemblyBasedNewVersionProvider<ProductsContext>()));

            using (var repo = new ProductRepository())
            {
                var products = repo.Select<Product>();

                repo.Insert(new Product() { ProductNumber = "111", ProductName = "Name 1111", IsActive = true });
            }

            Product product = new Product() { ProductId = 1 };

            using (var repo = new ProductRepository())
            {
                repo.Delete(product);
            }

            using (var repo = new ProductRepository())
            {
                var products = repo.Select<Product>();
                Assert.AreEqual(0, products.Count(), "Should have deleted one product");
            }
        }

        [TestMethod]
        public void RepsotirySelectTest()
        {
            SqlUtil.DropDatabase(SqlUtil.DatabaseName);

            Database.SetInitializer<ProductsContext>(
                 new ProductInitializer(new AssemblyBasedNewVersionProvider<ProductsContext>()));

            using (var repo = new ProductRepository())
            {
                var products = repo.Select<Product>();

                repo.Insert(new Product() { ProductNumber = "111", ProductName = "Name 1111", IsActive = true });
                repo.Insert(new Product() { ProductNumber = "011", ProductName = "Name 222", IsActive = false });
            }

            using (var repo = new ProductRepository())
            {
                var products = repo.Select<Product>();
                Assert.AreEqual(2, products.Count(), "Should have 2 products");

                products = repo.Select<Product>(whereClause: (p) => p.IsActive);
                Assert.AreEqual(1, products.Count(), "Should have 1 products");

                products = repo.Select<Product>(
                orderBy: new IOrderByClause<Product>[]
                             {
                                 new OrderByClause<Product, string>((p) => p.ProductNumber),
                                 new OrderByClause<Product, int>((p) => p.ProductId)
                             });
                Assert.AreEqual("011", products.First().ProductNumber, "Should have correct first product");

                products = repo.Select<Product>(
                    orderBy: new IOrderByClause<Product>[]
                             {
                                 new OrderByClause<Product, string>((p) => p.ProductNumber),
                                 new OrderByClause<Product, int>((p) => p.ProductId)
                             },
                             top: 1, 
                             skip: 1);
                Assert.AreEqual("111", products.First().ProductNumber, "Should have correct first product");
            }
        }
    }
}
