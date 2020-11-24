using EasyNetQ;
using EventSourcing_Pedido.Aplicacao;
using EventSourcing_Pedido.Aplicacao.InterfacesDeRepositorio;
using EventSourcing_Pedido.Aplicacao.Pedidos;
using EventSourcing_Pedido.Infra.Contexts;
using EventSourcing_Pedido.Infra.Contexts.Configuracoes;
using EventSourcing_Pedido.Infra.Repositorios;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventSourcing_Pedido.API.Configuracoes
{
    public static class ConfiguracaoDeInjecaoDeDependencia
    {
        public static void Configurar(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICriacaoDePedido, CriacaoDePedido>();
            services.AddScoped<IAtualizacaoDePedido, AtualizacaoDePedido>();
            services.AddSingleton<IEventoRepositorio, EventoRepositorio>();
            services.AddScoped<IPedidoRepositorio, PedidoRepositorio>();
            services.AddSingleton<IConfiguracaoDeEvento, ConfiguracaoPedidoCriadoEvento>();
            services.AddSingleton<IConfiguracaoDeEvento, ConfiguracaoAlteracaoCartaoDeCreditoDoPedidoEvento>();
            services.AddSingleton<IConfiguracaoDeEvento, ConfiguracaoPagamentoAprovadoEvento>();
            services.AddSingleton<IConfiguracaoDeEvento, ConfiguracaoPagamentoRecusadoEvento>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IBus>(x => RabbitHutch.CreateBus(configuration.GetValue<string>("RabbitConnection")));
        }    
    }
}