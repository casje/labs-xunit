using System;
using System.Text;
using Bogus;
using Labs.Feedback.API.Model;

namespace Labs.Feedback.API.CommonTests
{
    public class MensagemBuilder
    {
        private readonly Faker _faker;
        private Guid _ident;
        private string _descricao;
        private Categoria _categoria;

        public MensagemBuilder()
        {
            _faker = new Faker("pt_BR");

            _ident = Guid.NewGuid();
            _descricao = GerarTexto(20);
            _categoria = Categoria.DUVIDA;
        }

        public static MensagemBuilder Criar()
        {
            return new MensagemBuilder();
        }

        public MensagemBuilder ComIdent(Guid ident)
        {
            _ident = ident;
            return this;
        }

        public MensagemBuilder ComCategoria(Categoria categoria)
        {
            _categoria = categoria;
            return this;
        }

        public MensagemBuilder ComDescricao(string descricao)
        {
            _descricao = descricao;
            return this;
        }

        public MensagemBuilder ComDescricao(int tamanhoMinimoTexto)
        {
            _descricao = GerarTexto(tamanhoMinimoTexto);
            return this;
        }

        public Mensagem Build()
        {
            var mensagem = new Mensagem
            {
                Ident = _ident,
                Descricao = _descricao,
                Categoria = _categoria
            };

            return mensagem;
        }

        private string GerarTexto(int tamanhoMinimoTexto)
        {
            StringBuilder strTexto = new StringBuilder();

            while (strTexto.Length < tamanhoMinimoTexto)
                strTexto.Append($"{_faker.Lorem.Word()} ");

            return strTexto.ToString();
        }
    }
}