using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Labs.Feedback.API.Context;
using Labs.Feedback.API.Services;
using Labs.Feedback.API.Repositorios;
using Labs.Feedback.API.Notificacoes;
using Labs.Feedback.API.Filas;
using Labs.Feedback.API.Model;
using Labs.Feedback.API.UtilTest;
using Microsoft.AspNetCore.TestHost;
using Moq;
using Xunit;
using System.Net.Http;
using System.Text;
using System.Net.Mime;
using System.IO;
using System.Net;

namespace Labs.Feedback.API.IntegrationTests
{
    public class CasoDeUsoFeedback
    {
        private IHostBuilder CriarHostBuilderComGerenciadorFake()
        {
            var gerenciadorFilaFake = new GerenciadorFilaFake();
            return CriarHostBuilderComGerenciador(gerenciadorFilaFake);
        }
        private IHostBuilder CriarHostBuilderComGerenciador(IGerenciadorFila gerenciadorFila)
        {
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    // Add TestServer
                    webHost.UseTestServer();
                    webHost.ConfigureServices(services =>
                    {
                        services.AddControllers();
                        services.AddAutoMapper();
                        services.AddScoped<INotificador, Notificador>();
                        services.AddScoped<IMensagemService, MensagemService>();
                        services.AddScoped<IRepositorioMensagem, RepositorioMensagem>();

                        // Gerenciador de Fila "fake"
                        services.AddScoped<IGerenciadorFila>(sp => gerenciadorFila);
                        // Banco de dados em memória
                        services.AddDbContext<AppDbContext>(options =>
                        {
                            options.UseInMemoryDatabase(String.Concat("dbtest-", new Random().Next(10000000, 99999999)));
                        });
                    });
                    webHost.Configure(app =>
                    {
                        app.UseRouting();
                        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
                    });
                });
            return hostBuilder;
        }

        [Fact]
        public async Task CadastrarMensagem_RegistrarNovaMensagemDeFeedbackComCategoriaDeErro_RetornarHttpStatusCode201()
        {
            // Arrange
            var hostBuilder = CriarHostBuilderComGerenciadorFake();
            var host = await hostBuilder.StartAsync();

            var client = host.GetTestClient();

            var mensagemDto = MensagemDtoBuilder.Criar().ComCategoria("Erro").Build();
            var body = new StringContent(MensagemDtoBuilder.ToJson(mensagemDto), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("http://exemplo.com/api/feedback", body);
            var mensagemRetorno = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Contains(mensagemDto.Descricao, mensagemRetorno);
        }


        [Fact]
        public async Task CadastrarMensagem_RegistrarNovaMensagemComCategoriaInvalida_RetornarHttpStatusCode422()
        {
            // Arrange
            var hostBuilder = CriarHostBuilderComGerenciadorFake();
            var host = await hostBuilder.StartAsync();

            var client = host.GetTestClient();

            var mensagemDto = MensagemDtoBuilder.Criar().ComCategoria("qualquer-coisa").Build();
            var body = new StringContent(MensagemDtoBuilder.ToJson(mensagemDto), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("http://exemplo.com/api/feedback", body);
            var mensagemRetorno = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode); //422
            Assert.Contains(Mensagens.CATEGORIA_INVALIDA, mensagemRetorno);
        }
    }
}
