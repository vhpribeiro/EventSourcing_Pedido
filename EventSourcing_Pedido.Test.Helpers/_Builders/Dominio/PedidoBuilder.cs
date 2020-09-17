using Bogus;
using EventSourcing_Pedido.Dominio.CartoesDeCredito;
using EventSourcing_Pedido.Dominio.Pedidos;

namespace EventSourcing_Pedido.Test.Helpers._Builders.Dominio
{
    public class PedidoBuilder
    {
        private static readonly Faker _faker = new Faker();
        private string _produto = _faker.Random.Word();
        private int _quantidade = _faker.Random.Int(0);
        private decimal _valor = _faker.Random.Decimal(0);
        private CartaoDeCredito _cartaoDeCredito = CartaoDeCreditoBuilder.Novo().Criar();

        public static PedidoBuilder Novo()
        {
            return new PedidoBuilder();
        }

        public PedidoBuilder ComProduto(string produto)
        {
            _produto = produto;
            return this;
        }

        public PedidoBuilder ComQuantidade(int quantidade)
        {
            _quantidade = quantidade;
            return this;
        }

        public PedidoBuilder ComValor(decimal valor)
        {
            _valor = valor;
            return this;
        }

        public PedidoBuilder ComCartaoDeCredito(CartaoDeCredito cartaoDeCredito)
        {
            _cartaoDeCredito = cartaoDeCredito;
            return this;
        }

        public Pedido Criar()
        {
            return new Pedido(_produto, _quantidade, _valor, _cartaoDeCredito);
        }
        
    }
}