using System.Threading.Tasks;
using EventSourcingPedidoPagamento.Contratos.Eventos;

namespace EventSourcing_Pedido.Aplicacao.InterfacesDeRepositorio
{
    public interface IEventoRepositorio
    {
        Task Salvar(Evento evento);
    }
}