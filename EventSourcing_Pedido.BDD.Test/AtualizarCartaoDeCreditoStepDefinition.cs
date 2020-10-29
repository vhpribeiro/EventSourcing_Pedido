using EasyNetQ;
using EventSourcing_Pedido.Aplicacao.Dtos;
using EventSourcing_Pedido.Aplicacao.InterfacesDeRepositorio;
using EventSourcing_Pedido.Aplicacao.Pedidos;
using EventSourcing_Pedido.Dominio.Pedidos;
using EventSourcing_Pedido.Test.Helpers._Builders.Dominio;
using Moq;
using TechTalk.SpecFlow;
using Xunit;

namespace EventSourcing_Pedido.BDD.Test
{
    //Esse método já foi coberto de teste no projeto "EventSourcing_Pedido.Aplicacao.Test"
    //Esse teste foi realizado apenas como método de estudo da lib Specflow e da metodologia BDD
    [Binding]
    public class AtualizarCartaoDeCreditoStepDefinition
    {
        private string _antigoNumeroDoCartaoDeCredito;
        private string _nomeDoAntigoDonoDoCartaoDeCredito;
        private string _novoNumeroDoCartaoDeCredito = "123456789";
        private string _nomeDoNovoDonoDoCartaoDeCredito = "Vitor Ribeiro";
        

        [Given("que o antigo número do cartão de crédito é (.*) e o nome do antigo dono é (.*)")]
        public void Preencher_novo_numero_do_cartao_de_credito(string novoNumeroDoCartaoDeCredito, string nomeDoAntigoDonoDoCartaoDeCredito)
        {
            _antigoNumeroDoCartaoDeCredito = novoNumeroDoCartaoDeCredito;
            _nomeDoAntigoDonoDoCartaoDeCredito = nomeDoAntigoDonoDoCartaoDeCredito;
        }

        [When("eu solicitar a atualização do cartão")]
        public void Simular_alteracao_do_cartao_de_credito()
        {
            const int idDoPedido = 5;
            var cartaoDeCreditoDto = new CartaoDeCreditoDto
            {
                CVV = "788",
                Expiracao = "03/28",
                Nome = _nomeDoNovoDonoDoCartaoDeCredito,
                Numero = _novoNumeroDoCartaoDeCredito
            };
            var cartaoDeCredito = CartaoDeCreditoBuilder.Novo().ComNumero(_antigoNumeroDoCartaoDeCredito).ComNome(_nomeDoAntigoDonoDoCartaoDeCredito).Criar();
            var pedido = PedidoBuilder.Novo().ComCartaoDeCredito(cartaoDeCredito).Criar();
            var pedidoRepositorio = new Mock<IPedidoRepositorio>();
            var eventoRepositorio = new Mock<IEventoRepositorio>();
            var mensageria = new Mock<IBus>();
            var atualizacaoDePedido = new AtualizacaoDePedido(pedidoRepositorio.Object, eventoRepositorio.Object, mensageria.Object);
            pedidoRepositorio.Setup(pr => pr.ObterPedidoPeloId(It.IsAny<int>())).Returns(pedido);
            pedidoRepositorio.Setup(pr => pr.Salvar(It.IsAny<Pedido>()));

            atualizacaoDePedido.AtualizarCartaoDeCredito(idDoPedido, cartaoDeCreditoDto);
        }

        [Then("o novo número do cartão de crédito será (.*) e o nome do novo dono será (.*)")]
        public void Validar_novo_numero_do_cartao_de_credito(string novoNumeroDoCartaoDeCredito, string nomeDoNovoDonoDoCartaoDeCredito)
        {
            Assert.Equal(_novoNumeroDoCartaoDeCredito, novoNumeroDoCartaoDeCredito);
            Assert.Equal(_nomeDoNovoDonoDoCartaoDeCredito, nomeDoNovoDonoDoCartaoDeCredito);
        }
    }
}