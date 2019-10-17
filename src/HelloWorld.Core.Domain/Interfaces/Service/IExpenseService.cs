using System.Collections.Generic;
using System.Threading.Tasks;
using HelloWorld.Core.Domain.DTO.Request;
using HelloWorld.Core.Domain.Entities;

namespace HelloWorld.Core.Domain.Interfaces.Service
{
    public interface IExpenseService
    {
        Expense InvokeImporterEmailData(ExpenseEmailRequest request);

        Expense Add(Expense request);
        Expense GetById(int id);
        IEnumerable<Expense> GetAll();
    }
}