using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Labs.Feedback.API.Dto;
using Labs.Feedback.API.Model;

namespace Labs.Feedback.API.Services
{
    public interface IMensagemService
    {
        MensagemDto CadastrarMensagem(MensagemDto mensagemDto);

        MensagemDto PesquisaPorIdent(Guid ident);

        IEnumerable<MensagemDto> PesquisaPorCategoria(string textoCategoria);
    }
}