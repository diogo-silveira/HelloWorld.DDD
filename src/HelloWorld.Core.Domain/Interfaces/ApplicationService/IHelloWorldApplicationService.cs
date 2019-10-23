using System.Collections.Generic;
using System.Threading.Tasks;
using HelloWorld.Core.Domain.Entities;

namespace HelloWorld.Core.Domain.Interfaces.ApplicationService
{
    public interface IHelloWorldApplicationService : IBaseApplicationService
    {
        Task<string> HelloWorld();

    }
}