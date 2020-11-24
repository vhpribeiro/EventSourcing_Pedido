using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using EventSourcing_Pedido.Aplicacao.Pedidos;
using EventSourcingPedidoPagamento.Contratos.Eventos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EventSourcing_Pedido.Worker.BackgroundServices
{
    public class RabbitMqSubscriber : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public RabbitMqSubscriber(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var escopo = _scopeFactory.CreateScope())
            {
                var mensageria = escopo.ServiceProvider.GetService<IBus>();
                var atualizacaoDePedido = escopo.ServiceProvider.GetService<IAtualizacaoDePedido>();
                while (!stoppingToken.IsCancellationRequested)
                {
                    mensageria.Subscribe<PagamentoAprovadoEvento>("pagamentoAprovado", pagamentoAprovadoEvento =>
                    {
                        atualizacaoDePedido.AprovarPagamento(pagamentoAprovadoEvento);
                    });
                    mensageria.Subscribe<PagamentoRecusadoEvento>("pagamentoRecusado", pagamentoRecusadoEvento =>
                    {
                        atualizacaoDePedido.NegarPagamento(pagamentoRecusadoEvento);
                    });
                }
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            using (var escopo = _scopeFactory.CreateScope())
            {
                var mensageria = escopo.ServiceProvider.GetService<IBus>();
                mensageria.Dispose();
                return base.StopAsync(cancellationToken);
            }
        }
    }
}