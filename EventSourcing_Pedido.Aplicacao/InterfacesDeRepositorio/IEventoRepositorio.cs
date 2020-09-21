using System.Threading.Tasks;
using EventSourcing_Pedido.Dominio.Eventos;

namespace EventSourcing_Pedido.Aplicacao.InterfacesDeRepositorio
{
    public interface IEventoRepositorio
    {
        Task Salvar(Evento evento);
    }
}