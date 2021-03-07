using System;

namespace Labs.Feedback.API.Filas
{
    public interface IGerenciadorFila
    {
        bool AdicionarItem(string mensagem);
    }
}
