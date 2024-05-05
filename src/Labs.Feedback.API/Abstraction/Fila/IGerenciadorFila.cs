using Labs.Feedback.API.Model;

namespace Labs.Feedback.API.Abstraction.Fila;

public interface IGerenciadorFila
{
    bool AdicionarItem(Mensagem mensagem);
}
