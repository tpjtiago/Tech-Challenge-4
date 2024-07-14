using Microsoft.AspNetCore.Mvc;
using Tech.Challenge4.Domain.Contracts.Services.Coworkings;
using Tech.Challenge4.Domain.Models.Coworking;

namespace Tech.Challenge4.API.Controllers
{
    /// <summary>
    /// Controller para gerenciar coworkings.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CoworkingController : ControllerBase
    {
        private readonly ICoworkingService _coworkingService;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="CoworkingController"/>.
        /// </summary>
        public CoworkingController(ICoworkingService coworkingService)
        {
            _coworkingService = coworkingService;
        }

        /// <summary>
        /// Cria um novo coworking.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post(CoworkingModel coworkingModel)
        {
            var result = await _coworkingService.Post(coworkingModel);

            return Ok(result);
        }

        /// <summary>
        /// Obtém um coworking pelo ID.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _coworkingService.GetById(id);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Obtém todos os coworkings.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _coworkingService.GetAll());
        }

        /// <summary>
        /// Atualiza um coworking existente.
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] CoworkingModel coworkingModel)
        {
            var result = await _coworkingService.Put(id, coworkingModel);

            return Ok(result);
        }

        /// <summary>
        /// Exclui um coworking pelo ID.
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _coworkingService.DeleteById(id);

            return Ok();
        }
    }
}
