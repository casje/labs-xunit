using System;
using Microsoft.Extensions.Logging;

namespace Labs.Feedback.API.Filas
{
    public class GerenciadorFila : IGerenciadorFila
    {
        private readonly ILogger<GerenciadorFila> _logger;

        public GerenciadorFila(ILogger<GerenciadorFila> logger)
        {
            _logger = logger;
        }

        public bool AdicionarItem(string mensagem)
        {
            if(String.IsNullOrEmpty(mensagem))
            {
                return false;
            }

            _logger.LogInformation($"Novo item adicionado a fila: {mensagem}");
            return true;
        }
    }
}
