using System.Threading.Tasks;
using EventSourcing_Pedido.Dominio.Pedidos;

namespace EventSourcing_Pedido.Aplicacao.InterfacesDeRepositorio
{
    public interface IPedidoRepositorio
    {
        void Adicionar(Pedido pedido);
        Pedido ObterPedidoPeloId(int idDoPedido);
        Task AtualizarPedido(Pedido pedido);
    }
}