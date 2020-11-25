using EventSourcing_Pedido.Dominio._Helper;

namespace EventSourcing_Pedido.Aplicacao.InterfacesDeRepositorio
{
    public interface IRepositorioBase<TEntidade> where TEntidade : Entidade
    {
        void Adicionar(TEntidade entity);
    }
}