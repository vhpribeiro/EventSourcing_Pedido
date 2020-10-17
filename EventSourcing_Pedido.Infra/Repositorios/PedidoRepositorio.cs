using System.Linq;
using System.Threading.Tasks;
using EventSourcing_Pedido.Aplicacao.InterfacesDeRepositorio;
using EventSourcing_Pedido.Dominio.Pedidos;
using EventSourcing_Pedido.Infra.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace EventSourcing_Pedido.Infra.Repositorios
{
    public class PedidoRepositorio : IPedidoRepositorio
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public PedidoRepositorio(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        
        public async Task Salvar(Pedido pedido)
        {
            using (var escopo = _scopeFactory.CreateScope())
            {
                var servicosEscopo = escopo.ServiceProvider;
                var contexto = servicosEscopo.GetRequiredService<PedidoContext>();
                await contexto.Pedidos.AddAsync(pedido);
                await contexto.SaveChangesAsync();
            }
        }

        public Pedido ObterPedidoPeloId(int idDoPedido)
        {
            using (var escopo = _scopeFactory.CreateScope())
            {
                var servicosEscopo = escopo.ServiceProvider;
                var contexto = servicosEscopo.GetRequiredService<PedidoContext>();
                return contexto.Pedidos.First(p => p.Id == idDoPedido);
            }
        }

        public void AtualizarPedido(Pedido pedido)
        {
            using (var escopo = _scopeFactory.CreateScope())
            {
                var servicosEscopo = escopo.ServiceProvider;
                var contexto = servicosEscopo.GetRequiredService<PedidoContext>();
                contexto.Pedidos.Update(pedido);
            }
        }
    }
}