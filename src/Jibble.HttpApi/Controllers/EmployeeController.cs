using Jibble.Employees;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jibble.DTOs;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Net;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Jibble.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        IEmployeeRepository Repository {get;}
        public EmployeeController(IEmployeeRepository repository) => Repository = repository;

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<PagedResultDto<EmployeeDto>> GetAsync([FromQuery] PagedRequestDto request)
        {
            var queryable = await Repository.GetQueryableAsync();
            //TODO: Change to AutoMapper
            var results = await queryable.Skip(request.SkipCount).Take(request.MaxResultCount).Select(x => new EmployeeDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                DateOfBirth = x.DateOfBirth
            }).ToListAsync();
            return new PagedResultDto<EmployeeDto>(await queryable.CountAsync(), results);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<EmployeeDto> Get(int id)
        {
            var result = await Repository.GetAsync(id);
            if (result == null) 
                throw new HttpResponseException(HttpStatusCode.NotFound);
                
            return new EmployeeDto
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                DateOfBirth = result.DateOfBirth
            };
        }

        [HttpPut("{id}")]
        [HttpPost("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public Task<IActionResult> UpdateAsync(int id, [FromBody] EmployeeDto employee)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        [HttpPost("{id}/delete")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
