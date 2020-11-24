using EventSourcing_Pedido.Aplicacao.InterfacesDeRepositorio;
using EventSourcing_Pedido.Dominio._Helper;
using EventSourcing_Pedido.Infra.Contexts;

namespace EventSourcing_Pedido.Infra.Repositorios
{
    public class RepositorioBase<TEntidade> : IRepositorio<TEntidade> where TEntidade : Entidade
    {
        protected PedidoContext _pedidoContext;

        public RepositorioBase(PedidoContext pedidoContext)
        {
            _pedidoContext = pedidoContext;
        }
        
        public void Adicionar(TEntidade entidade)
        {
            _pedidoContext.Set<TEntidade>().Add(entidade);
        }
    }
}