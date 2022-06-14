using Microsoft.EntityFrameworkCore;
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

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await Context.Employees.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (entity is null) throw new ArgumentException();
            Context.Remove(entity);
            await Context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Employee> FindAsync(Expression<Func<Employee, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await Context.Employees.Where(predicate).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<Employee> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            return await Context.Employees.SingleOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);           
        }

        public async Task<IEnumerable<Employee>> GetListAsync(Expression<Func<Employee, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await Context.Employees.Where(predicate).AsNoTracking().ToListAsync(cancellationToken);
        }

        public Task<IQueryable<Employee>> GetQueryableAsync()
        {
            return Task.FromResult(Context.Employees.AsNoTracking());
        }


        public async Task<Employee> UpdateAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            var existing = await GetAsync(employee.Id);
            if (existing is null) throw new ArgumentException();
            Context.Employees.Attach(employee);
            Context.Entry(employee).State = EntityState.Modified;
            await Context.SaveChangesAsync(cancellationToken);
            return employee;
        }

        public async Task<long> GetCountAsync(CancellationToken cancellationToken = default)
        {
            return await Context.Employees.CountAsync(cancellationToken);
        }

        public async Task<List<Employee>> GetPagedListAsync(int skipCount, int maxResultCount, CancellationToken cancellationToken = default)
        {
            return await Context.Employees.AsNoTracking().OrderBy(x => x.Id).Skip(skipCount).Take(maxResultCount).ToListAsync(cancellationToken);
        }

        public Task<Employee> InsertAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
