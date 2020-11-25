using System.Linq;
using System.Threading.Tasks;
using EventSourcing_Pedido.Aplicacao.InterfacesDeRepositorio;
using EventSourcing_Pedido.Dominio.Pedidos;
using EventSourcing_Pedido.Infra.Contexts;

namespace EventSourcing_Pedido.Infra.Repositorios
{
    public class PedidoRepositorio : RepositorioBase<Pedido>, IPedidoRepositorio
    {

        public PedidoRepositorio(PedidoContext pedidoContext) : base(pedidoContext)
        {
            _pedidoContext = pedidoContext;
        }

        public Pedido ObterPedidoPeloId(int idDoPedido)
        {
            return _pedidoContext.Pedidos.First(p => p.Id == idDoPedido);
        }
        
        public void Salvar(Pedido pedido)
        {
            _pedidoContext.Pedidos.Add(pedido);
            _pedidoContext.SaveChanges();
        }

        public async Task AtualizarPedido(Pedido pedido)
        {
            _pedidoContext.Pedidos.Update(pedido);
        }
    }
}