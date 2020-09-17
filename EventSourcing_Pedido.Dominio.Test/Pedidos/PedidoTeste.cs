using Bogus;
using EventSourcing_Pedido.Dominio._Helper;
using EventSourcing_Pedido.Dominio.CartoesDeCredito;
using EventSourcing_Pedido.Dominio.Pedidos;
using EventSourcing_Pedido.Test.Helpers._Builders.Dominio;
using ExpectedObjects;
using Xunit;

namespace EventSourcing_Pedido.Dominio.Test.Pedidos
{
    public class PedidoTeste
    {
        private readonly string _produto;
        private readonly int _quantidade;
        private readonly decimal _valor;
        private readonly CartaoDeCredito _cartaoDeCredito;
        private readonly Faker _faker;

        public PedidoTeste()
        {
            _faker = new Faker();
            _produto = _faker.Random.Word();
            _quantidade = _faker.Random.Int(0);
            _valor = _faker.Random.Decimal();
            _cartaoDeCredito = CartaoDeCreditoBuilder.Novo().Criar();
        }
        
        [Fact]
        public void Deve_criar_um_pedido()
        {
            var pedidoEsperado = new
            {
                Produto = _produto,
                Quantidade = _quantidade,
                Valor = _valor,
                CartaoDeCredito = _cartaoDeCredito
            };
            
            var pedidoObtido = new Pedido(_produto, _quantidade, _valor, _cartaoDeCredito);
            
            pedidoEsperado.ToExpectedObject().ShouldMatch(pedidoObtido);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void Nao_deve_criar_pedido_com_produto_invalido(string produtoInvalido)
        {
            const string mensagemDeErroEsperada = "É necessário informar um produto válido";
            
            void Acao() => new Pedido(produtoInvalido, _quantidade, _valor, _cartaoDeCredito);
            
            Assert.Throws<ExcecaoDeDominio<Pedido>>(Acao).PossuiErroComAMensagemIgualA(mensagemDeErroEsperada);
        }
        
        [Fact]
        public void Nao_deve_criar_pedido_com_quantidade_invalida()
        {
            const string mensagemDeErroEsperada = "É necessário informar uma quantidade válida";
            var quantidadeInvalida = _faker.Random.Int(-100, -1);
            
            void Acao() => new Pedido(_produto, quantidadeInvalida, _valor, _cartaoDeCredito);
            
            Assert.Throws<ExcecaoDeDominio<Pedido>>(Acao).PossuiErroComAMensagemIgualA(mensagemDeErroEsperada);
        }
        
        [Fact]
        public void Nao_deve_criar_pedido_com_valor_invalido()
        {
            const string mensagemDeErroEsperada = "É necessário informar um valor válido";
            var valorInvalido = _faker.Random.Decimal(-100, -1);
            
            void Acao() => new Pedido(_produto, _quantidade, valorInvalido, _cartaoDeCredito);
            
            Assert.Throws<ExcecaoDeDominio<Pedido>>(Acao).PossuiErroComAMensagemIgualA(mensagemDeErroEsperada);
        }
        
        [Fact]
        public void Nao_deve_criar_pedido_sem_cartao_de_credito()
        {
            const string mensagemDeErroEsperada = "É necessário informar um valor válido";
            CartaoDeCredito cartaoDeCreditoInvalido = null;
            
            void Acao() => new Pedido(_produto, _quantidade, _valor, cartaoDeCreditoInvalido);
            
            Assert.Throws<ExcecaoDeDominio<Pedido>>(Acao).PossuiErroComAMensagemIgualA(mensagemDeErroEsperada);
        }
    }
}