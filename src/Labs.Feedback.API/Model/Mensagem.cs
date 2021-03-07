using System;
using FluentValidator;

namespace Labs.Feedback.API.Model
{
    public class Mensagem
    {
        public Mensagem(int ident, string texto, Categoria categoria)
        {
            Ident = ident;
            Texto = texto;
            Categoria = categoria;
        }

        public int Ident { get; set; }
        public string Texto { get; set; }
        public Categoria Categoria { get; set; }
    }
}