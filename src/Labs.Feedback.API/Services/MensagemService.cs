using AutoMapper;
using Labs.Feedback.API.Abstraction.Fila;
using Labs.Feedback.API.Abstraction.Notificacoes;
using Labs.Feedback.API.Abstraction.Repositorios;
using Labs.Feedback.API.Abstraction.Services;
using Labs.Feedback.API.Dto;
using Labs.Feedback.API.Extensions;
using Labs.Feedback.API.Model;
using Labs.Feedback.API.Model.Validadores;
using System;
using System.Collections.Generic;

namespace Labs.Feedback.API.Services;

internal class MensagemService : BaseService, IMensagemService
{
    private readonly IMapper _mapper;
    private readonly INotificador _notificador;
    private readonly IGerenciadorFila _gerenciadorFila;
    private readonly IRepositorioMensagem _repositorioMensagem;

    public MensagemService(IMapper mapper
                         , INotificador notificador
                         , IGerenciadorFila gerenciadorFila
                         , IRepositorioMensagem repositorioMensagem) : base(notificador)
    {
        this._mapper = mapper;
        this._notificador = notificador;
        this._gerenciadorFila = gerenciadorFila;
        this._repositorioMensagem = repositorioMensagem;
    }

    public MensagemDto CadastrarMensagem(MensagemDto mensagemDto)
    {
        var mensagem = _mapper.Map<Mensagem>(mensagemDto);

        if (!ExecutarValidacao(new MensagemValidador(), mensagem)) return null;

        this._repositorioMensagem.AdicionarMensagem(mensagem);

        if (mensagem?.Categoria == Categoria.ERRO)
            this._gerenciadorFila.AdicionarItem(mensagem);

        return _mapper.Map<MensagemDto>(mensagem);
    }

    public MensagemDto PesquisaPorIdent(string ident)
    {
        Guid identGuid = ident.ToGuid();

        if (identGuid == Guid.Empty)
        {
            this._notificador.Adicionar("Para a pesquisa deve informar um identificador válido");
            return null;
        }

        var mensagem = this._repositorioMensagem.PesquisaPorIdent(identGuid);

        return _mapper.Map<MensagemDto>(mensagem);
    }

    public IEnumerable<MensagemDto> PesquisaPorCategoria(string textoCategoria)
    {
        var categoria = textoCategoria.ToCategoria();

        if (categoria == Categoria.NENHUMA)
        {
            this._notificador.Adicionar($"A categoria {textoCategoria} é inválida");
            return null;
        }

        var mensagens = this._repositorioMensagem.PesquisaPor(m => m.Categoria == categoria);

        return _mapper.Map<IEnumerable<MensagemDto>>(mensagens);
    }
}
