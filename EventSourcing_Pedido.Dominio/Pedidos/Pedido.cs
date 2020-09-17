using System;
using System.Text.Json.Serialization;
using EventSourcing_Pedido.Dominio._Helper;
using EventSourcing_Pedido.Dominio.CartoesDeCredito;

namespace EventSourcing_Pedido.Dominio.Pedidos
{
    public class Pedido
    {
        [JsonIgnore]
        public Guid Id { get; }
        public string Produto { get; }
        public int Quantidade { get; }
        public decimal Valor { get; }
        public CartaoDeCredito CartaoDeCredito { get; }

        public Pedido(string produto, int quantidade, decimal valor, CartaoDeCredito cartaoDeCredito)
        {
            ValidarInformacoes(produto, quantidade, valor, cartaoDeCredito);
            
            Id = Guid.NewGuid();
            Produto = produto;
            Quantidade = quantidade;
            Valor = valor;
            CartaoDeCredito = cartaoDeCredito;
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
    }
}