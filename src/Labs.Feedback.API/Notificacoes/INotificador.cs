using System;
using System.Collections.Generic;

namespace Labs.Feedback.API.Notificacoes
{
    public interface INotificador
    {
        void Adicionar(Notificacao notificacao);
        void Adicionar(String mensagem);
        List<Notificacao> ObterNotificacoes();
        bool TemNotificacao();
    }
}