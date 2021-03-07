using System;
using System.Collections.Generic;
using System.Linq;
using Labs.Feedback.API.Dto;
using Labs.Feedback.API.Model;
using Labs.Feedback.API.Notificacoes;
using Labs.Feedback.API.Services;
using Labs.Feedback.API.UnitTests._Builders;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Labs.Feedback.API.UnitTests.Controllers
{
    public class FeedbackControllerTests
    {
        [Fact]
        public void PostCadastrarMensagem_CadastrarMensagemValida_MensagemComHttpStatusCode201()
        {
            // Arrange
            var mensagemDto = MensagemDtoBuilder.Criar().Build();

            var mockMensagemServices = new Mock<IMensagemService>();
            mockMensagemServices.Setup(m => m.CadastrarMensagem(It.IsAny<MensagemDto>())).Returns(mensagemDto);

            var mockNotificador = new Mock<INotificador>();
            mockNotificador.Setup(m => m.TemNotificacao()).Returns(false);

            var controller = new FeedbackController(mockMensagemServices.Object, mockNotificador.Object);

            // Act
            var response = controller.PostCadastrarMensagem(mensagemDto) as ObjectResult;
            var data = response.GetData<MensagemDto>();

            // Assert
            Assert.Equal(201, response.StatusCode);
            Assert.Equal(mensagemDto, data);
        }

        [Fact]
        public void PostCadastrarMensagem_CadastrarMensagemComDescricaoInvalida_NotificacaoDeDescricaoEHttpStatusCode422()
        {
            // Arrange
            var mensagemDto = MensagemDtoBuilder.Criar().Build();

            var mockMensagemServices = new Mock<IMensagemService>();
            mockMensagemServices.Setup(m => m.CadastrarMensagem(It.IsAny<MensagemDto>())).Returns(mensagemDto);

            var mockNotificador = new Mock<INotificador>();
            mockNotificador.Setup(m => m.TemNotificacao()).Returns(true);
            mockNotificador.Setup(m => m.ObterNotificacoes()).Returns(new List<Notificacao> { new Notificacao("descricao invalida") });

            var controller = new FeedbackController(mockMensagemServices.Object, mockNotificador.Object);

            // Act
            var response = controller.PostCadastrarMensagem(mensagemDto) as ObjectResult;
            var data = response.GetData<List<Notificacao>>();

            // Assert
            Assert.Equal(422, response.StatusCode);
            Assert.Single(data);
        }

        [Fact]
        public void PostCadastrarMensagem_CadastrarMensagemComErroDuranteProcessamento_HttpStatusCode400()
        {
            // Arrange
            var mensagemDto = MensagemDtoBuilder.Criar().Build();
            var mockMensagemServices = new Mock<IMensagemService>();
            var mockNotificador = new Mock<INotificador>();
            mockNotificador.Setup(m => m.TemNotificacao()).Returns(false);

            var controller = new FeedbackController(mockMensagemServices.Object, mockNotificador.Object);

            // Act
            var response = controller.PostCadastrarMensagem(mensagemDto);

            // Assert
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void GetMensagemPorIdent_PesquisaMensagemValida_MensagemComHttpStatusCode200()
        {
            // Arrange
            var mensagemDto = MensagemDtoBuilder.Criar().Build();

            var mockMensagemServices = new Mock<IMensagemService>();
            mockMensagemServices.Setup(m => m.PesquisaPorIdent(It.IsAny<String>())).Returns(mensagemDto);

            var mockNotificador = new Mock<INotificador>();
            mockNotificador.Setup(m => m.TemNotificacao()).Returns(false);

            var controller = new FeedbackController(mockMensagemServices.Object, mockNotificador.Object);

            // Act
            var response = controller.GetMensagemPorIdent(mensagemDto.Ident) as ObjectResult;
            var data = response.GetData<MensagemDto>();

            // Assert
            Assert.Equal(200, response.StatusCode);
            Assert.Equal(mensagemDto, data);
        }

        [Fact]
        public void GetMensagemPorIdent_PesquisaMensagemComIdentInvalido_NotificacaoDeIdentInvalidoEHttpStatusCode422()
        {
            // Arrange
            var mensagemDto = MensagemDtoBuilder.Criar().Build();

            var mockMensagemServices = new Mock<IMensagemService>();
            mockMensagemServices.Setup(m => m.PesquisaPorIdent(It.IsAny<String>())).Returns(mensagemDto);

            var mockNotificador = new Mock<INotificador>();
            mockNotificador.Setup(m => m.TemNotificacao()).Returns(true);
            mockNotificador.Setup(m => m.ObterNotificacoes()).Returns(new List<Notificacao> { new Notificacao("ident invalido") });

            var controller = new FeedbackController(mockMensagemServices.Object, mockNotificador.Object);

            // Act
            var response = controller.GetMensagemPorIdent(mensagemDto.Ident) as ObjectResult;
            var data = response.GetData<List<Notificacao>>();

            // Assert
            Assert.Equal(422, response.StatusCode);
            Assert.Single(data);
        }

        [Fact]
        public void GetMensagemPorIdent_PesquisaMensagemComIdentInexistente_HttpStatusCode404()
        {
            // Arrange
            var mensagemDto = MensagemDtoBuilder.Criar().Build();
            var mockMensagemServices = new Mock<IMensagemService>();
            var mockNotificador = new Mock<INotificador>();
            mockNotificador.Setup(m => m.TemNotificacao()).Returns(false);

            var controller = new FeedbackController(mockMensagemServices.Object, mockNotificador.Object);

            // Act
            var response = controller.GetMensagemPorIdent(mensagemDto.Ident);

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public void GetMensagemPorCategoria_PesquisaMensagemPorCategoriaValida_ListaDeMensagensEHttpStatusCode200()
        {
            // Arrange
            var mensagemDto = MensagemDtoBuilder.Criar().Build();

            var mockMensagemServices = new Mock<IMensagemService>();
            mockMensagemServices
                .Setup(m => m.PesquisaPorCategoria(It.IsAny<string>()))
                .Returns(new List<MensagemDto> {
                    MensagemDtoBuilder.Criar().ComCategoria("ERRO").Build(),
                    MensagemDtoBuilder.Criar().ComCategoria("ERRO").Build(),
                    MensagemDtoBuilder.Criar().ComCategoria("ERRO").Build()
                });

            var mockNotificador = new Mock<INotificador>();
            mockNotificador.Setup(m => m.TemNotificacao()).Returns(false);

            var controller = new FeedbackController(mockMensagemServices.Object, mockNotificador.Object);

            // Act
            var response = controller.GetMensagemPorCategoria("ERRO") as ObjectResult;
            var data = response.GetData<List<MensagemDto>>();

            // Assert
            Assert.Equal(200, response.StatusCode);
            Assert.Equal(3, data.Count());
        }

        [Fact]
        public void GetMensagemPorCategoria_PesquisaMensagemPorCategoriaInvalida_NotificacaoDeCategoriaEHttpStatusCode422()
        {
            // Arrange
            var mensagemDto = MensagemDtoBuilder.Criar().Build();

            var mockMensagemServices = new Mock<IMensagemService>();
            mockMensagemServices.Setup(m => m.PesquisaPorCategoria(It.IsAny<string>()));

            var mockNotificador = new Mock<INotificador>();
            mockNotificador.Setup(m => m.TemNotificacao()).Returns(true);
            mockNotificador.Setup(m => m.ObterNotificacoes()).Returns(new List<Notificacao> { new Notificacao("categoria invalida") });

            var controller = new FeedbackController(mockMensagemServices.Object, mockNotificador.Object);

            // Act
            var response = controller.GetMensagemPorCategoria("qualquer-coisa") as ObjectResult;
            var data = response.GetData<List<Notificacao>>();

            // Assert
            Assert.Equal(422, response.StatusCode);
            Assert.Single(data);
        }

        [Fact]
        public void GetMensagemPorCategoria_PesquisaMensagemPorCategoriaInexistente_HttpStatusCode404()
        {
            // Arrange
            var mensagemDto = MensagemDtoBuilder.Criar().Build();
            var mockMensagemServices = new Mock<IMensagemService>();
            var mockNotificador = new Mock<INotificador>();
            mockNotificador.Setup(m => m.TemNotificacao()).Returns(false);

            var controller = new FeedbackController(mockMensagemServices.Object, mockNotificador.Object);

            // Act
            var response = controller.GetMensagemPorCategoria("ERRO");

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }
    }
}