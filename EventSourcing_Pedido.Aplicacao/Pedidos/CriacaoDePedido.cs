using System.Threading.Tasks;
using EventSourcing_Pedido.Aplicacao.Dtos;
using EventSourcing_Pedido.Aplicacao.InterfacesDeRepositorio;
using EventSourcing_Pedido.Aplicacao.Mapeadores;

namespace EventSourcing_Pedido.Aplicacao.Pedidos
{
    public class CriacaoDePedido
    {
        private readonly IPedidoRepositorio _pedidoRepositorio;

        public CriacaoDePedido(IPedidoRepositorio pedidoRepositorio)
        {
            _pedidoRepositorio = pedidoRepositorio;
        }

        public async Task Criar(PedidoDto pedidoDto)
        {
            var pedido = MapeadorDePedido.Mapear(pedidoDto);
            
            await _pedidoRepositorio.Salvar(pedido);
        }
    }
}