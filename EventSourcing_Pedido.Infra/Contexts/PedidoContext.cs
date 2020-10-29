using System.Collections.Generic;
using EventSourcing_Pedido.Dominio.CartoesDeCredito;
using EventSourcing_Pedido.Dominio.Pedidos;
using EventSourcing_Pedido.Infra.Contexts.Configuracoes;
using EventSourcingPedidoPagamento.Contratos.Eventos;
using Microsoft.EntityFrameworkCore;

namespace EventSourcing_Pedido.Infra.Contexts
{
    public class PedidoContext : DbContext
    {
        private readonly IEnumerable<IConfiguracaoDeEvento> _configuradoresDeEventos;
        public PedidoContext() { }
        
        public PedidoContext(DbContextOptions<PedidoContext> opcoes, IEnumerable<IConfiguracaoDeEvento> configuradoresDeEventos)
        : base(opcoes)
        {
            _configuradoresDeEventos = configuradoresDeEventos;
        }
        
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<CartaoDeCredito> CartoesDeCreditos { get; set; }
        public DbSet<Evento> Eventos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var configurador in _configuradoresDeEventos)
                modelBuilder = configurador.Configurar(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}