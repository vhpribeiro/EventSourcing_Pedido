using System;
using EventSourcing_Pedido.Dominio.Eventos;
using EventSourcing_Pedido.Test.Helpers._Builders.Dominio;
using ExpectedObjects;
using Newtonsoft.Json;
using Xunit;

namespace EventSourcing_Pedido.Dominio.Test.Eventos
{
    public class PedidoCriadoEventoTeste
    {
        [Fact]
        public void Deve_criar_um_evento_de_pedido_criado()
        {
            var cartaoDeCredito = CartaoDeCreditoBuilder.Novo().Criar();
            var pedido = PedidoBuilder.Novo().ComCartaoDeCredito(cartaoDeCredito).Criar();
            var eventoEsperado = new
            {
                IdDoPedido = pedido.Id,
                Data = DateTime.Now,
                NomeDoUsuario = cartaoDeCredito.Nome,
                NumeroDoCartao = cartaoDeCredito.Numero,
                Produto = pedido.Produto,
                Valor = pedido.Valor,
            };
            
            var eventoObtido = new PedidoCriadoEvento(pedido);
            
            eventoEsperado.ToExpectedObject(ctx 
                => ctx.Ignore(e => e.Data))
                .ShouldMatch(eventoObtido);
        }
    }
}