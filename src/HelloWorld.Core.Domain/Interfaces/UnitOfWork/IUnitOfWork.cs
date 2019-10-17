using System;

namespace HelloWorld.Core.Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ActionResponse Commit();
    }
}