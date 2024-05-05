using Labs.Feedback.API.Dto;
using System.Collections.Generic;

namespace Labs.Feedback.API.Abstraction.Services;

public interface IMensagemService
{
    MensagemDto CadastrarMensagem(MensagemDto mensagemDto);

    MensagemDto PesquisaPorIdent(string ident);

    IEnumerable<MensagemDto> PesquisaPorCategoria(string textoCategoria);
}