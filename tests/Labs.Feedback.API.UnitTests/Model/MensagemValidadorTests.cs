using System;
using Labs.Feedback.API.Model;
using Labs.Feedback.API.Model.Validadores;
using Labs.Feedback.API.UtilTest;
using FluentValidation.TestHelper;
using Xunit;

namespace Labs.Feedback.API.UnitTests.Model
{
    public class MensagemValidadorTests
    {
        private MensagemValidador _validador;

        public MensagemValidadorTests()
        {
            _validador = new MensagemValidador();
        }

        [Fact]
        public void MensagemValidador_ValidacaoDoCampoDescricaoValida_SemNotificacaoDeValidacao()
        {
            // Arrange
            var mensagem = MensagemBuilder.Criar().ComDescricao("Descricao da mensagem com tamanho valido").Build();

            // Act
            var resultado = _validador.TestValidate(mensagem);

            // Assert
            Assert.Equal(0, resultado.Errors.Count);
        }

        [Fact]
        public void MensagemValidador_ValidacaoDoCampoDescricaoVazia_NotificacaoDeDescricaoVazia()
        {
            // Arrange
            var mensagem = MensagemBuilder.Criar().ComDescricao("").Build();

            // Act
            var resultado = _validador.TestValidate(mensagem);

            // Assert
            resultado.ShouldHaveValidationErrorFor(m => m.Descricao);
        }

        [Fact]
        public void MensagemValidador_ValidacaoDoCampoDescricaoComTamanhoExcedido_NotificacaoDeDescricaoComTamanhoAcimaDoPermitido()
        {
            // Arrange
            var mensagem = MensagemBuilder.Criar().ComDescricao(110).Build();

            // Act
            var resultado = _validador.TestValidate(mensagem);

            // Assert
            resultado.ShouldHaveValidationErrorFor(m => m.Descricao);
        }

        [Fact]
        public void MensagemValidador_ValidacaoDoCampoCategoriaValida_SemNotificacaoDeValidacao()
        {
            // Arrange
            var mensagem = MensagemBuilder.Criar().ComCategoria(Categoria.ELOGIO).Build();

            // Act
            var resultado = _validador.TestValidate(mensagem);

            // Assert
            Assert.Equal(0, resultado.Errors.Count);
        }

        [Fact]
        public void MensagemValidador_ValidacaoDoCampoCategoriaInvalida_NotificacaoDeCategoriaInvalida()
        {
            // Arrange
            var mensagem = MensagemBuilder.Criar().ComCategoria(Categoria.NENHUMA).Build();

            // Act
            var resultado = _validador.TestValidate(mensagem);

            // Assert
            resultado.ShouldHaveValidationErrorFor(m => m.Categoria);
        }

    }
}