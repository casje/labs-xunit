using AutoMapper;
using Labs.Feedback.API.Dto;
using Labs.Feedback.API.Extensions;
using Labs.Feedback.API.Model;

namespace Labs.Feedback.API.UnitTests;

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
        var ident = Guid.NewGuid();
        var mensagemDto = new MensagemDto()
        {
            Ident = ident.ToString(),
            Descricao = "Mensagem de feedback",
            Categoria = "ELOGIO"
        };

        // Act
        var mensagemModel = _mapper.Map<Mensagem>(mensagemDto);

        // Assert
        Assert.Equal(ident, mensagemModel.Ident);
        Assert.Equal("Mensagem de feedback", mensagemModel.Descricao);
        Assert.Equal(Categoria.ELOGIO, mensagemModel.Categoria);
    }

    [Fact]
    public void AutoMapperProfile_ConverterMensagemModelParaMensagemDto_True()
    {
        // Arrange
        var ident = Guid.NewGuid();
        var mensagem = new Mensagem
        {
            Ident = ident,
            Descricao = "Mensagem de feedback",
            Categoria = Categoria.ERRO
        };

        // Act
        var mensagemDto = _mapper.Map<MensagemDto>(mensagem);

        // Assert
        Assert.Equal(ident, mensagemDto.Ident.ToGuid());
        Assert.Equal("Mensagem de feedback", mensagemDto.Descricao);
        Assert.Equal("ERRO", mensagemDto.Categoria);
    }

    [Fact]
    public void AutoMapperProfile_ConverterMensagemDtoParaMensagemModelComCategoriaInvalida_CategoriaNENHUMA()
    {
        // Arrange
        var ident = Guid.NewGuid();
        var mensagemDto = new MensagemDto
        {
            Ident = ident.ToString(),
            Descricao = "Mensagem de feedback",
            Categoria = "qualquer-coisa"
        };

        // Act
        var mensagemModel = _mapper.Map<Mensagem>(mensagemDto);

        // Assert
        Assert.Equal(Categoria.NENHUMA, mensagemModel.Categoria);
    }
}