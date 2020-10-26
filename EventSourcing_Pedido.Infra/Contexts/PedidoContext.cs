using EventSourcing_Pedido.Dominio.CartoesDeCredito;
using EventSourcing_Pedido.Dominio.Pedidos;
using EventSourcing_Pedido.Infra.Contexts.Configuracoes;
using EventSourcingPedidoPagamento.Contratos.Eventos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EventSourcing_Pedido.Infra.Contexts
{
    public class PedidoContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public PedidoContext() { }
        
        public PedidoContext(DbContextOptions<PedidoContext> opcoes, IConfiguration configuration)
        : base(opcoes)
        {
            _configuration = configuration;
        }
        
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<CartaoDeCredito> CartoesDeCreditos { get; set; }
        public DbSet<Evento> Eventos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder = ConfiguracaoPedidoCriadoEvento.Configuruar(modelBuilder);
            modelBuilder = ConfiguracaoAlteracaoCartaoDeCreditoDoPedidoEvento.Configuruar(modelBuilder);
            modelBuilder = ConfiguracaoPagamentoAprovadoEvento.Configuruar(modelBuilder);
            modelBuilder = ConfiguracaoPagamentoRecusadoEvento.Configuruar(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}