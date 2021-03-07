using System;
using System.Net.Http;
using Labs.Feedback.API.Dto;
using Labs.Feedback.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Labs.Feedback.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IMensagemService _mensagemService;

        public FeedbackController(IMensagemService mensagemService)
        {
            this._mensagemService = mensagemService;
        }

        [HttpPost]
        public IActionResult PostCadastrarMensagem(MensagemDto mensagemDto)
        {
            var mensagem = this._mensagemService.CadastrarMensagem(mensagemDto);

            if (mensagem == null)
                return BadRequest();

            return Ok(new {
                data = mensagem
            });
        }

        [HttpGet("{ident}")]
        public IActionResult GetMensagemPorIdent(int ident)
        {
            var mensagem = this._mensagemService.PesquisaPorIdent(ident);

            if (mensagem == null)
                return NotFound();

            return Ok(new {
                data = mensagem
            });
        }

        [HttpGet("categoria/{categoria}")]
        public IActionResult GetMensagemPorCategoria(string categoria)
        {
            var mensagens = this._mensagemService.PesquisaPorCategoria(categoria);

            if (mensagens == null)
                return NotFound();

            return Ok(new {
                data = mensagens
            });
        }
    }
}