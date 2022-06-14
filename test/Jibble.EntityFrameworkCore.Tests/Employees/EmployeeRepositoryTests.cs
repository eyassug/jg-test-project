using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
namespace Jibble.Employees
{
    public class EmployeeRepositoryTests
    {
        static DbContextOptions<EmployeeDbContext> GetDbContextOptions() => new DbContextOptionsBuilder<EmployeeDbContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}.db")
            .Options;
        static List<Employee> TestEmployees = new List<Employee>
        {
            new Employee {Id = 1, FirstName = "Harry", LastName = "Potter", DateOfBirth = new DateTime(1991, 1, 1)},
            new Employee {Id = 2, FirstName = "Ron", LastName = "Weasley", DateOfBirth = new DateTime(1992, 2, 2)},
            new Employee {Id = 3, FirstName = "Harry", LastName = "Potter", DateOfBirth = new DateTime(1993, 3, 3)},
            new Employee {Id = 4, FirstName = "Harry", LastName = "Potter", DateOfBirth = new DateTime(1994, 4, 4)},
        };

        static Employee SingleEmployee = new Employee { Id = 5, FirstName = "Tom", LastName = "Riddle", DateOfBirth = new DateTime(1995, 5, 5) };


        [Fact]
        public void Test()
        {
            Assert.Equal(1, 1);
        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task DeleteAsync_Success_Test(int id)
        {
            var context = await CreateDbContext();
            var repository = CreateRepository(context);

            // Act
            await repository.DeleteAsync(id);

            // Assert
            var deleted = await repository.GetAsync(id);
            Assert.Null(deleted);
            
        }

        [Theory]
        [InlineData(10)]
        public async Task DeleteAsync_ThrowArgumentException_NonExistingData(int id)
        {
            var context = await CreateDbContext();
            var repository = CreateRepository(context);

            var nonExisting = await repository.GetAsync(id);
            Assert.Null(nonExisting);
            await Assert.ThrowsAsync<ArgumentException>(async() => await repository.DeleteAsync(id));

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task UpdateAsync_Success_Test(int id)
        {
            var context = await CreateDbContext();
            var repository = CreateRepository(context);
            var update = new Employee
            {
                Id = id,
                FirstName = "Updated",
                LastName = "Updated"
            };

            var existing = await repository.GetAsync(id);
            Assert.NotNull(existing);

            existing.FirstName = update.FirstName;
            existing.LastName = update.LastName;
            existing.DateOfBirth = update.DateOfBirth;

            await repository.UpdateAsync(existing);

            // Assert
            var actual = await repository.GetAsync(id);
            Assert.NotNull(actual);
            Assert.Equal(update.FirstName, actual.FirstName);
            Assert.Equal(update.FirstName, actual.FirstName);
            Assert.Equal(update.DateOfBirth, actual.DateOfBirth);

        }

        [Theory]
        [InlineData(10)]
        public async Task UpdateAsync_ThrowArgumentException_NonExistingData(int id)
        {
            var context = await CreateDbContext();
            var repository = CreateRepository(context);

            // Act

            await Assert.ThrowsAsync<ArgumentException>(async () => await repository.UpdateAsync(new Employee
            {
                Id = id,
                FirstName = "updated",
                LastName = "updated"
            }));

            // Assert
            var deleted = await repository.GetAsync(id);
            Assert.Null(deleted);

        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        private async Task CountAsync_SuccessTest(int id)
        {
            var context = await CreateDbContext();
            var repository = CreateRepository(context);

            Assert.Equal(await context.Employees.CountAsync(), await repository.GetCountAsync());

            await repository.DeleteAsync(id);

            Assert.Equal(await context.Employees.CountAsync(), await repository.GetCountAsync());
        }
        private async Task PopulateDataAsync(EmployeeDbContext context)
        {
            await context.AddRangeAsync(TestEmployees);
            await context.SaveChangesAsync();
        }

        private async Task<EmployeeDbContext> CreateDbContext()
        {
            var context = new EmployeeDbContext(GetDbContextOptions());
            await PopulateDataAsync(context);
            return context;
        }

        private EmployeeRepository CreateRepository(EmployeeDbContext context) => new EmployeeRepository(context);
    }
}
