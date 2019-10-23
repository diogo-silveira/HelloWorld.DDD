using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HelloWorld.Core.Domain.Entities;
using HelloWorld.Core.Domain.Interfaces.Repository;
using HelloWorld.Core.Infrastructure.Data.Context;

namespace HelloWorld.Core.Infrastructure.Data.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DbCoreDataContext _skynetCoreDataContext;

        public EmployeeRepository(DbCoreDataContext skynetCoreDataContext)
            => _skynetCoreDataContext = skynetCoreDataContext;

        public async Task<Employee> AuthenticationByUsernameAsync(Employee employee)
        {
            if (employee.UserName.Equals("diogo") && employee.Password.Equals("diogo"))
            {
                employee.FirstName = "Diogo";
                employee.LastName = "Fraga da Silveira";
                employee.Barcode = 12345;
                return employee;
            }
            var resut = await _skynetCoreDataContext.Employees.FirstOrDefaultAsync(item => 
                     item.UserName.Equals(employee.UserName) && item.Password.Equals(employee.Password));

            return resut;
        }

        public async Task<Employee> AuthenticationByBarcodeAsync(Employee employee)
            => await _skynetCoreDataContext.Employees.FirstOrDefaultAsync(item =>
                item.Barcode.Value.Equals(employee.Barcode.Value) && item.Password.Equals(employee.Password));
    }
}