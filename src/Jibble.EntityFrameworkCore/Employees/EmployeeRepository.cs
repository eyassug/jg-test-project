﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<Employee> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            return await Context.Employees.SingleOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);           
        }

        public async Task<IEnumerable<Employee>> GetListAsync(Expression<Func<Employee, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await Context.Employees.Where(predicate).ToListAsync(cancellationToken);
        }

        public Task<IQueryable<Employee>> GetQueryableAsync()
        {
            return Task.FromResult(Context.Employees.AsQueryable());
        }

        public Task<Employee> InsertAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Employee> UpdateAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            Context.Employees.Attach(employee);
            Context.Entry(employee).State = EntityState.Modified;
            await Context.SaveChangesAsync(cancellationToken);
            return employee;
        }
    }
}
