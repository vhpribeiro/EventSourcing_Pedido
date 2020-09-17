using Bogus;
using EventSourcing_Pedido.Aplicacao.Dtos;
using EventSourcing_Pedido.Aplicacao.Mapeadores;
using EventSourcing_Pedido.Dominio.CartoesDeCredito;
using EventSourcing_Pedido.Test.Helpers._Builders.Dominio;
using ExpectedObjects;
using Xunit;

namespace EventSourcing_Pedido.Aplicacao.Test.Mapeadores
{
    public class MapeadorDePedidoTeste
    {
        private Faker _faker;

        public MapeadorDePedidoTeste()
        {
            _faker = new Faker();
        }
        
        [Fact]
        public void Deve_mapear_nulo_quando_pedido_dto_for_nulo()
        {
            PedidoDto pedidoDtoInvalido = null;
            
            var pedido = MapeadorDePedido.Mapear(pedidoDtoInvalido);
            
            Assert.Null(pedido);
        }

        [Fact]
        public void Deve_mapear_pedido()
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
            
            var pedidoObtido = MapeadorDePedido.Mapear(pedidoDto);
            
            pedidoEsperado.ToExpectedObject(ctx => ctx.Ignore(p => p.Id)).ShouldMatch(pedidoObtido);
        }
    }
}