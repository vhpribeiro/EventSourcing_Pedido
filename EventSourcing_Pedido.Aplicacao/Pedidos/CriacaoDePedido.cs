using System.Threading.Tasks;
using EasyNetQ;
using EventSourcing_Pedido.Aplicacao.Dtos;
using EventSourcing_Pedido.Aplicacao.InterfacesDeRepositorio;
using EventSourcing_Pedido.Aplicacao.Mapeadores;
using EventSourcing_Pedido.Dominio.Eventos;
using EventSourcing_Pedido.Dominio.Pedidos;

namespace EventSourcing_Pedido.Aplicacao.Pedidos
{
    public class CriacaoDePedido : ICriacaoDePedido
    {
        private readonly IPedidoRepositorio _pedidoRepositorio;
        private readonly IEventoRepositorio _eventoRepositorio;
        private readonly IBus _mensageria;

        public CriacaoDePedido(IPedidoRepositorio pedidoRepositorio, IEventoRepositorio eventoRepositorio, IBus mensageria)
        {
            _pedidoRepositorio = pedidoRepositorio;
            _eventoRepositorio = eventoRepositorio;
            _mensageria = mensageria;
        }

        public async Task Criar(PedidoDto pedidoDto)
        {
            var pedido = MapeadorDePedido.Mapear(pedidoDto);
            await _pedidoRepositorio.Salvar(pedido);

            await NotificarServicoDePagamento(pedido);
        }

        private async Task NotificarServicoDePagamento(Pedido pedido)
        {
            var eventoDePedidoCriado = new PedidoCriadoEvento(pedido);
            await _eventoRepositorio.Salvar(eventoDePedidoCriado);
            await _mensageria.PublishAsync(eventoDePedidoCriado);
        }
    }
}