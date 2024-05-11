using Labs.Feedback.API.Abstraction.Fila;
using Labs.Feedback.API.Abstraction.Notificacoes;
using Labs.Feedback.API.Abstraction.Repositorios;
using Labs.Feedback.API.Abstraction.Services;
using Labs.Feedback.API.Context;
using Labs.Feedback.API.Filas;
using Labs.Feedback.API.Notificacoes;
using Labs.Feedback.API.Repositorios;
using Labs.Feedback.API.Services;

namespace Labs.Feedback.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
       // builder.Services.AddAuthorization();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAutoMapper();
        builder.Services.AddScoped<INotificador, Notificador>();

        builder.Services.AddScoped<IMensagemService, MensagemService>();
        builder.Services.AddScoped<IRepositorioMensagem, RepositorioMensagem>();
        builder.Services.AddScoped<IGerenciadorFila, GerenciadorFila>();

        builder.Services.AddDbContext<AppDbContext>(ServiceLifetime.Singleton);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // app.UseHttpsRedirection();
        // app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}