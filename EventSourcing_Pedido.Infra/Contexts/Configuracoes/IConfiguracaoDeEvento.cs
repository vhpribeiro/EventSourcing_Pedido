using EventSourcingPedidoPagamento.Contratos.Eventos;
using Microsoft.EntityFrameworkCore;

namespace EventSourcing_Pedido.Infra.Contexts.Configuracoes
{
    public interface IConfiguracaoDeEvento
    {
        ModelBuilder Configurar(ModelBuilder modelBuilder);
    }
}