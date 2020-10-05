using EventSourcing_Pedido.Dominio.CartoesDeCredito;
using EventSourcing_Pedido.Dominio.Pedidos;

namespace EventSourcing_Pedido.Dominio.Eventos
{
    public class AlterouCartaoDeCreditoDoPedidoEvento : Evento
    {
        public AlterouCartaoDeCreditoDoPedidoEvento(Pedido pedido, CartaoDeCredito novoCartaoDeCredito) 
            : base(pedido.Id, novoCartaoDeCredito.Nome, novoCartaoDeCredito.Numero, pedido.Produto, pedido.Valor)
        {
        }
    }
}