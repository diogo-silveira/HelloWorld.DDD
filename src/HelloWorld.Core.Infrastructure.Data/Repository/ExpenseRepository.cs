using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HelloWorld.Core.Domain.Entities;
using HelloWorld.Core.Domain.Interfaces.Repository;
using HelloWorld.Core.Infrastructure.Data.Context;

namespace HelloWorld.Core.Infrastructure.Data.Repository
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly SerkoCoreDataContext _serkoCoreDataContext;

        public ExpenseRepository(SerkoCoreDataContext serkoCoreDataContext)
            => _serkoCoreDataContext = serkoCoreDataContext;

        public Expense Add(Expense expense)
        {
            var result =  _serkoCoreDataContext.Expense.Add(expense).Entity;
            _serkoCoreDataContext.SaveChanges();
            return result;
        }

        public Expense GetById(int id)
        {
            return _serkoCoreDataContext.Expense.Find(id);
        }

        public IEnumerable<Expense> GetAll()
        {
            return _serkoCoreDataContext.Expense.ToList();
        }
    }
}