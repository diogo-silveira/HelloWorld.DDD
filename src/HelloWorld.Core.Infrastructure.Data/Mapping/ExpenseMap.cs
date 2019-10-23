using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HelloWorld.Core.Domain.Entities;

namespace HelloWorld.Core.Infrastructure.Data.Mapping
{
    class ExpenseMap : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> entity)
        {
            entity.ToTable("Expense");
            entity.Property(e => e.ExpenseId).HasColumnName("expense_id").ValueGeneratedOnAdd();
            entity.Property(e => e.CostCentre).HasColumnName("cost_centre");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.PaymentMethod).HasColumnName("payment_method");
            entity.Property(e => e.Total).HasColumnName("total");
            entity.Property(e => e.Vendor).HasColumnName("vendor");

        }
    }

}
