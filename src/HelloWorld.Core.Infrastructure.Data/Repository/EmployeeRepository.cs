using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HelloWorld.Core.Domain.Entities;
using HelloWorld.Core.Domain.Interfaces.Repository;
using HelloWorld.Core.Infrastructure.Data.Context;

namespace HelloWorld.Core.Infrastructure.Data.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly SerkoCoreDataContext _skynetCoreDataContext;

        public EmployeeRepository(SerkoCoreDataContext skynetCoreDataContext)
            => _skynetCoreDataContext = skynetCoreDataContext;

        public async Task<Employee> AuthenticationByUsernameAsync(Employee employee)
            => await _skynetCoreDataContext.Employees.FirstOrDefaultAsync(item =>
                item.UserName.Equals(employee.UserName) && item.Password.Equals(employee.Password));

        public async Task<Employee> AuthenticationByBarcodeAsync(Employee employee)
            => await _skynetCoreDataContext.Employees.FirstOrDefaultAsync(item =>
                item.Barcode.Value.Equals(employee.Barcode.Value) && item.Password.Equals(employee.Password));
    }
}