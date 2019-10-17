using System;
using HelloWorld.Core.Domain.Interfaces.Entity;

namespace HelloWorld.Core.Domain.Entities
{
    public class Employee : IEntity
    {
        public int EmployeeNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Barcode { get; set; }
        public int? RoleId { get; set; }
        public string Custom1 { get; set; }
        public string Custom2 { get; set; }
        public string Custom3 { get; set; }
        public bool? IsActive { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public string FirstEnteredBy { get; set; }
        public DateTime? FirstEnteredOn { get; set; }

    }
}