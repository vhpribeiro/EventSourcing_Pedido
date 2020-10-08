using System.Threading.Tasks;
using Bogus;
using EasyNetQ;
using EventSourcing_Pedido.Aplicacao.Dtos;
using EventSourcing_Pedido.Aplicacao.InterfacesDeRepositorio;
using EventSourcing_Pedido.Aplicacao.Mapeadores;
using EventSourcing_Pedido.Aplicacao.Pedidos;
using EventSourcing_Pedido.Dominio.Pedidos;
using EventSourcing_Pedido.Test.Helpers._Builders.Dominio;
using EventSourcingPedidoPagamento.Contratos.Eventos;
using ExpectedObjects;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace EventSourcing_Pedido.Aplicacao.Test.Pedidos
{
    public class CriacaoDePedidoTeste
    {
        private readonly Faker _faker;
        private readonly Mock<IPedidoRepositorio> _pedidoRepositorio;
        private readonly CriacaoDePedido _criacaoDePedido;
        private readonly Mock<IEventoRepositorio> _eventoRepositorio;
        private readonly Mock<IBus> _mensageria;

        public CriacaoDePedidoTeste()
        {
            _faker = new Faker();
            _pedidoRepositorio = new Mock<IPedidoRepositorio>();
            _eventoRepositorio = new Mock<IEventoRepositorio>();
            _mensageria = new Mock<IBus>();
            _criacaoDePedido = new CriacaoDePedido(_pedidoRepositorio.Object, _eventoRepositorio.Object, _mensageria.Object);
            _pedidoRepositorio.Setup(pr => pr.Salvar(It.IsAny<Pedido>()));
            _eventoRepositorio.Setup(er => er.Salvar(It.IsAny<PedidoCriadoEvento>()));
            var configurationSection = new Mock<IConfigurationSection>();
        }
        
        [Fact]
        public async Task Deve_criar_um_pedido()
        {
            var produto = _faker.Random.Word();
            var quantidade = _faker.Random.Int(0);
            var valor = _faker.Random.Decimal();
            var cartaoDeCreditoDto = new CartaoDeCreditoDto
            {
                Numero = _faker.Random.Int(0).ToString(),
                Nome = _faker.Person.FirstName,
                CVV = _faker.Random.Int(100, 999).ToString(),
                Expiracao = "03/27"
            };
            var cartaoDeCredito = MapeadorDeCartaoDeCredito.Mapear(cartaoDeCreditoDto);
            var pedidoDto = new PedidoDto
            {
                Produto = produto,
                Quantidade = quantidade,
                Valor = valor,
                CartaoDeCreditoDto = cartaoDeCreditoDto
            };
            var pedidoEsperado = PedidoBuilder.Novo().ComProduto(produto).ComQuantidade(quantidade).ComValor(valor)
                .ComCartaoDeCredito(cartaoDeCredito).Criar();
            
            await _criacaoDePedido.Criar(pedidoDto);
            
            _pedidoRepositorio.Verify(pr => pr.Salvar(It.Is<Pedido>(pedido 
                => pedido.ToExpectedObject(ctx => ctx.Ignore(p => p.Id)).Matches(pedidoEsperado))));
        }
        
        [Fact]
        public async Task Deve_salvar_um_evento_de_pedido_criado_ao_criar_pedido()
        {
            var produto = _faker.Random.Word();
            var quantidade = _faker.Random.Int(0);
            var valor = _faker.Random.Decimal();
            var cartaoDeCreditoDto = new CartaoDeCreditoDto
            {
                Numero = _faker.Random.Int(0).ToString(),
                Nome = _faker.Person.FirstName,
                CVV = _faker.Random.Int(100, 999).ToString(),
                Expiracao = "03/27"
            };
            var cartaoDeCredito = MapeadorDeCartaoDeCredito.Mapear(cartaoDeCreditoDto);
            var pedidoDto = new PedidoDto
            {
                Produto = produto,
                Quantidade = quantidade,
                Valor = valor,
                CartaoDeCreditoDto = cartaoDeCreditoDto
            };
            var pedidoCriado = PedidoBuilder.Novo().ComProduto(produto).ComQuantidade(quantidade).ComValor(valor)
                .ComCartaoDeCredito(cartaoDeCredito).Criar();
            var eventoEsperado = new PedidoCriadoEvento(pedidoCriado.Id, pedidoCriado.CartaoDeCredito.Nome,
                pedidoCriado.CartaoDeCredito.Numero, pedidoCriado.Produto, pedidoCriado.Valor);
            
            await _criacaoDePedido.Criar(pedidoDto);
            
            _eventoRepositorio.Verify(pr => pr.Salvar(It.Is<PedidoCriadoEvento>(evento 
                => evento.ToExpectedObject(ctx => ctx.Ignore(p => p.Id)).Matches(eventoEsperado))));
        }

        [Fact]
        public async Task Deve_notificar_que_pedido_foi_criado()
        {
            var produto = _faker.Random.Word();
            var quantidade = _faker.Random.Int(0);
            var valor = _faker.Random.Decimal();
            var cartaoDeCreditoDto = new CartaoDeCreditoDto
            {
                Numero = _faker.Random.Int(0).ToString(),
                Nome = _faker.Person.FirstName,
                CVV = _faker.Random.Int(100, 999).ToString(),
                Expiracao = "03/27"
            };
            var pedidoDto = new PedidoDto
            {
                Produto = produto,
                Quantidade = quantidade,
                Valor = valor,
                CartaoDeCreditoDto = cartaoDeCreditoDto
            };

            await _criacaoDePedido.Criar(pedidoDto);

            _mensageria.Verify(m => m.PublishAsync(
                It.Is<PedidoCriadoEvento>(p 
                    => p.Produto == produto && p.Valor == valor && p.NomeDoUsuario == cartaoDeCreditoDto.Nome)));
        }
    }
}