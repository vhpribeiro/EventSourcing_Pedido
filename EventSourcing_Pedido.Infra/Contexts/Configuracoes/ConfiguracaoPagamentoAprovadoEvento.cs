using EventSourcingPedidoPagamento.Contratos.Eventos;
using Microsoft.EntityFrameworkCore;

namespace EventSourcing_Pedido.Infra.Contexts.Configuracoes
{
    public class ConfiguracaoPagamentoAprovadoEvento
    {
        public static ModelBuilder Configuruar(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PagamentoAprovadoEvento>()
                .Property(e => e.Data)
                .HasColumnType("datetime2");
            modelBuilder.Entity<PagamentoAprovadoEvento>()
                .Property(e => e.IdDoPedido)
                .HasColumnType("int");
            modelBuilder.Entity<PagamentoAprovadoEvento>()
                .Property(e => e.NomeDoUsuario)
                .HasColumnType("nvarchar(max)");
            modelBuilder.Entity<PagamentoAprovadoEvento>()
                .Property(e => e.NumeroDoCartao)
                .HasColumnType("nvarchar(max)");
            modelBuilder.Entity<PagamentoAprovadoEvento>()
                .Property(e => e.Produto)
                .HasColumnType("nvarchar(max)");
            modelBuilder.Entity<PagamentoAprovadoEvento>()
                .Property(e => e.Valor)
                .HasColumnType("decimal(18,2)");
            return modelBuilder;
        }
    }
}