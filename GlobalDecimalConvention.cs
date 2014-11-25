using System.Data.Entity.ModelConfiguration.Configuration;
using System.Reflection;
using MFramework.EF.Core.Conventions;

namespace MFramewoek.EF.Tests
{
    public class GlobalDecimalConvention : GlobalConfigurationConvention<MemberInfo, DecimalPropertyConfiguration>
    {
        protected override void Apply(MemberInfo memberInfo, DecimalPropertyConfiguration propertyConfiguration)
        {
            propertyConfiguration.HasPrecision(8, 2);
        }
    }
}
