using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HelloWorld.Core.Domain.Entities;

namespace HelloWorld.Core.Infrastructure.Data.Mapping
{
    public class EmployeeMap : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> entity)
        {
            entity.ToTable("tblEmployees");

            entity.HasKey(e => e.EmployeeNumber);

            entity.HasIndex(e => e.Barcode).HasName("Barcode").IsUnique();
            entity.HasIndex(e => e.FirstName).HasName("FirstName");
            entity.HasIndex(e => e.LastName).HasName("FirstName1");
            entity.HasIndex(e => e.RoleId).HasName("tblRolestblEmployees");
            entity.HasIndex(e => e.UserName).HasName("UserName").IsUnique();

            entity.Property(e => e.EmployeeNumber);
            entity.Property(e => e.Barcode).HasDefaultValueSql("((0))");
            entity.Property(e => e.Custom1).HasMaxLength(255);
            entity.Property(e => e.Custom2).HasMaxLength(255);
            entity.Property(e => e.Custom3).HasMaxLength(255);
            entity.Property(e => e.FirstEnteredBy).HasMaxLength(30);
            entity.Property(e => e.FirstEnteredOn).HasColumnType("datetime").HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FirstName).HasMaxLength(30);
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            entity.Property(e => e.LastModifiedBy).HasMaxLength(30);
            entity.Property(e => e.LastModifiedOn).HasColumnType("datetime").HasDefaultValueSql("(getdate())");
            entity.Property(e => e.LastName).HasMaxLength(30);
            entity.Property(e => e.Password).HasMaxLength(20);
            entity.Property(e => e.RoleId).HasColumnName("RoleID").HasDefaultValueSql("((1))");
            entity.Property(e => e.UserName).HasMaxLength(30);

        }
    }
}
