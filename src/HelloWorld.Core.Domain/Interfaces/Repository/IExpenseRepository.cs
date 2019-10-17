using System.Collections.Generic;
using System.Threading.Tasks;
using HelloWorld.Core.Domain.Entities;

namespace HelloWorld.Core.Domain.Interfaces.Repository
{
    public interface IExpenseRepository
    {

        Expense Add(Expense request);
        Expense GetById(int id);
        IEnumerable<Expense> GetAll();
    }
}