﻿using System.Threading.Tasks;
using Bogus;
using EasyNetQ;
using EventSourcing_Pedido.Aplicacao.Dtos;
using EventSourcing_Pedido.Aplicacao.InterfacesDeRepositorio;
using EventSourcing_Pedido.Aplicacao.Pedidos;
using EventSourcing_Pedido.Dominio.Pedidos;
using EventSourcing_Pedido.Test.Helpers._Builders.Dominio;
using EventSourcingPedidoPagamento.Contratos.Eventos;
using ExpectedObjects;
using Moq;
using Xunit;

namespace EventSourcing_Pedido.Aplicacao.Test.Pedidos
{
    public class AtualizacaoDePedidoTeste
    {
        private readonly Faker _faker;
        private readonly Mock<IPedidoRepositorio> _pedidoRepositorio;
        private readonly Mock<IEventoRepositorio> _eventoRepositorio;
        private readonly Mock<IBus> _mensageria;
        private readonly AtualizacaoDePedido _atualizacaoDePedido;

        public AtualizacaoDePedidoTeste()
        {
            _faker = new Faker();
            _pedidoRepositorio = new Mock<IPedidoRepositorio>();
            _eventoRepositorio = new Mock<IEventoRepositorio>();
            _mensageria = new Mock<IBus>();
            _atualizacaoDePedido = new AtualizacaoDePedido(_pedidoRepositorio.Object, _eventoRepositorio.Object, _mensageria.Object);
            _eventoRepositorio.Setup(er => er.Salvar(It.IsAny<AlterouCartaoDeCreditoDoPedidoEvento>()));
        }
        
        [Fact]
        public async Task Deve_atualizar_cartao_de_credito_do_pedido()
        {
            var idDoPedido = _faker.Random.Int(0);
            const string expiracaoDoCartaoDeCredito = "03/27";
            var nomeDoDonoDoCartao = _faker.Person.FirstName;
            var numeroDoNovoCartaoDeCredito = _faker.Random.Int(0).ToString();
            var cvvDoNovoCartao = _faker.Random.Int(100, 999).ToString();
            var cartaoDeCreditoDto = new CartaoDeCreditoDto
            {
                Expiracao = expiracaoDoCartaoDeCredito,
                Nome = nomeDoDonoDoCartao,
                CVV = cvvDoNovoCartao,
                Numero = numeroDoNovoCartaoDeCredito
            };
            var pedido = PedidoBuilder.Novo().Criar();
            _pedidoRepositorio.Setup(pr => pr.ObterPedidoPeloId(idDoPedido)).Returns(pedido);
            _pedidoRepositorio.Setup(pr => pr.Adicionar(It.IsAny<Pedido>()));
            
            await _atualizacaoDePedido.AtualizarCartaoDeCredito(idDoPedido, cartaoDeCreditoDto);
            
            _pedidoRepositorio.Verify(pr 
                => pr.AtualizarPedido(It.Is<Pedido>(p => p.Id == pedido.Id
                && p.CartaoDeCredito.Nome == nomeDoDonoDoCartao && p.CartaoDeCredito.Numero == numeroDoNovoCartaoDeCredito)));
        }
        
        [Fact]
        public async Task Deve_salvar_evento_de_alteracao_de_cartao_de_credito_do_pedido()
        {
            var idDoPedido = _faker.Random.Int(0);
            const string expiracaoDoCartaoDeCredito = "01/27";
            var nomeDoDonoDoCartao = _faker.Person.FirstName;
            var numeroDoNovoCartaoDeCredito = _faker.Random.Int(0).ToString();
            var cvvDoNovoCartao = _faker.Random.Int(100, 999).ToString();
            var cartaoDeCreditoDto = new CartaoDeCreditoDto
            {
                Expiracao = expiracaoDoCartaoDeCredito,
                Nome = nomeDoDonoDoCartao,
                CVV = cvvDoNovoCartao,
                Numero = numeroDoNovoCartaoDeCredito
            };
            var pedido = PedidoBuilder.Novo().Criar();
            _pedidoRepositorio.Setup(pr => pr.ObterPedidoPeloId(idDoPedido)).Returns(pedido);
            _pedidoRepositorio.Setup(pr => pr.Adicionar(It.IsAny<Pedido>()));

            
            await _atualizacaoDePedido.AtualizarCartaoDeCredito(idDoPedido, cartaoDeCreditoDto);
            
            _eventoRepositorio.Verify(er 
                => er.Salvar(It.Is<AlterouCartaoDeCreditoDoPedidoEvento>(e 
                    => e.IdDoPedido == pedido.Id && e.NomeDoUsuario == nomeDoDonoDoCartao && e.NumeroDoCartao == numeroDoNovoCartaoDeCredito)));
        }

        [Fact]
        public async Task Deve_notificar_servico_de_pagamento_sobre_atualizacao_do_cartao_de_credito()
        {
            var idDoPedido = _faker.Random.Int(0);
            const string expiracaoDoCartaoDeCredito = "02/27";
            var nomeDoDonoDoCartao = _faker.Person.FirstName;
            var numeroDoNovoCartaoDeCredito = _faker.Random.Int(0).ToString();
            var cvvDoNovoCartao = _faker.Random.Int(100, 999).ToString();
            var cartaoDeCreditoDto = new CartaoDeCreditoDto
            {
                Expiracao = expiracaoDoCartaoDeCredito,
                Nome = nomeDoDonoDoCartao,
                CVV = cvvDoNovoCartao,
                Numero = numeroDoNovoCartaoDeCredito
            };
            var pedido = PedidoBuilder.Novo().Criar();
            _pedidoRepositorio.Setup(pr => pr.ObterPedidoPeloId(idDoPedido)).Returns(pedido);
            _pedidoRepositorio.Setup(pr => pr.Adicionar(It.IsAny<Pedido>()));
            
            await _atualizacaoDePedido.AtualizarCartaoDeCredito(idDoPedido, cartaoDeCreditoDto);
            
            _mensageria.Verify(m => m.PublishAsync(
                It.Is<AlterouCartaoDeCreditoDoPedidoEvento>(a
                    => a.IdDoPedido == pedido.Id && a.NomeDoUsuario == nomeDoDonoDoCartao && a.NumeroDoCartao == numeroDoNovoCartaoDeCredito)));
        }

        [Fact]
        public async Task Deve_aprovar_pagamento()
        {
            var idDoPedido = _faker.Random.Int(0);
            var nomeDoDonoDoCartao = _faker.Person.FirstName;
            var numeroDoNovoCartaoDeCredito = _faker.Random.Int(0).ToString();
            var produto = _faker.Random.Word();
            var valor = _faker.Random.Decimal();
            var pedido = PedidoBuilder.Novo().Criar();
            var pagamentoAprovadoEvento = new PagamentoAprovadoEvento(idDoPedido, nomeDoDonoDoCartao, numeroDoNovoCartaoDeCredito, produto, valor);
            _pedidoRepositorio.Setup(pr => pr.ObterPedidoPeloId(idDoPedido)).Returns(pedido);
            _pedidoRepositorio.Setup(pr => pr.AtualizarPedido(It.IsAny<Pedido>()));
            _eventoRepositorio.Setup(er => er.Salvar(It.IsAny<PagamentoAprovadoEvento>()));
            
            await _atualizacaoDePedido.AprovarPagamento(pagamentoAprovadoEvento);
            
            _pedidoRepositorio.Verify(pr => pr.AtualizarPedido(It.Is<Pedido>(
                p => p.Id == pedido.Id && p.Situacao == SituacaoDoPedido.PagamentoAprovado)));
        }

        [Fact]
        public async Task Deve_salvar_evennto_de_pagamento_aprovado_ao_aprovar_pagamento()
        {
            var idDoPedido = _faker.Random.Int(0);
            var nomeDoDonoDoCartao = _faker.Person.FirstName;
            var numeroDoNovoCartaoDeCredito = _faker.Random.Int(0).ToString();
            var produto = _faker.Random.Word();
            var valor = _faker.Random.Decimal();
            var pedido = PedidoBuilder.Novo().Criar();
            var pagamentoAprovadoEvento = new PagamentoAprovadoEvento(idDoPedido, nomeDoDonoDoCartao, numeroDoNovoCartaoDeCredito, produto, valor);
            _pedidoRepositorio.Setup(pr => pr.ObterPedidoPeloId(idDoPedido)).Returns(pedido);
            _pedidoRepositorio.Setup(pr => pr.AtualizarPedido(It.IsAny<Pedido>()));
            _eventoRepositorio.Setup(er => er.Salvar(It.IsAny<PagamentoAprovadoEvento>()));
            
            await _atualizacaoDePedido.AprovarPagamento(pagamentoAprovadoEvento);
            
            _eventoRepositorio.Verify(er => er.Salvar(It.Is<PagamentoAprovadoEvento>(
                evento => evento.ToExpectedObject().Matches(pagamentoAprovadoEvento))));
        }
        
        [Fact]
        public async Task Deve_negar_pagamento()
        {
            var idDoPedido = _faker.Random.Int(0);
            var nomeDoDonoDoCartao = _faker.Person.FirstName;
            var numeroDoNovoCartaoDeCredito = _faker.Random.Int(0).ToString();
            var produto = _faker.Random.Word();
            var valor = _faker.Random.Decimal();
            var pedido = PedidoBuilder.Novo().Criar();
            var pagamentoRecusadoEvento = new PagamentoRecusadoEvento(idDoPedido, nomeDoDonoDoCartao, numeroDoNovoCartaoDeCredito, produto, valor);
            _pedidoRepositorio.Setup(pr => pr.ObterPedidoPeloId(idDoPedido)).Returns(pedido);
            _pedidoRepositorio.Setup(pr => pr.AtualizarPedido(It.IsAny<Pedido>()));
            _eventoRepositorio.Setup(er => er.Salvar(It.IsAny<PagamentoRecusadoEvento>()));
            
            await _atualizacaoDePedido.NegarPagamento(pagamentoRecusadoEvento);
            
            _pedidoRepositorio.Verify(pr => pr.AtualizarPedido(It.Is<Pedido>(
                p => p.Id == pedido.Id && p.Situacao == SituacaoDoPedido.PagamentoNegado)));
        }
        
        [Fact]
        public async Task Deve_salvar_evennto_de_pagamentorecusado_ao_negar_pagamento()
        {
            var idDoPedido = _faker.Random.Int(0);
            var nomeDoDonoDoCartao = _faker.Person.FirstName;
            var numeroDoNovoCartaoDeCredito = _faker.Random.Int(0).ToString();
            var produto = _faker.Random.Word();
            var valor = _faker.Random.Decimal();
            var pedido = PedidoBuilder.Novo().Criar();
            var pagamentoRecusadoEvento = new PagamentoRecusadoEvento(idDoPedido, nomeDoDonoDoCartao, numeroDoNovoCartaoDeCredito, produto, valor);
            _pedidoRepositorio.Setup(pr => pr.ObterPedidoPeloId(idDoPedido)).Returns(pedido);
            _pedidoRepositorio.Setup(pr => pr.AtualizarPedido(It.IsAny<Pedido>()));
            _eventoRepositorio.Setup(er => er.Salvar(It.IsAny<PagamentoRecusadoEvento>()));
            
            await _atualizacaoDePedido.NegarPagamento(pagamentoRecusadoEvento);
            
            _eventoRepositorio.Verify(er => er.Salvar(It.Is<PagamentoRecusadoEvento>(
                evento => evento.ToExpectedObject().Matches(pagamentoRecusadoEvento))));
        }
    }
}