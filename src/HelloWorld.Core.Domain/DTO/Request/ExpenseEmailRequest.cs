using System;
using System.Collections.Generic;
using System.Text;
using HelloWorld.Core.Domain.Interfaces.Entity;

namespace HelloWorld.Core.Domain.DTO.Request
{
    public class ExpenseEmailRequest:IEntity
    {
        public string EmailData { get; set; }
    }
}
