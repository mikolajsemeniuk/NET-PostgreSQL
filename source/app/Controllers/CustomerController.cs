using System.Collections.Generic;
using System.Threading.Tasks;
using app.Inputs;
using app.Interfaces;
using app.Payloads;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _repository;

        public CustomerController(ICustomerRepository repository) =>
            _repository = repository;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CustomerPayload>>> GetCustomersAsync() =>
            Ok(await _repository.GetCustomersAsync());

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerPayload>> GetCustomerAsync(int id) =>
            Ok(await _repository.GetCustomerAsync(id));

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomerPayload>> AddCustomerAsync(CustomerInput input) =>
            Ok(await _repository.AddCustomerAsync(input));

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomerPayload>> UpdateCustomerAsync(int id, CustomerInput input) =>
            Ok(await _repository.UpdateCustomerAsync(id, input));

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RemoveCustomerAsync(int id)
        {
            await _repository.RemoveCustomerAsync(id);
            return NoContent();
        }
    }
}