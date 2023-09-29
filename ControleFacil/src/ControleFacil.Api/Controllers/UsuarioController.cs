using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFacil.Api.Contract.Usuario;
using ControleFacil.Api.Damain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleFacil.Api.Controllers
{
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }


        [HttpPost] //HttpPost > Cadastrando algo no banco
        [AllowAnonymous] //qualquer um cadastra
        public async Task<IActionResult> Adicionar(UsuarioRequestContract contrato)
        {
            try
            {   //Create > Envia um código HTTP 201 > Indicando que a entidade foi criada com sucesso.
                return Created("", await _usuarioService.Adicionar(contrato, 0)) ;
            }
            catch(Exception ex)
            {  //Retorna um código HTTP 500 indicando um erro no código.
                return Problem(ex.Message);
            }
        }

        [HttpGet] //Consultar algo
        [AllowAnonymous] //qualquer um cadastra
        public async Task<IActionResult> Obter()
        {
            try
            {
                return Ok(await _usuarioService.Obter(0));
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Obter(long id)
        {
            try
            {
                return Ok(await _usuarioService.Obter(id, 0));
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut] //HttpPut > Faz uma atualização
        [Route("{id}")]
        [AllowAnonymous] //qualquer um cadastra
        public async Task<IActionResult> Atualizar(long id, UsuarioRequestContract contrato)
        {
            try
            {
                return Ok(await _usuarioService.Atualizar(id,contrato, 0)) ;
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        
        [HttpDelete]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Deletar(long id)
        {
            try
            {
                await _usuarioService.Deletar(id, 0);
                return NoContent();

            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}