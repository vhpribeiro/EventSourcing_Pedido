using Bogus;
using EventSourcing_Pedido.Dominio._Helper;
using EventSourcing_Pedido.Dominio.CartoesDeCredito;
using ExpectedObjects;
using Xunit;

namespace EventSourcing_Pedido.Dominio.Test.CartoesDeCredito
{
    public class CartaoDeCreditoTeste
    {
        private readonly string _numero;
        private readonly string _nome;
        private readonly string _cvv;
        private readonly string _expiracao;

        public CartaoDeCreditoTeste()
        {
            var faker = new Faker();
            _numero = faker.Random.Int(0).ToString();
            _nome = faker.Person.FirstName;
            _cvv = faker.Random.Int(100, 999).ToString();
            _expiracao = "03/27";
        }
        
        [Fact]
        public void Deve_criar_um_cartao_de_credito()
        {
            var cartaoDeCreditoEsperado = new
            {
                Numero = _numero,
                Nome = _nome,
                CVV = _cvv,
                Expiracao = _expiracao
            };
            
            var cartaoDeCreditoObtido = new CartaoDeCredito(_numero, _nome, _cvv, _expiracao);
            
            cartaoDeCreditoObtido.ToExpectedObject().ShouldMatch(cartaoDeCreditoEsperado);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void Nao_deve_criar_um_cartao_de_credito_com_numero_invalido(string numeroInvalido)
        {
            const string mensagemDeErroEsperada = "É necessário informar o número do cartão de crédito";
            
            void Acao () => new CartaoDeCredito(numeroInvalido, _nome, _cvv, _expiracao);
            
            Assert.Throws<ExcecaoDeDominio<CartaoDeCredito>>(Acao).PossuiErroComAMensagemIgualA(mensagemDeErroEsperada);
        }
        
        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void Nao_deve_criar_um_cartao_de_credito_com_nome_invalido(string nomeInvalido)
        {
            const string mensagemDeErroEsperada = "É necessário informar o nome do dono do cartão de crédito";
            
            void Acao () => new CartaoDeCredito(nomeInvalido, _nome, _cvv, _expiracao);
            
            Assert.Throws<ExcecaoDeDominio<CartaoDeCredito>>(Acao).PossuiErroComAMensagemIgualA(mensagemDeErroEsperada);
        }
        
        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void Nao_deve_criar_um_cartao_de_credito_com_cvv_invalido(string cvvInvalido)
        {
            const string mensagemDeErroEsperada = "É necessário informar o cvv do cartão de crédito";
            
            void Acao () => new CartaoDeCredito(cvvInvalido, _nome, _cvv, _expiracao);
            
            Assert.Throws<ExcecaoDeDominio<CartaoDeCredito>>(Acao).PossuiErroComAMensagemIgualA(mensagemDeErroEsperada);
        }
    }
}