using EventSourcing_Pedido.Dominio.Pedidos;

namespace EventSourcing_Pedido.Aplicacao.InterfacesDeRepositorio
{
    public interface IPedidoRepositorio
    {
        void Salvar(Pedido pedido);
    }
}