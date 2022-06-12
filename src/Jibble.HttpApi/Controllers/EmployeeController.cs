using Jibble.Employees;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jibble.DTOs;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading;
using AutoMapper;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Jibble.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        IEmployeeRepository Repository {get;}
        IMapper Mapper { get; }
        public EmployeeController(IEmployeeRepository repository, IMapper mapper) => (Repository, Mapper) = (repository, mapper);

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<PagedResultDto<EmployeeDto>> GetAsync([FromQuery] PagedRequestDto request, CancellationToken cancellationToken)
        {
            var queryable = await Repository.GetQueryableAsync();
            //TODO: Change to AutoMapper
            var results = await Repository.GetPagedListAsync(request.SkipCount, request.MaxResultCount, cancellationToken);
            return new PagedResultDto<EmployeeDto>(await Repository.GetCountAsync(cancellationToken), Mapper.Map<List<Employee>, List<EmployeeDto>>(results));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<EmployeeDto> Get(int id)
        {
            var result = await Repository.GetAsync(id);
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<EmployeeDto>(result);
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
