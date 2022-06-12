using Jibble.Employees;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Jibble.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        public Task<IEnumerable<EmployeeDto>> GetAsync()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public Task<EmployeeDto> Get(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        [HttpPost("{id}")]
        public Task<IActionResult> UpdateAsync(int id, [FromBody] EmployeeDto employee)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        [HttpPost("{id}/delete")]
        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
