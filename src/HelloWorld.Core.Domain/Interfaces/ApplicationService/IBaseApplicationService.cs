using System.Collections.Generic;
using FluentValidator;

namespace HelloWorld.Core.Domain.Interfaces.ApplicationService
{
    public interface IBaseApplicationService
    {
        IEnumerable<Notification> ListNotifications();

        bool Commit();
    }
}