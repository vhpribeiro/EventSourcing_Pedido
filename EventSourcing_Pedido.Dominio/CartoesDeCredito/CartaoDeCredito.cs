using System.ComponentModel.DataAnnotations.Schema;
using EventSourcing_Pedido.Dominio._Helper;

namespace EventSourcing_Pedido.Dominio.CartoesDeCredito
{
    public class CartaoDeCredito
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Numero { get; set; }
        public string Nome { get; set; }
        public string CVV { get; set; }
        public string Expiracao { get; set; }

        public CartaoDeCredito() {}

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