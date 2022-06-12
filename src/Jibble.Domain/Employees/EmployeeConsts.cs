using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jibble.Employees
{
    public static class EmployeeConsts
    {
        public static class CSV
        {
            // Headers
            public const string Id = "Emp ID";
            public const string FirstName = "First Name";
            public const string LastName = "Last Name";
            public const string DateOfBirth = "Date of Birth";

            // Separator
            public const char Separator = ',';

            // Date format
            public const string DateFormat = "M/d/yyyy";
        }
    }
}
