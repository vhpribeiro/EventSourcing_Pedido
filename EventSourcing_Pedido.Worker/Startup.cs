using EventSourcing_Pedido.Bootstrap.Configuracoes;
using EventSourcing_Pedido.Infra.Contexts;
using EventSourcing_Pedido.Worker.BackgroundServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EventSourcing_Pedido.Worker
{
    public class Startup
    {
        public IConfiguration _configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<PedidoContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("EventSourcing_Pedido.Infra")));
            ConfiguracaoDeInjecaoDeDependencia.Configurar(services, _configuration);
            services.AddHostedService<RabbitMqSubscriber>();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}