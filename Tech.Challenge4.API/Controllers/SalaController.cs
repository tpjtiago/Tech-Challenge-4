using Microsoft.AspNetCore.Mvc;
using Tech.Challenge4.Domain.Contracts.Services.Salas;
using Tech.Challenge4.Domain.Models.Sala;

namespace Tech.Challenge4.API.Controllers
{
    /// <summary>
    /// Controller para gerenciar salas.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SalaController : ControllerBase
    {
        private readonly ISalaService _salasService;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="SalaController"/>.
        /// </summary>
        public SalaController(ISalaService salasService)
        {
            _salasService = salasService;
        }

        /// <summary>
        /// Cria uma nova sala.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post(SalaModel salaModel)
        {
            var result = await _salasService.Post(salaModel);

            return Ok(result);
        }

        /// <summary>
        /// Obtém uma sala pelo ID.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _salasService.GetById(id);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Obtém todas as salas.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _salasService.GetAll());
        }

        /// <summary>
        /// Atualiza uma sala existente.
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] SalaModel salaModel)
        {
            var result = await _salasService.Put(id, salaModel);

            return Ok(result);
        }

        /// <summary>
        /// Exclui uma sala pelo ID.
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _salasService.DeleteById(id);

            return Ok();
        }
    }
}
