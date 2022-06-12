using Jibble.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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

        public DbSet<Employee> Employees { get; set; }
    }
}
