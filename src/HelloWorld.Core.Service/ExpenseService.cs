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


        public Expense InvokeImporterEmailData(ExpenseEmailRequest request)
        {
            Expense result = null;
            var xmlRaw = ExtractData(request);
            var xmlDoc = LoadXML(xmlRaw);
            Expense expense = LoadEntity(xmlDoc);
            result = _expenseRepository.Add(expense);

            return result;
        }

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


        #region Private methods

        private Expense LoadEntity(XmlDocument xmlDoc)
        {
            Expense expense = new Expense();

            expense.CostCentre = GetValueFromNode(xmlDoc.GetElementsByTagName("cost_centre")) == null ? "UNKNOWN" : GetValueFromNode(xmlDoc.GetElementsByTagName("cost_centre"));

            if (GetValueFromNode(xmlDoc.GetElementsByTagName("total")) != String.Empty)
            {
                expense.Total = double.Parse(GetValueFromNode(xmlDoc.GetElementsByTagName("total")));
            }

            expense.PaymentMethod = GetValueFromNode(xmlDoc.GetElementsByTagName("payment_method"));
            expense.Vendor = GetValueFromNode(xmlDoc.GetElementsByTagName("vendor"));
            expense.Description = GetValueFromNode(xmlDoc.GetElementsByTagName("description"));

            if (GetValueFromNode(xmlDoc.GetElementsByTagName("date")) != String.Empty)
            {
                expense.Date = GetValueFromNode(xmlDoc.GetElementsByTagName("date"));
            }

            return expense;
        }

        private XmlDocument LoadXML(string xmlRaw)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.LoadXml(xmlRaw);
            }
            catch
            {
                xmlRaw = "<expense>" + xmlRaw + "</expense>";
                xmlDoc.LoadXml(xmlRaw);
            }

            return xmlDoc;
        }

        private string ExtractData(ExpenseEmailRequest request)
        {
            var expresion = @"<expense>(.*?)</expense>|<(.*?)>(.*?)<(.*?)>";
            Expense result = null;
            Regex regex = new Regex(expresion);
            var xmlRaw = string.Empty;

            request.EmailData = request.EmailData.Replace('\n', ' ');

            foreach (var values in regex.Matches(request.EmailData))
            {
                xmlRaw += values;
            }

            return xmlRaw;
        }

        private string GetValueFromNode(XmlNodeList nodeList)
        {
            foreach (XmlNode node in nodeList)
            {
                return node.InnerText;
            }
            return string.Empty;
        }
        #endregion
    }
}