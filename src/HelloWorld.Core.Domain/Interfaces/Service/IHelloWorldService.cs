using System.Collections.Generic;
using System.Threading.Tasks;
using HelloWorld.Core.Domain.DTO.Request;
using HelloWorld.Core.Domain.Entities;

namespace HelloWorld.Core.Domain.Interfaces.Service
{
    public interface IHelloWorldService
    {
        Task<string> HelloWorld();
    }
}