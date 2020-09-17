using System;
using EventSourcing_Pedido.Dominio._Helper;
using EventSourcing_Pedido.Dominio.CartoesDeCredito;
using EventSourcing_Pedido.Dominio.Eventos;
using EventSourcing_Pedido.Test.Helpers._Builders.Dominio;
using ExpectedObjects;
using Xunit;

namespace EventSourcing_Pedido.Dominio.Test.Eventos
{
    public class PedidoCriadoEventoTeste
    {
        [Fact]
        public void Deve_criar_um_evento_de_pedido_criado()
        {
            var cartaoDeCredito = CartaoDeCreditoBuilder.Novo().Criar();
            var id = Guid.NewGuid();
            var eventoEsperado = new
            {
                Id = id,
                CartaoDeCredito = cartaoDeCredito
            };
            
            var eventoObtido = new PedidoCriadoEvento(id, cartaoDeCredito);
            
            eventoEsperado.ToExpectedObject().ShouldMatch(eventoObtido);
        }

        [Fact]
        public void Nao_deve_criar_um_evento_de_pedido_criado_sem_cartao_de_credito()
        {
            const string mensagemDeErroEsperada = "É necessário informar o cartão de crédito";
            CartaoDeCredito cartaoDeCreditoTesteInvalido = null;
            var id = Guid.NewGuid();
            
            void Acao () => new PedidoCriadoEvento(id, cartaoDeCreditoTesteInvalido);
            
            Assert.Throws<ExcecaoDeDominio<PedidoCriadoEvento>>(Acao).PossuiErroComAMensagemIgualA(mensagemDeErroEsperada);
        }
        
        [Fact]
        public void Nao_deve_criar_um_evento_de_pedido_criado_sem_id()
        {
            const string mensagemDeErroEsperada = "É necessário informar o id";
            var cartaoDeCredito = CartaoDeCreditoBuilder.Novo().Criar();
            var idInvalido = Guid.Empty;
            
            void Acao () => new PedidoCriadoEvento(idInvalido, cartaoDeCredito);
            
            Assert.Throws<ExcecaoDeDominio<PedidoCriadoEvento>>(Acao).PossuiErroComAMensagemIgualA(mensagemDeErroEsperada);
        }
    }
}