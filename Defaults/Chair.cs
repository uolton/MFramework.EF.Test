using System;
using System.ComponentModel.DataAnnotations.Schema;
using MFramework.EF.Core.Defaults;
using MFramework.EF.Core.Indexes;

namespace MFramewoek.EF.Tests.Defaults
{
    [Table("MyChair")]
    public class Chair
    {
        public int ChairID { get; set; }
        public string Description { get; set; }

        [Column("ChairHeight")]
        [Default("10")]
		[Indexed("Main", 0, direction: IndexDirection.Descending)]
        public decimal Height { get; set; }

        [Default("GetDate()")]
		[Indexed("Main", 1)]
        public DateTime DateAdded { get; set; }
    }
}

