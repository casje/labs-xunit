using Labs.Feedback.API.Abstraction.Fila;
using Labs.Feedback.API.Model;
using Microsoft.Extensions.Logging;

namespace Labs.Feedback.API.Filas;

internal class GerenciadorFila : IGerenciadorFila
{
    private readonly ILogger<GerenciadorFila> _logger;

    public GerenciadorFila(ILogger<GerenciadorFila> logger)
    {
        _logger = logger;
    }

    public bool AdicionarItem(Mensagem mensagem)
    {
        if(mensagem == null)
            return false;

        _logger.LogInformation($"Novo item adicionado a fila: [{mensagem.Categoria}] {mensagem.Ident} - {mensagem.Descricao}");
        return true;
    }
}
