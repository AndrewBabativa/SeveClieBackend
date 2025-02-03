using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeveClie.Application.Commands;
using SeveClie.Application.Queries;
using SeveClie.Application.DTOs;  // Asegúrate de importar los DTOs correspondientes

namespace SeveClie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClieController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClieController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creación de Cliente.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] CreateClieCommand command)
        {
            try
            {
                var clieDto = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetClient), new { id = clieDto.IdClie }, clieDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error inesperado al crear el cliente.", error = ex.Message });
            }
        }

        /// <summary>
        /// Actualizar Cliente.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] UpdateClieCommand request)
        {
            try
            {
                request.IdClie = id;

                var success = await _mediator.Send(request);

                if (success == null)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error inesperado al actualizar el cliente.", error = ex.Message });
            }
        }

        /// <summary>
        /// Eliminar Cliente por ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            try
            {
                var command = new DeleteClieCommand(id);
                var result = await _mediator.Send(command);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error inesperado al eliminar el cliente.", error = ex.Message });
            }
        }

        /// <summary>
        /// Consultar todos los Clientes.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            try
            {
                var query = new GetAllClieQuery();
                var clients = await _mediator.Send(query);

                return Ok(clients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error inesperado al obtener los clientes.", error = ex.Message });
            }
        }

        /// <summary>
        /// Consultar Cliente por ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClient(int id)
        {
            try
            {
                var query = new GetAllClieQuery();
                var clients = await _mediator.Send(query);
                var clientDto = clients.FirstOrDefault(c => c.IdClie == id);

                if (clientDto == null)
                    return NotFound();

                return Ok(clientDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error inesperado al obtener el cliente.", error = ex.Message });
            }
        }
    }
}
