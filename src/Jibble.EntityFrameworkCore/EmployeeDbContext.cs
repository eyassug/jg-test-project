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
        
        public DbSet<Employee> Employees { get; set; }
    }
}
