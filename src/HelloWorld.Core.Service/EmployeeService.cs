using System.Threading.Tasks;
using HelloWorld.Core.Domain.Entities;
using HelloWorld.Core.Domain.Interfaces.Repository;
using HelloWorld.Core.Domain.Interfaces.Service;

namespace HelloWorld.Core.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
            => _employeeRepository = employeeRepository;

        public async Task<Employee> AuthenticationByUsernameAsync(Employee employee)
            => await _employeeRepository.AuthenticationByUsernameAsync(employee);


        public async Task<Employee> AuthenticationByBarcodeAsync(Employee employee)
            => await _employeeRepository.AuthenticationByBarcodeAsync(employee);
    }
}