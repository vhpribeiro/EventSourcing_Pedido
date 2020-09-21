using System.Threading.Tasks;
using EventSourcing_Pedido.Aplicacao.Dtos;
using EventSourcing_Pedido.Aplicacao.InterfacesDeRepositorio;
using EventSourcing_Pedido.Aplicacao.Mapeadores;
using EventSourcing_Pedido.Dominio.Eventos;

namespace EventSourcing_Pedido.Aplicacao.Pedidos
{
    public class CriacaoDePedido : ICriacaoDePedido
    {
        private readonly IPedidoRepositorio _pedidoRepositorio;
        private readonly IEventoRepositorio _eventoRepositorio;

        public CriacaoDePedido(IPedidoRepositorio pedidoRepositorio, IEventoRepositorio eventoRepositorio)
        {
            _pedidoRepositorio = pedidoRepositorio;
            _eventoRepositorio = eventoRepositorio;
        }

        public async Task Criar(PedidoDto pedidoDto)
        {
            var pedido = MapeadorDePedido.Mapear(pedidoDto);
            await _pedidoRepositorio.Salvar(pedido);
            
            var eventoDePedidoCriado = new PedidoCriadoEvento(pedido);
            await _eventoRepositorio.Salvar(eventoDePedidoCriado);
        }
    }
}