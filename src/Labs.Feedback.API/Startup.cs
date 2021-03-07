using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Labs.Feedback.API.Context;
using Labs.Feedback.API.Services;
using Labs.Feedback.API.Repositorios;
using Labs.Feedback.API.Notificacoes;
using Labs.Feedback.API.Filas;

namespace Labs.Feedback.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAutoMapper();
            services.AddScoped<INotificador, Notificador>();

            services.AddScoped<IMensagemService, MensagemService>();
            services.AddScoped<IRepositorioMensagem, RepositorioMensagem>();
            services.AddScoped<IGerenciadorFila, GerenciadorFila>();

            services.AddDbContext<AppDbContext>(ServiceLifetime.Singleton);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
