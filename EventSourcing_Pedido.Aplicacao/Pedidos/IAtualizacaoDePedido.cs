using System.Threading.Tasks;
using EventSourcing_Pedido.Aplicacao.Dtos;

namespace EventSourcing_Pedido.Aplicacao.Pedidos
{
    public interface IAtualizacaoDePedido
    {
        Task AtualizarCartaoDeCredito(int idDoPedido, CartaoDeCreditoDto cartaoDeCreditoDto);
        Task AprovarPagamento(int idDoPedido);
        Task NegarPagamento(int idDoPedido);
    }
}