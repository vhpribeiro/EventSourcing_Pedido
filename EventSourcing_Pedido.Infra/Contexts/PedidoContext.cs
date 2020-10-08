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
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //TODO jogar essa connection string no appsettings
            optionsBuilder.UseSqlServer("Password=vhpr1706;Persist Security Info=True;User ID=sa;Initial Catalog=EventSourcingProjeto;Data Source=DESKTOP-NEOJFCR\\MSSQLSERVER2019",
                b => b.MigrationsAssembly("EventSourcing_Pedido.Infra"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder = ConfiguracaoPedidoCriadoEvento.Configuruar(modelBuilder);
            modelBuilder = ConfiguracaoAlteracaoCartaoDeCreditoDoPedidoEvento.Configuruar(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}