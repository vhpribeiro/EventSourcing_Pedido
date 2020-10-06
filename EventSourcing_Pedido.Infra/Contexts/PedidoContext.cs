using EventSourcing_Pedido.Dominio.CartoesDeCredito;
using EventSourcing_Pedido.Dominio.Eventos;
using EventSourcing_Pedido.Dominio.Pedidos;
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
            ConfigurarPedidoCriadoEvento(modelBuilder);
            ConfigurarAlterouCartaoDeCreditoDoPedidoEvento(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void ConfigurarPedidoCriadoEvento(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PedidoCriadoEvento>()
                .Property(e => e.Data)
                .HasColumnType("datetime2");
            modelBuilder.Entity<PedidoCriadoEvento>()
                .Property(e => e.IdDoPedido)
                .HasColumnType("int");
            modelBuilder.Entity<PedidoCriadoEvento>()
                .Property(e => e.NomeDoUsuario)
                .HasColumnType("nvarchar(max)");
            modelBuilder.Entity<PedidoCriadoEvento>()
                .Property(e => e.NumeroDoCartao)
                .HasColumnType("nvarchar(max)");
            modelBuilder.Entity<PedidoCriadoEvento>()
                .Property(e => e.Produto)
                .HasColumnType("nvarchar(max)");
            modelBuilder.Entity<PedidoCriadoEvento>()
                .Property(e => e.Valor)
                .HasColumnType("decimal(18,2)");
        }
        
        private static void ConfigurarAlterouCartaoDeCreditoDoPedidoEvento(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlterouCartaoDeCreditoDoPedidoEvento>()
                .Property(e => e.Data)
                .HasColumnType("datetime2");
            modelBuilder.Entity<AlterouCartaoDeCreditoDoPedidoEvento>()
                .Property(e => e.IdDoPedido)
                .HasColumnType("int");
            modelBuilder.Entity<AlterouCartaoDeCreditoDoPedidoEvento>()
                .Property(e => e.NomeDoUsuario)
                .HasColumnType("nvarchar(max)");
            modelBuilder.Entity<AlterouCartaoDeCreditoDoPedidoEvento>()
                .Property(e => e.NumeroDoCartao)
                .HasColumnType("nvarchar(max)");
            modelBuilder.Entity<AlterouCartaoDeCreditoDoPedidoEvento>()
                .Property(e => e.Produto)
                .HasColumnType("nvarchar(max)");
            modelBuilder.Entity<AlterouCartaoDeCreditoDoPedidoEvento>()
                .Property(e => e.Valor)
                .HasColumnType("decimal(18,2)");
        }
    }
}