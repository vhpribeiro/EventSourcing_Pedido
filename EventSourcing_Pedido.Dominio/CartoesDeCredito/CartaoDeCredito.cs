using EventSourcing_Pedido.Dominio._Helper;

namespace EventSourcing_Pedido.Dominio.CartoesDeCredito
{
    public class CartaoDeCredito
    {
        public string Numero { get; }
        public string Nome { get; }
        public string CVV { get; }
        public string Expiracao { get; }

        public CartaoDeCredito(string numero, string nome, string cvv, string expiracao)
        {
            ValidarInformacoes(numero, nome, cvv);
            
            Numero = numero;
            Nome = nome;
            CVV = cvv;
            Expiracao = expiracao;
        }

        private static void ValidarInformacoes(string numero, string nome, string cvv)
        {
            Validacoes<CartaoDeCredito>.Criar()
                .Obrigando(numero, "É necessário informar o número do cartão de crédito")
                .Obrigando(nome, "É necessário informar o nome do dono do cartão de crédito")
                .Obrigando(cvv, "É necessário informar o cvv do cartão de crédito")
                .DispararSeHouverErros();
        }
    }
}