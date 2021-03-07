using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Labs.Feedback.API.Dto;
using Labs.Feedback.API.Extensions;
using Labs.Feedback.API.Model;
using Labs.Feedback.API.Repositorio;

namespace Labs.Feedback.API.Services
{
    public class MensagemService : IMensagemService
    {
        private readonly IMapper _mapper;
        private readonly IRepositorioMensagem _repositorioMensagem;

        public MensagemService(IMapper mapper
                             , IRepositorioMensagem repositorioMensagem)
        {
            this._mapper = mapper;
            this._repositorioMensagem = repositorioMensagem;
        }

        public MensagemDto CadastrarMensagem(MensagemDto mensagemDto)
        {
            var mensagem = _mapper.Map<Mensagem>(mensagemDto);

            if (mensagem != null)
            {
                this._repositorioMensagem.AdicionarMensagem(mensagem);
            }

            return _mapper.Map<MensagemDto>(mensagem);
        }

        public MensagemDto PesquisaPorIdent(int ident)
        {
            if (ident < 1) return null;

            var mensagem = this._repositorioMensagem.PesquisaPorIdent(ident);

            return _mapper.Map<MensagemDto>(mensagem);
        }

        public IEnumerable<MensagemDto> PesquisaPorCategoria(string textoCategoria)
        {
            var categoria = textoCategoria.ToCategoria();

            if (categoria == Categoria.NENHUMA) return null;

            var mensagens = this._repositorioMensagem.PesquisaPor(m => m.Categoria == categoria);

            return _mapper.Map<IEnumerable<MensagemDto>>(mensagens);
        }
    }
}
