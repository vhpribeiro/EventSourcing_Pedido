using System.Threading.Tasks;
using EventSourcing_Pedido.Dominio.Pedidos;

namespace EventSourcing_Pedido.Aplicacao.InterfacesDeRepositorio
{
    public interface IPedidoRepositorio
    {
        Task Salvar(Pedido pedido);
    }
}