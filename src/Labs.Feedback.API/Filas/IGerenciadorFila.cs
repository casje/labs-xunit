using System;
using Labs.Feedback.API.Model;

namespace Labs.Feedback.API.Filas
{
    public interface IGerenciadorFila
    {
        bool AdicionarItem(Mensagem mensagem);
    }
}
