using System.Threading.Tasks;
using HelloWorld.Core.Domain.Entities;

namespace HelloWorld.Core.Domain.Interfaces.Service
{
    public interface IEmployeeService
    {
        Task<Employee> AuthenticationByUsernameAsync(Employee employee);

        Task<Employee> AuthenticationByBarcodeAsync(Employee employee);
    }
}