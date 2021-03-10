using System;
using Labs.Feedback.API.Model;
using Labs.Feedback.API.Filas;

namespace Labs.Feedback.API.UtilTest
{
    public class GerenciadorFilaFake : IGerenciadorFila
    {
        public bool AdicionarItem(Mensagem mensagem)
        {
            return true;
        }
    }
}
