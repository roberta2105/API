using ControleFacil.Api.Contract.Areceber;
using ControleFacil.Api.Damain.Services;
using ControleFacil.Api.Damain.Services.Interfaces;
using ControleFacil.Api.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleFacil.Api.Controllers
{
    [ApiController]
    [Route("areceber")]
    public class AreceberController : BaseController
    {
        private readonly IReceberService _areceberService;

        public AreceberController(IReceberService areceberService)
        {
            _areceberService = areceberService;
        }

        [HttpPost] //HttpPost > Cadastrando algo no banco
        [Authorize]
        public async Task<IActionResult> Adicionar(AreceberRequestContract contrato)
        {
            try
            {
                long idUsuario = ObterIdUsuarioLogado();
                return Created("", await _areceberService.Adicionar(contrato, idUsuario));
            }

            catch (BadRequestException ex)
            {
                return BadRequest(RetornarModelBadRequest(ex));
            }
            catch (Exception ex)
            {  //Retorna um código HTTP 500 indicando um erro no código.
                return Problem(ex.Message);
            }
        }

        // [HttpGet] //Consultar algo
        // [Authorize]
        // public async Task<IActionResult> Obter()
        // {
        //     try
        //     {
        //         long idUsuario = ObterIdUsuarioLogado();
        //         return Ok(await _areceberService.Obter(idUsuario));
        //     }
        //     catch (Exception ex)
        //     {
        //         return Problem(ex.Message);
        //     }
        // }

        [HttpGet] 
        [Authorize]
        public async Task<IActionResult> Obter(long idNaturezaDeLancamento)
        {
            try
            {
                long idUsuario = ObterIdUsuarioLogado();
                var recebimentosPorNatureza = await _areceberService.ObterPorNaturezaDeLancamento(idNaturezaDeLancamento, idUsuario);

                return Ok(recebimentosPorNatureza); // Retornar os recebimentos por natureza
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> ObterId(long id)
        {
            try
            {
                long idUsuario = ObterIdUsuarioLogado();
                return Ok(await _areceberService.ObterId(id, idUsuario));
            }
            catch (NotFoundException ex)
            {
                return NotFound(RetornarModelNotFound(ex));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut] //HttpPut > Faz uma atualização
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Atualizar(long id, AreceberRequestContract contrato)
        {
            try
            {
                long idUsuario = ObterIdUsuarioLogado();
                return Ok(await _areceberService.Atualizar(id, contrato, idUsuario));
            }
            catch (NotFoundException ex)
            {
                return NotFound(RetornarModelNotFound(ex));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(RetornarModelBadRequest(ex));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Deletar(long id)
        {
            try
            {
                long idUsuario = ObterIdUsuarioLogado();
                await _areceberService.Deletar(id, idUsuario);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(RetornarModelNotFound(ex));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}