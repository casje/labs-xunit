using Labs.Feedback.API.Context;
using Labs.Feedback.API.Model;
using Labs.Feedback.API.Repositorios;
using Labs.Feedback.API.UtilTest;
using Microsoft.EntityFrameworkCore;

namespace Labs.Feedback.API.UnitTests.Repositorios;

public class RepositorioMensagemTests
{
    private readonly AppDbContext _dbContextTest;

    public RepositorioMensagemTests()
    {
        string databaseName = String.Concat("dbtestemensagem", new Random().Next(10000000, 99999999));
        var dbBuilder = new DbContextOptionsBuilder<AppDbContext>();
        dbBuilder.UseInMemoryDatabase(databaseName: databaseName);

        _dbContextTest = new AppDbContext(dbBuilder.Options);
    }

    [Fact]
    public void AdicionarMensagem_AdicionarNovoRegistroDeMensagemNoBancoDeDados_RetornoDaInclusaoIgualTrue()
    {
        // Arrange
        var mensagem = MensagemBuilder.Criar().Build();

        var repositorioMensagem = new RepositorioMensagem(_dbContextTest);

        // Act
        var resultadoOperacao = repositorioMensagem.AdicionarMensagem(mensagem);

        // Assert
        Assert.True(resultadoOperacao);
    }

    [Fact]
    public void AdicionarMensagem_AdicionarRegistrosComAMesmaChavePrimaria_RetornoExceptionDoTipoInvalidOperationException()
    {
        // Arrange
        var chavePrimaria = Guid.NewGuid();
        var mensagem = MensagemBuilder.Criar().ComIdent(chavePrimaria).Build();
        var mensagemRepetida = MensagemBuilder.Criar().ComIdent(chavePrimaria).Build();

        var repositorioMensagem = new RepositorioMensagem(_dbContextTest);

        // Act
        var resultadoOperacao = repositorioMensagem.AdicionarMensagem(mensagem);

        var exception = Assert.Throws<InvalidOperationException>(() => repositorioMensagem.AdicionarMensagem(mensagemRepetida));

        // Assert
        Assert.True(resultadoOperacao);
        Assert.IsType<InvalidOperationException>(exception);
    }

    [Fact]
    public void PesquisaPorIdent_ConsultarUmRegistroAPartirDaChavePrimaria_MensagemComDadosCadastrados()
    {
        // Arrange
        var chavePrimariaErro = Guid.NewGuid();
        var chavePrimariaElogio = Guid.NewGuid();
        var mensagemErro = MensagemBuilder.Criar().ComIdent(chavePrimariaErro).ComDescricao("Mensagem ERRO UnitTest").ComCategoria(Categoria.ERRO).Build();
        var mensagemElogio = MensagemBuilder.Criar().ComIdent(chavePrimariaElogio).ComDescricao("Mensagem ELOGIO UnitTest").ComCategoria(Categoria.ELOGIO).Build();

        var repositorioMensagem = new RepositorioMensagem(_dbContextTest);
        repositorioMensagem.AdicionarMensagem(mensagemErro);
        repositorioMensagem.AdicionarMensagem(mensagemElogio);

        // Act
        var mensagemErroResultado = repositorioMensagem.PesquisaPorIdent(chavePrimariaErro);
        var mensagemElogioResultado = repositorioMensagem.PesquisaPorIdent(chavePrimariaElogio);

        // Assert
        Assert.Equal(chavePrimariaErro, mensagemErroResultado.Ident);
        Assert.Equal(Categoria.ERRO, mensagemErroResultado.Categoria);
        Assert.Equal("Mensagem ERRO UnitTest", mensagemErroResultado.Descricao);

        Assert.Equal(chavePrimariaElogio, mensagemElogioResultado.Ident);
        Assert.Equal(Categoria.ELOGIO, mensagemElogioResultado.Categoria);
        Assert.Equal("Mensagem ELOGIO UnitTest", mensagemElogioResultado.Descricao);
    }
}
