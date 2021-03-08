using System;
using AutoMapper;
using Labs.Feedback.API.CommonTests;
using Labs.Feedback.API.Dto;
using Labs.Feedback.API.Filas;
using Labs.Feedback.API.Model;
using Labs.Feedback.API.Notificacoes;
using Labs.Feedback.API.Repositorios;
using Labs.Feedback.API.Services;
using Moq;
using Xunit;

namespace Labs.Feedback.API.UnitTests.Services
{
    public class MensagemServiceTests
    {
        [Fact]
        public void CadastrarMensagem_ValidacaoDasInformacoesDaMensagemComDadosValidos_MensagemDtoComAsInformacoesDeCadastro()
        {
            // Arrange
            var ident = Guid.NewGuid();
            var mensagemDto = MensagemDtoBuilder.Criar().ComIdent(ident.ToString()).ComDescricao("Texto da mensagem").ComCategoria("ERRO").Build();
            var mensagem = MensagemBuilder.Criar().ComIdent(ident).ComDescricao("Texto da mensagem").ComCategoria(Categoria.ERRO).Build();

            var mockNotification = new Mock<INotificador>();

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<Mensagem>(It.IsAny<MensagemDto>())).Returns(mensagem);
            mockMapper.Setup(m => m.Map<MensagemDto>(It.IsAny<Mensagem>())).Returns(mensagemDto);

            var mockRepositorio = new Mock<IRepositorioMensagem>();
            var mockGerenciadorFila = new Mock<IGerenciadorFila>();

            var mensagemService = new MensagemService(mockMapper.Object, mockNotification.Object, mockGerenciadorFila.Object, mockRepositorio.Object);

            // Act
            var mensagemCadastrada = mensagemService.CadastrarMensagem(mensagemDto);

            // Assert
            Assert.Equal(ident.ToString(), mensagemCadastrada.Ident, ignoreCase: true);
            Assert.Equal("Texto da mensagem", mensagemCadastrada.Descricao, ignoreCase: true);
            Assert.Equal("ERRO", mensagemCadastrada.Categoria, ignoreCase: true);
        }

        [Fact]
        public void CadastrarMensagem_ValidacaoDoComportamentoDoCadastroComMensagemValidaECategoriaErro_ComportamentoDeCadastroParaCategoriaErro()
        {
            // Arrange
            var ident = Guid.NewGuid();
            var mensagemDto = MensagemDtoBuilder.Criar().ComIdent(ident.ToString()).ComCategoria("ERRO").Build();
            var mensagem = MensagemBuilder.Criar().ComIdent(ident).ComCategoria(Categoria.ERRO).Build();

            var mockNotification = new Mock<INotificador>();

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<Mensagem>(It.IsAny<MensagemDto>())).Returns(mensagem);
            mockMapper.Setup(m => m.Map<MensagemDto>(It.IsAny<Mensagem>())).Returns(mensagemDto);

            var mockRepositorio = new Mock<IRepositorioMensagem>();
            mockRepositorio.Setup(m => m.AdicionarMensagem(mensagem)).Returns(true);

            var mockGerenciadorFila = new Mock<IGerenciadorFila>();
            mockGerenciadorFila.Setup(m => m.AdicionarItem(mensagem)).Returns(true);

            var mensagemService = new MensagemService(mockMapper.Object, mockNotification.Object, mockGerenciadorFila.Object, mockRepositorio.Object);

            // Act
            var mensagemCadastrada = mensagemService.CadastrarMensagem(mensagemDto);

            // Assert
            mockMapper.Verify(m => m.Map<Mensagem>(It.Is<MensagemDto>(x => x.Ident == ident.ToString())), Times.Once());
            mockRepositorio.Verify(m => m.AdicionarMensagem(It.Is<Mensagem>(x => x.Ident == ident)), Times.Once());
            mockGerenciadorFila.Verify(m => m.AdicionarItem(It.Is<Mensagem>(x => x.Ident == ident)), Times.Once());
            mockMapper.Verify(m => m.Map<MensagemDto>(It.Is<Mensagem>(x => x.Ident == ident)), Times.Once());
            Assert.NotNull(mensagemCadastrada);
        }

        [Fact]
        public void CadastrarMensagem_ValidacaoDoComportamentoDoCadastroComMensagemValidaECategoriaDuvida_ComportamentoDeCadastroParaCategoriaDuvida()
        {
            // Arrange
            var ident = Guid.NewGuid();
            var mensagemDto = MensagemDtoBuilder.Criar().ComIdent(ident.ToString()).ComCategoria("Duvida").Build();
            var mensagem = MensagemBuilder.Criar().ComIdent(ident).ComCategoria(Categoria.DUVIDA).Build();

            var mockNotification = new Mock<INotificador>();

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<Mensagem>(It.IsAny<MensagemDto>())).Returns(mensagem);
            mockMapper.Setup(m => m.Map<MensagemDto>(It.IsAny<Mensagem>())).Returns(mensagemDto);

            var mockRepositorio = new Mock<IRepositorioMensagem>();
            mockRepositorio.Setup(m => m.AdicionarMensagem(mensagem)).Returns(true);

            var mockGerenciadorFila = new Mock<IGerenciadorFila>();
            mockGerenciadorFila.Setup(m => m.AdicionarItem(mensagem)).Returns(true);

            var mensagemService = new MensagemService(mockMapper.Object, mockNotification.Object, mockGerenciadorFila.Object, mockRepositorio.Object);

            // Act
            var mensagemCadastrada = mensagemService.CadastrarMensagem(mensagemDto);

            // Assert
            mockMapper.Verify(m => m.Map<Mensagem>(It.Is<MensagemDto>(x => x.Ident == ident.ToString())), Times.Once());
            mockRepositorio.Verify(m => m.AdicionarMensagem(It.Is<Mensagem>(x => x.Ident == ident)), Times.Once());
            mockGerenciadorFila.Verify(m => m.AdicionarItem(It.IsAny<Mensagem>()), Times.Never());
            mockMapper.Verify(m => m.Map<MensagemDto>(It.Is<Mensagem>(x => x.Ident == ident)), Times.Once());
            Assert.NotNull(mensagemCadastrada);
        }

        [Fact]
        public void CadastrarMensagem_ValidacaoDoComportamentoDoCadastroDeMensagemComCategoriaInvalida_NotificacaoDeCategoriaInvalidaEMensagemNula()
        {
            // Arrange
            var ident = Guid.NewGuid();
            var mensagemDto = MensagemDtoBuilder.Criar().ComIdent(ident.ToString()).ComCategoria("qualquer-coisa").Build();
            var mensagem = MensagemBuilder.Criar().ComIdent(ident).ComCategoria(Categoria.NENHUMA).Build();

            var mockNotification = new Mock<INotificador>();
            mockNotification.Setup(m => m.Adicionar(It.IsAny<Notificacao>()));

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<Mensagem>(It.IsAny<MensagemDto>())).Returns(mensagem);
            mockMapper.Setup(m => m.Map<MensagemDto>(It.IsAny<Mensagem>())).Returns(mensagemDto);

            var mockRepositorio = new Mock<IRepositorioMensagem>();
            var mockGerenciadorFila = new Mock<IGerenciadorFila>();

            var mensagemService = new MensagemService(mockMapper.Object, mockNotification.Object, mockGerenciadorFila.Object, mockRepositorio.Object);

            // Act
            var mensagemCadastrada = mensagemService.CadastrarMensagem(mensagemDto);

            // Assert
            mockMapper.Verify(m => m.Map<Mensagem>(It.Is<MensagemDto>(x => x.Ident == ident.ToString())), Times.Once());
            mockNotification.Verify(m => m.Adicionar(It.IsAny<Notificacao>()), Times.Once());
            Assert.Null(mensagemCadastrada);
            mockRepositorio.Verify(m => m.AdicionarMensagem(It.IsAny<Mensagem>()), Times.Never());
            mockGerenciadorFila.Verify(m => m.AdicionarItem(It.IsAny<Mensagem>()), Times.Never());
            mockMapper.Verify(m => m.Map<MensagemDto>(It.Is<Mensagem>(x => x.Ident == ident)), Times.Never());
        }
    }
}
