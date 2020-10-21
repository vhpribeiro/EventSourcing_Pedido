using System.Threading.Tasks;
using EventSourcing_Pedido.Aplicacao.Dtos;
using EventSourcingPedidoPagamento.Contratos.Eventos;

namespace EventSourcing_Pedido.Aplicacao.Pedidos
{
    public interface IAtualizacaoDePedido
    {
        Task AtualizarCartaoDeCredito(int idDoPedido, CartaoDeCreditoDto cartaoDeCreditoDto);
        Task AprovarPagamento(PagamentoAprovadoEvento pagamentoAprovadoEvento);
        Task NegarPagamento(PagamentoRecusadoEvento pagamentoRecusadoEvento);
    }
}