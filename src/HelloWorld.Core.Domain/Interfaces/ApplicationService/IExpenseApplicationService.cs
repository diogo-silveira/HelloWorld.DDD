using System.Collections.Generic;
using System.Threading.Tasks;
using HelloWorld.Core.Domain.Entities;

namespace HelloWorld.Core.Domain.Interfaces.ApplicationService
{
    public interface IExpenseApplicationService : IBaseApplicationService
    {
        Expense InvokeImporterEmailData(dynamic request);
        string Add(dynamic request);
        Expense GetById(int id);
        IEnumerable<Expense> GetAll();

    }
}