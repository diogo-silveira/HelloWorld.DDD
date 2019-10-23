using System;
using System.Collections.Generic;
using HelloWorld.Core.Domain.Entities;
using HelloWorld.Core.Domain.Interfaces.ApplicationService;
using HelloWorld.Core.Domain.Interfaces.Service;
using HelloWorld.Core.Domain.Interfaces.UnitOfWork;
using HelloWorld.Core.Domain.Resources;
using HelloWorld.Core.Domain.Scopes;
using HelloWorld.Core.Domain.Util;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using HelloWorld.Core.Domain.DTO.Request;

namespace HelloWorld.Core.ApplicationService
{
    public class ExpenseApplicationService : BaseApplicationService, IExpenseApplicationService
    {
        private readonly IExpenseService _expenseService;

        public ExpenseApplicationService(IExpenseService expenseService,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _expenseService = expenseService;
        }

        public string Add(dynamic request)
        {
            Expense expense = Converter.ConvertTo<Expense>(request);

            var result = _expenseService.Add(expense);

            return Commit() ? string.Format(Messages.SUCCESS_ADD, "Expense") : string.Empty;
        }

        public Expense GetById(int id)
        {
            var expense = new Expense { ExpenseId = id };

            var validateScan = ExpenseScope.ValidateEmailExpenses(expense);

            if (validateScan.Any())
            {
                AddNotifications(validateScan);
                return null;
            }

            var expenseResult = _expenseService.GetById(expense.ExpenseId);

            if (expenseResult == null)
            {
                AddNotification("Expense", Messages.SUCCESS_NO_RECORDS);
                return null;
            }

            return expenseResult;
        }

        public IEnumerable<Expense> GetAll()
        {
            var expenses = _expenseService.GetAll().ToList();

            if (!expenses.Any())
                    AddNotification("Expense", Messages.SUCCESS_NO_RECORDS);
            
            return expenses.ToList();
        }
    }
}