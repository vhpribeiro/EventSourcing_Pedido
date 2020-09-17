using Bogus;
using EventSourcing_Pedido.Aplicacao.Dtos;
using EventSourcing_Pedido.Aplicacao.Mapeadores;
using EventSourcing_Pedido.Test.Helpers._Builders.Dominio;
using ExpectedObjects;
using Xunit;

namespace EventSourcing_Pedido.Aplicacao.Test.Mapeadores
{
    public class MapeadorDeCartaoDeCreditoTeste
    {
        private Faker _faker;

        public MapeadorDeCartaoDeCreditoTeste()
        {
            _faker = new Faker();
        }
        
        [Fact]
        public void Deve_retornar_nulo_quando_dto_for_nulo()
        {
            CartaoDeCreditoDto cartaoDeCreditoDtoInvalido = null;
            
            var cartaoDeCredito = MapeadorDeCartaoDeCredito.Mapear(cartaoDeCreditoDtoInvalido);
            
            Assert.Null(cartaoDeCredito);
        }

        [Fact]
        public void Deve_mapear_o_cartao_de_credito()
        {
            var numero = _faker.Random.Int(0).ToString();
            var nome = _faker.Person.FirstName;
            var cvv = _faker.Random.Int(100, 999).ToString();
            const string expiracao = "03/27";
            var cartaoDeCreditoEsperado = CartaoDeCreditoBuilder.Novo().ComNome(nome).ComNumero(numero).ComCvv(cvv)
                .ComExpiracao(expiracao).Criar();
            var cartaoDeCreditoDto = new CartaoDeCreditoDto
            {
                Numero = numero,
                Nome = nome,
                CVV = cvv,
                Expiracao = expiracao,
            };
            
            var cartaoDeCreditoObtido = MapeadorDeCartaoDeCredito.Mapear(cartaoDeCreditoDto);
            
            cartaoDeCreditoEsperado.ToExpectedObject().ShouldMatch(cartaoDeCreditoObtido);
        }
    }
}