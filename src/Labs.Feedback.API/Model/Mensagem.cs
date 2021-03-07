using System;

namespace Labs.Feedback.API.Model
{
    public class Mensagem : EntityBase
    {
        public string Descricao { get; set; }
        public Categoria Categoria { get; set; }
    }
}