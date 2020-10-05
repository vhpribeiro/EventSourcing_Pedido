using System.Linq;
using System.Threading.Tasks;
using EventSourcing_Pedido.Aplicacao.InterfacesDeRepositorio;
using EventSourcing_Pedido.Dominio.Pedidos;
using EventSourcing_Pedido.Infra.Contexts;

namespace EventSourcing_Pedido.Infra.Repositorios
{
    public class PedidoRepositorio : IPedidoRepositorio
    {
        private readonly PedidoContext _context;

        public PedidoRepositorio(PedidoContext context)
        {
            _context = context;
        }
        
        public async Task Salvar(Pedido pedido)
        {
            await _context.Pedidos.AddAsync(pedido);
            await _context.SaveChangesAsync();
        }

        public Pedido ObterPedidoPeloId(int idDoPedido) 
            => _context.Pedidos.First(p => p.Id == idDoPedido);

        public void AtualizarPedido(Pedido pedido)
        {
            _context.Pedidos.Update(pedido);
        }
    }
}