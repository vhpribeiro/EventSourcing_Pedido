﻿// <auto-generated />
using System;
using EventSourcing_Pedido.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EventSourcing_Pedido.Infra.Migrations
{
    [DbContext(typeof(PedidoContext))]
    [Migration("20201012120859_Criando_Situacao_Do_Pedido")]
    partial class Criando_Situacao_Do_Pedido
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EventSourcingPedidoPagamento.Contratos.Eventos.Evento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdDoPedido")
                        .HasColumnType("int");

                    b.Property<string>("NomeDoUsuario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroDoCartao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Produto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Eventos");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Evento");
                });

            modelBuilder.Entity("EventSourcing_Pedido.Dominio.CartoesDeCredito.CartaoDeCredito", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CVV")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Expiracao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Numero")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CartoesDeCreditos");
                });

            modelBuilder.Entity("EventSourcing_Pedido.Dominio.Pedidos.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CartaoDeCreditoId")
                        .HasColumnType("int");

                    b.Property<string>("Produto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<int>("Situacao")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CartaoDeCreditoId");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("EventSourcingPedidoPagamento.Contratos.Eventos.AlterouCartaoDeCreditoDoPedidoEvento", b =>
                {
                    b.HasBaseType("EventSourcingPedidoPagamento.Contratos.Eventos.Evento");

                    b.HasDiscriminator().HasValue("AlterouCartaoDeCreditoDoPedidoEvento");
                });

            modelBuilder.Entity("EventSourcingPedidoPagamento.Contratos.Eventos.PedidoCriadoEvento", b =>
                {
                    b.HasBaseType("EventSourcingPedidoPagamento.Contratos.Eventos.Evento");

                    b.HasDiscriminator().HasValue("PedidoCriadoEvento");
                });

            modelBuilder.Entity("EventSourcing_Pedido.Dominio.Pedidos.Pedido", b =>
                {
                    b.HasOne("EventSourcing_Pedido.Dominio.CartoesDeCredito.CartaoDeCredito", "CartaoDeCredito")
                        .WithMany()
                        .HasForeignKey("CartaoDeCreditoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
