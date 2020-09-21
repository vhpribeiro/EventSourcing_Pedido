using System.Threading.Tasks;
using EventSourcing_Pedido.Aplicacao.Dtos;

namespace EventSourcing_Pedido.Aplicacao.Pedidos
{
    public interface ICriacaoDePedido
    {
        Task Criar(PedidoDto pedidoDto);
    }
}