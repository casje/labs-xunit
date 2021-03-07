using System;
using AutoMapper;
using Labs.Feedback.API.Dto;
using Labs.Feedback.API.Extensions;
using Labs.Feedback.API.Model;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Labs.Feedback.API.UnitTests
{
    public class AutoMapperProfileTests
    {
        private readonly IMapper _mapper;

        public AutoMapperProfileTests()
        {
            var services = new ServiceCollection();
            services.AddAutoMapper();

            var scope = services.BuildServiceProvider().CreateScope();
            _mapper = scope.ServiceProvider.GetService<IMapper>();
        }


        [Fact]
        public void AutoMapperProfile_ConverterMensagemDtoParaMensagemModel_True()
        {
            // Arrange
            var mensagemDto = new MensagemDto()
            {
                Ident = 1,
                Texto = "Mensagem de feedback",
                Categoria = "ELOGIO"
            };

            // Act
            var mensagemModel = _mapper.Map<Mensagem>(mensagemDto);

            // Assert
            Assert.Equal(1, mensagemModel.Ident);
            Assert.Equal("Mensagem de feedback", mensagemModel.Texto);
            Assert.Equal(Categoria.ELOGIO, mensagemModel.Categoria);
        }

        [Fact]
        public void AutoMapperProfile_ConverterMensagemModelParaMensagemDto_True()
        {
            // Arrange
            var mensagem = new Mensagem(
                ident: 1,
                texto: "Mensagem de feedback",
                categoria: Categoria.ERRO
            );

            // Act
            var mensagemDto = _mapper.Map<MensagemDto>(mensagem);

            // Assert
            Assert.Equal(1, mensagemDto.Ident);
            Assert.Equal("Mensagem de feedback", mensagemDto.Texto);
            Assert.Equal("ERRO", mensagemDto.Categoria);
        }

        [Fact]
        public void AutoMapperProfile_ConverterMensagemDtoParaMensagemModelComCategoriaInvalida_CategoriaNENHUMA()
        {
            // Arrange
            var mensagemDto = new MensagemDto
            {
                Ident = 1,
                Texto = "Mensagem de feedback",
                Categoria = "qualquer-coisa"
            };

            // Act
            var mensagemModel = _mapper.Map<Mensagem>(mensagemDto);

            // Assert
            Assert.Equal(Categoria.NENHUMA, mensagemModel.Categoria);
        }
    }
}