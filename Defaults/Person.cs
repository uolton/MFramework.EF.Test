using MFramework.EF.Core.Defaults;
using MFramework.EF.Core.Indexes;

namespace MFramewoek.EF.Tests.Defaults
{
    public class Person
    {
        public int PersonID { get; set; }
        public string PersonName { get; set; }
        [Default("1")]
        public bool IsActive { get; set; }
        [Default("People", "Salary", "1100")]
		[Indexed("Main")]
        public decimal Salary { get; set; }
    }
}

