using System;
using System.Collections.Generic;
using System.Text;
using HelloWorld.Core.Domain.Interfaces.Entity;

namespace HelloWorld.Core.Domain.Entities
{
    public partial class Expense : IEntity
    {
        public int ExpenseId { get; set; }
        public string CostCentre { get; set; }
        public double? Total { get; set; }
        public string PaymentMethod { get; set; }
        public string Vendor { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
    }
}
