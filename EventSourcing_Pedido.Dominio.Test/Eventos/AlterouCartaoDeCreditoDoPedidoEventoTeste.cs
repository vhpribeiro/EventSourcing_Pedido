using System;
using EventSourcing_Pedido.Dominio.Eventos;
using EventSourcing_Pedido.Test.Helpers._Builders.Dominio;
using ExpectedObjects;
using Xunit;

namespace EventSourcing_Pedido.Dominio.Test.Eventos
{
    public class AlterouCartaoDeCreditoDoPedidoEventoTeste
    {
        [Fact]
        public void Deve_criar_um_evento_de_alteracao_de_cartao_de_credito_do_pedido()
        {
            var cartaoDeCredito = CartaoDeCreditoBuilder.Novo()
                .ComNome("João Ribeiro")
                .ComNumero("4567895465798")
                .Criar();
            var pedido = PedidoBuilder.Novo().ComCartaoDeCredito(cartaoDeCredito).Criar();
            var novoCartaoDeCredito = CartaoDeCreditoBuilder.Novo()
                .ComNome("Vitor Ribeiro")
                .ComNumero("123456789")
                .Criar();
            var eventoEsperado = new
            {
                IdDoPedido = pedido.Id,
                Data = DateTime.Now,
                NomeDoUsuario = novoCartaoDeCredito.Nome,
                NumeroDoCartao = novoCartaoDeCredito.Numero,
                Produto = pedido.Produto,
                Valor = pedido.Valor,
            };
            
            var eventoObtido = new AlterouCartaoDeCreditoDoPedidoEvento(pedido, novoCartaoDeCredito);
            
            eventoEsperado.ToExpectedObject(ctx 
                    => ctx.Ignore(e => e.Data))
                .ShouldMatch(eventoObtido);
        }
    }
}