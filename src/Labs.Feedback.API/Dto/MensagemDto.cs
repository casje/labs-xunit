using System;

namespace Labs.Feedback.API.Dto
{
    public class MensagemDto
    {
        public int Ident { get; set; }
        public string Texto { get; set; }
        public string Categoria { get; set; }
    }
}