using System;
using EventSourcing_Pedido.Dominio._Helper;
using EventSourcing_Pedido.Dominio.CartoesDeCredito;

namespace EventSourcing_Pedido.Dominio.Eventos
{
    public class PedidoCriadoEvento
    {
        public Guid Id { get; set; }
        public CartaoDeCredito CartaoDeCredito { get; set; }
        
        public PedidoCriadoEvento(Guid id, CartaoDeCredito cartaoDeCredito)
        {
            ValidarInformacoes(id, cartaoDeCredito);
            
            Id = id;
            CartaoDeCredito = cartaoDeCredito;
        }

        private static void ValidarInformacoes(Guid id, CartaoDeCredito cartaoDeCredito)
        {
            Validacoes<PedidoCriadoEvento>.Criar()
                .Quando(id == Guid.Empty, "É necessário informar o id")
                .Obrigando(cartaoDeCredito, "É necessário informar o cartão de crédito")
                .DispararSeHouverErros();
        }
    }
}