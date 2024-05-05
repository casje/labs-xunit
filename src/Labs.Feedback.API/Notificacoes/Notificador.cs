using Labs.Feedback.API.Abstraction.Notificacoes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Labs.Feedback.API.Notificacoes;

internal class Notificador : INotificador
{
    private List<Notificacao> _notificacoes;

    public Notificador()
    {
        _notificacoes = new List<Notificacao>();
    }

    public void Adicionar(Notificacao notificacao)
    {
        _notificacoes.Add(notificacao);
    }

    public void Adicionar(String mensagem)
    {
        var notificacao = new Notificacao(mensagem);
        Adicionar(notificacao);
    }

    public List<Notificacao> ObterNotificacoes()
    {
        return _notificacoes;
    }

    public bool TemNotificacao()
    {
        return _notificacoes.Any();
    }
}