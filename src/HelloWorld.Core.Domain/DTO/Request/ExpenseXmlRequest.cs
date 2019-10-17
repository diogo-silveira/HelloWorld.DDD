using System;
using System.Collections.Generic;
using System.Text;
using HelloWorld.Core.Domain.Interfaces.Entity;

namespace HelloWorld.Core.Domain.DTO.Request
{
    public class ExpenseXmlRequest:IEntity
    {
        public string cost_centre { get; set; }
        public double? total { get; set; }
        public string payment_method { get; set; }
        public string vendor { get; set; }
        public string description { get; set; }
        public DateTime? date { get; set; }
    }
}
