using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jibble.Employees
{
    public interface IEmployeeRepository
    {
        Task<IQueryable<Employee>> GetQueryableAsync();
        Task<Employee> FindAsync(Expression<Func<Employee, bool>> predicate, CancellationToken cancellationToken = default);
        Task<Employee> InsertAsync(Employee employee, CancellationToken cancellationToken = default);
        Task<Employee> GetAsync(int id, CancellationToken cancellationToken = default);
        Task<Employee> UpdateAsync(Employee employee, CancellationToken cancellationToken = default);
        Task<IEnumerable<Employee>> GetListAsync(Expression<Func<Employee, bool>> predicate, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
