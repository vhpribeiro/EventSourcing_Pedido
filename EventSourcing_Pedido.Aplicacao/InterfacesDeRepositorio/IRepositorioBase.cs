namespace EventSourcing_Pedido.Aplicacao.InterfacesDeRepositorio
{
    public interface IRepositorioBase<TEntidade>
    {
        void Adicionar(TEntidade entity);
    }
}