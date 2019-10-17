using System.Threading.Tasks;
using HelloWorld.Core.Domain.Entities;

namespace HelloWorld.Core.Domain.Interfaces.Repository
{
    public interface IEmployeeRepository
    {
        Task<Employee> AuthenticationByUsernameAsync(Employee employee);

        Task<Employee> AuthenticationByBarcodeAsync(Employee employee);
    }
}