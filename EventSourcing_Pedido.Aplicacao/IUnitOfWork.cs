using System.Threading.Tasks;

namespace EventSourcing_Pedido.Aplicacao
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}