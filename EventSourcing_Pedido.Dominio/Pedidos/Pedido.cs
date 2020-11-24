using System;
using System.ComponentModel.DataAnnotations.Schema;
using EventSourcing_Pedido.Dominio._Helper;
using EventSourcing_Pedido.Dominio.CartoesDeCredito;

namespace EventSourcing_Pedido.Dominio.Pedidos
{
    public class Pedido : Entidade
    {
        public string Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public SituacaoDoPedido Situacao { get; set; }
        public int CartaoDeCreditoId { get; set; }
        public CartaoDeCredito CartaoDeCredito { get; set; }

        public Pedido() {}

        public Pedido(string produto, int quantidade, decimal valor, CartaoDeCredito cartaoDeCredito)
        {
            ValidarInformacoes(produto, quantidade, valor, cartaoDeCredito);
            
            Produto = produto;
            Quantidade = quantidade;
            Valor = valor;
            CartaoDeCredito = cartaoDeCredito;
            Situacao = SituacaoDoPedido.PedidoCriado;
        }

        private void ValidarInformacoes(string produto, int quantidade, decimal valor, CartaoDeCredito cartaoDeCredito)
        {
            Validacoes<Pedido>.Criar()
                .Obrigando(produto, "É necessário informar um produto válido")
                .Obrigando(quantidade, "É necessário informar uma quantidade válida")
                .Obrigando(valor, "É necessário informar um valor válido")
                .Obrigando(cartaoDeCredito, "É necessário informar um valor válido")
                .DispararSeHouverErros();
        }

        public void AtualizarCartaoDeCredito(CartaoDeCredito cartaoDeCredito)
        {
            Validacao<Pedido>.EhObrigatorio(cartaoDeCredito, "É necessário informar um cartão de crédito");

            CartaoDeCredito = cartaoDeCredito;
        }

        public void AprovarPagamento() => Situacao = SituacaoDoPedido.PagamentoAprovado;

        public void NegarPagamento() => Situacao = SituacaoDoPedido.PagamentoNegado;
    }
}