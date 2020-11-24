using System.Threading.Tasks;
using EventSourcing_Pedido.Aplicacao.InterfacesDeRepositorio;
using EventSourcing_Pedido.Infra.Contexts;
using EventSourcingPedidoPagamento.Contratos.Eventos;

namespace EventSourcing_Pedido.Infra.Repositorios
{
    public class EventoRepositorio : IEventoRepositorio
    {
        private readonly PedidoContext _pedidoContext;

        public EventoRepositorio(PedidoContext pedidoContext)
        {
            _pedidoContext = pedidoContext;
        }

        public async Task Salvar(Evento evento)
        {
            _pedidoContext.Eventos.Add(evento);
            await _pedidoContext.SaveChangesAsync();
        }
    }
}