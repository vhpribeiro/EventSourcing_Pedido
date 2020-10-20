using System.Threading.Tasks;
using EventSourcing_Pedido.Aplicacao.InterfacesDeRepositorio;
using EventSourcing_Pedido.Infra.Contexts;
using EventSourcingPedidoPagamento.Contratos.Eventos;
using Microsoft.Extensions.DependencyInjection;

namespace EventSourcing_Pedido.Infra.Repositorios
{
    public class EventoRepositorio : IEventoRepositorio
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public EventoRepositorio(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task Salvar(Evento evento)
        {
            using (var escopo = _scopeFactory.CreateScope())
            {
                var servicosEscopo = escopo.ServiceProvider;
                var contexto = servicosEscopo.GetRequiredService<PedidoContext>();
                await contexto.Eventos.AddAsync(evento);
                await contexto.SaveChangesAsync();
            }
        }
    }
}