using System;
using System.Collections.Generic;
using Xunit;

namespace Jibble.Employees
{
    public class EmployeeRepositoryTests
    {
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
    }
}
