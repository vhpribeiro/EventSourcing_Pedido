using System.Threading.Tasks;
using EventSourcing_Pedido.Aplicacao;

namespace EventSourcing_Pedido.Infra.Contexts
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PedidoContext _pedidoContext;

        public UnitOfWork(PedidoContext pedidoContext)
        {
            _pedidoContext = pedidoContext;
        }

        public async Task Commit()
        {
            await _pedidoContext.SaveChangesAsync();
        }
    }
}