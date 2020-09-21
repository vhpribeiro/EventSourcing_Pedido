using EventSourcing_Pedido.Dominio.Pedidos;

namespace EventSourcing_Pedido.Dominio.Eventos
{
    public class PedidoCriadoEvento : Evento
    {   
        public PedidoCriadoEvento() {}
        
        public PedidoCriadoEvento(Pedido pedido) : base(pedido.Id, pedido)
        {
        }
    }
}