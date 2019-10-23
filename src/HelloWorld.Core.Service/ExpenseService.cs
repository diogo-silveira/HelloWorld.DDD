using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HelloWorld.Core.Domain.DTO.Request;
using HelloWorld.Core.Domain.Entities;
using HelloWorld.Core.Domain.Interfaces.Repository;
using HelloWorld.Core.Domain.Interfaces.Service;
using HelloWorld.Core.Domain.Resources;
using HelloWorld.Core.Domain.Util;

namespace HelloWorld.Core.Service
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseService(IExpenseRepository expenseRepository)
            => _expenseRepository = expenseRepository;
        
        public Expense Add(Expense request)
        {
            return _expenseRepository.Add(request);
        }

        Expense IExpenseService.GetById(int id)
        {
            return _expenseRepository.GetById(id);
        }

        IEnumerable<Expense> IExpenseService.GetAll()
        {
            return _expenseRepository.GetAll();
        }
    }
}