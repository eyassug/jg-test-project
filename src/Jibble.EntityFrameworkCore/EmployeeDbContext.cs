using Jibble.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jibble
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext()
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=employeedb;Trusted_Connection=True;"); // TODO: Replace with environment connection string

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .Property(c => c.Id)
                .ValueGeneratedNever()
                .HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.None);
                
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
