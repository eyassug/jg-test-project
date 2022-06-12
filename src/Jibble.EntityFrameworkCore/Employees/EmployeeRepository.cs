using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jibble.Employees
{
    public class EmployeeRepository : IEmployeeRepository
    {
        EmployeeDbContext Context { get; }
        public EmployeeRepository(EmployeeDbContext dbContext) => Context = dbContext;

        public Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> FindAsync(Expression<Func<Employee, bool>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Employee>> GetListAsync(Expression<Func<Employee, bool>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Employee>> GetQueryableAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Employee> InsertAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> UpdateAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
