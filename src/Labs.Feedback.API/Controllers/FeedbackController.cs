using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Labs.Feedback.API.Dto;
using Labs.Feedback.API.Notificacoes;
using Labs.Feedback.API.Services;
using System.Linq;

namespace Labs.Feedback.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IMensagemService _mensagemService;
        private readonly INotificador _notificador;

        public FeedbackController(IMensagemService mensagemService
                                , INotificador notificador)
        {
            this._mensagemService = mensagemService;
            this._notificador = notificador;
        }

        [HttpPost]
        public IActionResult PostCadastrarMensagem(MensagemDto mensagemDto)
        {
            var mensagem = this._mensagemService.CadastrarMensagem(mensagemDto);

            if (_notificador.TemNotificacao())
            {
                return StatusCode(422, new {
                    data = _notificador.ObterNotificacoes()
                });
            }

            if (mensagem == null)
                return BadRequest();

            return StatusCode(201, new {
                data = mensagem
            });
        }

        [HttpGet("{ident}")]
        public IActionResult GetMensagemPorIdent(string ident)
        {
            var mensagem = this._mensagemService.PesquisaPorIdent(ident);

            if (_notificador.TemNotificacao())
            {
                return StatusCode(422, new
                {
                    data = _notificador.ObterNotificacoes()
                });
            }

            if (mensagem == null)
                return NotFound();

            return Ok(new
            {
                data = mensagem
            });
        }

        [HttpGet("categoria/{categoria}")]
        public IActionResult GetMensagemPorCategoria(string categoria)
        {
            var mensagens = this._mensagemService.PesquisaPorCategoria(categoria);

            if (_notificador.TemNotificacao())
            {
                return StatusCode(422, new
                {
                    data = _notificador.ObterNotificacoes()
                });
            }

            if (! (mensagens != null && mensagens.Any()))
                return NotFound();

            return Ok(new
            {
                data = mensagens
            });
        }
    }
}