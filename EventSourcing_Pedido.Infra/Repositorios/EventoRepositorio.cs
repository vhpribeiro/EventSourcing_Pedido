using System.Threading.Tasks;
using EventSourcing_Pedido.Aplicacao.InterfacesDeRepositorio;
using EventSourcing_Pedido.Dominio.Eventos;
using EventSourcing_Pedido.Infra.Contexts;

namespace EventSourcing_Pedido.Infra.Repositorios
{
    public class EventoRepositorio : IEventoRepositorio
    {
        private readonly PedidoContext _context;

        public EventoRepositorio(PedidoContext context)
        {
            _context = context;
        }

        public async Task Salvar(Evento evento)
        {
            await _context.Eventos.AddAsync(evento);
            await _context.SaveChangesAsync();
        }
    }
}