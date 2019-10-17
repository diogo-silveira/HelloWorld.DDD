using System;
using System.Collections.Generic;
using FluentValidator;
using FluentValidator.Validation;
using HelloWorld.Core.Domain.Entities;
using HelloWorld.Core.Domain.Resources;

namespace HelloWorld.Core.Domain.Scopes
{
    public static class ExpenseScope
    {
        private static ValidationContract Contract { get; set; }

        public static IReadOnlyCollection<Notification> ValidateEmailExpenses(Expense expense)
        {
            Contract = new ValidationContract()
                .Requires();

            return Contract.Notifications;
        }
        public static IReadOnlyCollection<Notification> ValidateGetByIdExpenses(Expense expense)
        {
            Contract = new ValidationContract()
               .Requires()
                .IsGreaterThan(expense.ExpenseId, 0, "ExpenseId",
                    string.Format(Messages.ERROR_FIELD_ISNULL, "Barcode"));

            return Contract.Notifications;
        }
    }
}