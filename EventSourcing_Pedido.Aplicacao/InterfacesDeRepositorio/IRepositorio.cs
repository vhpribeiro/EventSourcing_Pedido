namespace EventSourcing_Pedido.Aplicacao.InterfacesDeRepositorio
{
    public interface IRepositorio<TEntidade>
    {
        void Adicionar(TEntidade entity);
    }
}