using EventSourcing_Pedido.API.Configuracoes;
using EventSourcing_Pedido.Infra.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EventSourcing_Pedido.API
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
            ConfiguracaoDoSwagger.Configurar(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseSwagger();  
            app.UseSwaggerUI(options =>options.SwaggerEndpoint("/swagger/v1/swagger.json", "Pedido - Pagamento v1"));  
        }
    }
}