using System.ComponentModel.DataAnnotations;
using MFramework.EF.Core.Conventions.Common;

namespace MFramewoek.EF.Tests.Source
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductNumber { get; set; }
        [String(0, 50, true)]
        public string ProductName { get; set; }
        [StringLength(int.MaxValue)]
        public string Notes { get; set; }
        public bool IsActive { get; set; }
        public decimal Salary { get; set; }
    }
}

