using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Labs.Feedback.API.Model;

namespace Labs.Feedback.API.Repositorios
{
    public interface IRepositorioMensagem
    {
        Boolean AdicionarMensagem(Mensagem mensagem);
        Mensagem PesquisaPorIdent(Guid ident);
        IEnumerable<Mensagem> PesquisaPor(Expression<Func<Mensagem, bool>> condicao);
    }
}
