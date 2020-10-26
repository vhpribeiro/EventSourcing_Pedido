using EventSourcingPedidoPagamento.Contratos.Eventos;
using Microsoft.EntityFrameworkCore;

namespace EventSourcing_Pedido.Infra.Contexts.Configuracoes
{
    public class ConfiguracaoPagamentoRecusadoEvento : IConfiguracaoDeEvento
    {
        public ModelBuilder Configurar(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PagamentoRecusadoEvento>()
                .Property(e => e.Data)
                .HasColumnType("datetime2");
            modelBuilder.Entity<PagamentoRecusadoEvento>()
                .Property(e => e.IdDoPedido)
                .HasColumnType("int");
            modelBuilder.Entity<PagamentoRecusadoEvento>()
                .Property(e => e.NomeDoUsuario)
                .HasColumnType("nvarchar(max)");
            modelBuilder.Entity<PagamentoRecusadoEvento>()
                .Property(e => e.NumeroDoCartao)
                .HasColumnType("nvarchar(max)");
            modelBuilder.Entity<PagamentoRecusadoEvento>()
                .Property(e => e.Produto)
                .HasColumnType("nvarchar(max)");
            modelBuilder.Entity<PagamentoRecusadoEvento>()
                .Property(e => e.Valor)
                .HasColumnType("decimal(18,2)");
            return modelBuilder;
        }
    }
}