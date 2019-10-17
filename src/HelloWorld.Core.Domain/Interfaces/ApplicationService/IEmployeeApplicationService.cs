using System.Threading.Tasks;
using HelloWorld.Core.Domain.Entities;

namespace HelloWorld.Core.Domain.Interfaces.ApplicationService
{
    public interface IEmployeeApplicationService : IBaseApplicationService
    {
        Task<Employee> AuthenticationAsync(dynamic request);
    }
}