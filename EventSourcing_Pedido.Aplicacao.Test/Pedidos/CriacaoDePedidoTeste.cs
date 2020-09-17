﻿using System.Threading.Tasks;
using Bogus;
using EventSourcing_Pedido.Aplicacao.Dtos;
using EventSourcing_Pedido.Aplicacao.InterfacesDeRepositorio;
using EventSourcing_Pedido.Aplicacao.Mapeadores;
using EventSourcing_Pedido.Aplicacao.Pedidos;
using EventSourcing_Pedido.Dominio.Pedidos;
using EventSourcing_Pedido.Test.Helpers._Builders.Dominio;
using ExpectedObjects;
using Moq;
using Xunit;

namespace EventSourcing_Pedido.Aplicacao.Test.Pedidos
{
    public class CriacaoDePedidoTeste
    {
        private readonly Faker _faker;
        private readonly Mock<IPedidoRepositorio> _pedidoRepositorio;
        private readonly CriacaoDePedido _criacaoDePedido;

        public CriacaoDePedidoTeste()
        {
            _faker = new Faker();
            _pedidoRepositorio = new Mock<IPedidoRepositorio>();
            _criacaoDePedido = new CriacaoDePedido(_pedidoRepositorio.Object);
        }
        
        
        [Fact]
        public async Task Deve_criar_um_pedido()
        {
            var produto = _faker.Random.Word();
            var quantidade = _faker.Random.Int(0);
            var valor = _faker.Random.Decimal();
            var cartaoDeCreditoDto = new CartaoDeCreditoDto
            {
                Numero = _faker.Random.Int(0).ToString(),
                Nome = _faker.Person.FirstName,
                CVV = _faker.Random.Int(100, 999).ToString(),
                Expiracao = "03/27"
            };
            var cartaoDeCredito = MapeadorDeCartaoDeCredito.Mapear(cartaoDeCreditoDto);
            var pedidoDto = new PedidoDto
            {
                Produto = produto,
                Quantidade = quantidade,
                Valor = valor,
                CartaoDeCreditoDto = cartaoDeCreditoDto
            };
            var pedidoEsperado = PedidoBuilder.Novo().ComProduto(produto).ComQuantidade(quantidade).ComValor(valor)
                .ComCartaoDeCredito(cartaoDeCredito).Criar();
            _pedidoRepositorio.Setup(pr => pr.Salvar(It.IsAny<Pedido>()));
            
            await _criacaoDePedido.Criar(pedidoDto);
            
            _pedidoRepositorio.Verify(pr => pr.Salvar(It.Is<Pedido>(pedido 
                => pedido.ToExpectedObject(ctx => ctx.Ignore(p => p.Id)).Matches(pedidoEsperado))));
        }
    }
}