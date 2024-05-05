using Labs.Feedback.API.Abstraction.Fila;
using Labs.Feedback.API.Model;

namespace Labs.Feedback.API.UtilTest;

public class GerenciadorFilaFake : IGerenciadorFila
{
    public bool AdicionarItem(Mensagem mensagem)
    {
        return true;
    }
}
