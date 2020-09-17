using Bogus;
using EventSourcing_Pedido.Dominio;
using EventSourcing_Pedido.Dominio.CartoesDeCredito;

namespace EventSourcing_Pedido.Test.Helpers._Builders.Dominio
{
    public class CartaoDeCreditoBuilder
    {
        private static Faker _faker = new Faker();
        private string _numero = _faker.Random.Word();
        private string _nome = _faker.Person.FirstName;
        private string _cvv = _faker.Random.Word();
        private string _expiracao = _faker.Random.Word();

        public static CartaoDeCreditoBuilder Novo()
        {
            return new CartaoDeCreditoBuilder();
        }

        public CartaoDeCreditoBuilder ComNumero(string numero)
        {
            _numero = numero;
            return this;
        }

        public CartaoDeCreditoBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public CartaoDeCreditoBuilder ComCvv(string cvv)
        {
            _cvv = cvv;
            return this;
        }

        public CartaoDeCreditoBuilder ComExpiracao(string expiracao)
        {
            _expiracao = expiracao;
            return this;
        }

        public CartaoDeCredito Criar() => new CartaoDeCredito(_numero, _nome, _cvv, _expiracao);
    }
}