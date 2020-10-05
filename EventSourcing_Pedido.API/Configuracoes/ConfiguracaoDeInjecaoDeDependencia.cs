using EasyNetQ;
using EventSourcing_Pedido.Aplicacao.InterfacesDeRepositorio;
using EventSourcing_Pedido.Aplicacao.Pedidos;
using EventSourcing_Pedido.Infra.Contexts;
using EventSourcing_Pedido.Infra.Repositorios;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventSourcing_Pedido.API.Configuracoes
{
    public static class ConfiguracaoDeInjecaoDeDependencia
    {
        public static void Configurar(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(provider => configuration);
            services.AddScoped<ICriacaoDePedido, CriacaoDePedido>();
            services.AddScoped<IAtualizacaoDePedido, AtualizacaoDePedido>();
            services.AddScoped<IEventoRepositorio, EventoRepositorio>();
            services.AddScoped<IPedidoRepositorio, PedidoRepositorio>();
            services.AddScoped<PedidoContext, PedidoContext>();
            services.AddTransient<IBus>(x => RabbitHutch.CreateBus(configuration.GetValue<string>("RabbitConnection")));
        }    
    }
}