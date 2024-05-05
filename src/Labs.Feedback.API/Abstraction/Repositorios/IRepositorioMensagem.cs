using Labs.Feedback.API.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Labs.Feedback.API.Abstraction.Repositorios;

public interface IRepositorioMensagem
{
    bool AdicionarMensagem(Mensagem mensagem);
    Mensagem PesquisaPorIdent(Guid ident);
    IEnumerable<Mensagem> PesquisaPor(Expression<Func<Mensagem, bool>> condicao);
}
