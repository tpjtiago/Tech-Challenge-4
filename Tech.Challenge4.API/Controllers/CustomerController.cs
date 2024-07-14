using Microsoft.AspNetCore.Mvc;
using Tech.Challenge4.Domain.Contracts.Services.Customers;
using Tech.Challenge4.Domain.Models.Customers;

namespace Tech.Challenge4.API.Controllers
{
    /// <summary>
    /// Controller para gerenciar clientes.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="CustomerController"/>.
        /// </summary>
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Cria um novo cliente.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post(CustomerModel customerModel)
        {
            var result = await _customerService.Post(customerModel);

            return Ok(result);
        }

        /// <summary>
        /// Obtém um cliente pelo ID.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _customerService.GetById(id);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Obtém todos os clientes.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _customerService.GetAll());
        }

        /// <summary>
        /// Atualiza um cliente existente.
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] CustomerModel customerModel)
        {
            var result = await _customerService.Put(id, customerModel);

            return Ok(result);
        }

        /// <summary>
        /// Exclui um cliente pelo ID.
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _customerService.DeleteById(id);

            return Ok();
        }
    }
}
