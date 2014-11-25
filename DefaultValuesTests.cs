using System.Configuration;
using System.Data.Entity;
using System.Linq;
using MFramewoek.EF.Tests.Defaults;
using MFramework.EF.Core.Pluralization;
using MFramework.EF.Migrations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MFramewoek.EF.Tests
{
    [TestClass]
    public class DefaultValuesTests
    {

        [TestMethod]
        public void PluralizationTest()
        {
            Assert.AreEqual("people", Pluralizer.Pluralize("person"));
            Assert.AreEqual("people", Pluralizer.Pluralize("people"));
        }

        [TestMethod]
        public void GetDefaultsAndIndexesTest()
        {
            SqlUtil.DropDatabase("EFDefaultsContribTest");

            Database.SetInitializer<DefaultsCOntext>(
                new DefaultInitializer(new ModelBasedNewVersionProvider<DefaultsCOntext>()));
            using (var cxt = new DefaultsCOntext(ConfigurationManager.ConnectionStrings["EFDefaultsConnection"].ConnectionString))
            {
                var persons = cxt.People.FirstOrDefault();
                var defaults = cxt.MyDefaults;
                Assert.AreEqual(4, defaults.Count(), "Defaults not processed");

            	var indexData = SqlUtil.ExecuteQuery(SqlUtil.TestDefaultsConnectionString,
													 string.Format(Properties.Resources.IndexesSQL, "MyChair"));
				Assert.AreEqual(2, indexData.Count, "Should have index with 2 columns");
				Assert.AreEqual("Main", indexData[0][0].ToString(), "Should have index called Main");
				Assert.AreEqual("Main", indexData[1][0].ToString(), "Should have index called Main");
				Assert.AreEqual("ChairHeight", indexData[0][1].ToString(), "Should have column ChairHeight");
				Assert.AreEqual("DateAdded", indexData[1][1].ToString(), "Should have column DateAdded");
				Assert.AreEqual(true, (bool)indexData[0][5], "Should have column ChairHeight descending");
				Assert.AreEqual(false, (bool)indexData[1][5], "Should have column DateAdded ascending");

				indexData = SqlUtil.ExecuteQuery(SqlUtil.TestDefaultsConnectionString,
													 string.Format(Properties.Resources.IndexesSQL, "People"));
				Assert.AreEqual(1, indexData.Count, "Should have index with 1 column");
				Assert.AreEqual("Main", indexData[0][0].ToString(), "Should have index called Main");
				Assert.AreEqual("Salary", indexData[0][1].ToString(), "Should have column Salary");
				Assert.AreEqual(false, (bool)indexData[0][5], "Should have column Salary ascending");
            }
        }
    }
}
