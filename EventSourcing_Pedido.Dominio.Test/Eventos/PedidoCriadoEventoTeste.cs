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
                MetaDado = JsonConvert.SerializeObject(pedido)
            };
            
            var eventoObtido = new PedidoCriadoEvento(pedido);
            
            eventoEsperado.ToExpectedObject().ShouldMatch(eventoObtido);
        }
    }
}