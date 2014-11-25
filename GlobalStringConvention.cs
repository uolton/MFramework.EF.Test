using System.Data.Entity.ModelConfiguration.Configuration;
using System.Reflection;
using MFramework.EF.Core.Conventions;

namespace MFramewoek.EF.Tests
{
    public class GlobalStringConvention : GlobalConfigurationConvention<MemberInfo, StringPropertyConfiguration>
    {
        protected override void Apply(MemberInfo memberInfo, StringPropertyConfiguration propertyConfiguration)
        {
            propertyConfiguration.HasMaxLength(51);
        }
    }
}
