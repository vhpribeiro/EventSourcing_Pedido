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

        public void Criar(PedidoDto pedidoDto)
        {
            var pedido = MapeadorDePedido.Mapear(pedidoDto);
            
            _pedidoRepositorio.Salvar(pedido);
        }
    }
}