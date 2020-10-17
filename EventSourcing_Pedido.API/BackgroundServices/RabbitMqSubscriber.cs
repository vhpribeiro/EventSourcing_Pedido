using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using EventSourcing_Pedido.Aplicacao.Pedidos;
using EventSourcingPedidoPagamento.Contratos.Eventos;
using Microsoft.Extensions.Hosting;

namespace EventSourcing_Pedido.API.BackgroundServices
{
    public class RabbitMqSubscriber : BackgroundService
    {
        private readonly IBus _mensageria;
        private readonly IAtualizacaoDePedido _atualizacaoDePedido;

        public RabbitMqSubscriber(IBus mensageria, IAtualizacaoDePedido atualizacaoDePedido)
        {
            _mensageria = mensageria;
            _atualizacaoDePedido = atualizacaoDePedido;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _mensageria.Subscribe<PagamentoAprovadoEvento>("pagamentoAprovado", pagamentoAprovadoEvento =>
                {
                    _atualizacaoDePedido.AprovarPagamento(pagamentoAprovadoEvento.IdDoPedido);
                });
                _mensageria.Subscribe<PagamentoRecusadoEvento>("pagamentoRecusado", pagamentoRecusadoEvento =>
                {
                    _atualizacaoDePedido.NegarPagamento(pagamentoRecusadoEvento.IdDoPedido);
                });
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _mensageria.Dispose();
            return base.StopAsync(cancellationToken);
        }
    }
}