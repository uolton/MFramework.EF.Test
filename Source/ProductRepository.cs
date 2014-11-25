using System.Configuration;
using MFramework.EF.Repository;

namespace MFramewoek.EF.Tests.Source
{
    public class ProductRepository : ReadWriteRepository<ProductsContext>
    {
        protected override string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["EFContribConnection"].ConnectionString; }
        }
    }
}
