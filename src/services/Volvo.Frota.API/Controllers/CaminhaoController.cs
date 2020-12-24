using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volvo.Frota.API.Business.Interfaces;
using Volvo.Frota.API.Dtos;
using Volvo.Frota.API.Dtos.Pagination;

namespace Volvo.Frota.API.Controllers
{
    [Route("api/caminhoes")]
    public class CaminhaoController : MainController
    {
        private readonly ICaminhaoBusiness _caminhaoBusiness;
        public CaminhaoController(ICaminhaoBusiness caminhaoBusiness)
        {
            _caminhaoBusiness = caminhaoBusiness;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] PaginationFilterDto paginationFilter)
        {
            var result = await _caminhaoBusiness.GetAllPaginatedAsync(paginationFilter);

            return CustomResponse(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _caminhaoBusiness.GetByIdAsync(id);

            return CustomResponse(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] NewCaminhaoDto caminhaoDto)
        {
            var result = await _caminhaoBusiness.AddAsync(caminhaoDto);

            if (result.Failure)
                return CustomResponse(result);

            return CreatedAtAction(nameof(GetById), new { id = result.Value }, null);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromBody] UpdateCaminhaoDto caminhaoDto)
        {
            var result = await _caminhaoBusiness.UpdateAsync(caminhaoDto);

            return CustomResponse(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _caminhaoBusiness.RemoveAsync(id);

            return CustomResponse(result);
        }
    }
}
