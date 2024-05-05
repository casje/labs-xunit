using Labs.Feedback.API.Notificacoes;
using System.Collections.Generic;

namespace Labs.Feedback.API.Abstraction.Notificacoes;

public interface INotificador
{
    void Adicionar(Notificacao notificacao);
    void Adicionar(string mensagem);
    List<Notificacao> ObterNotificacoes();
    bool TemNotificacao();
}