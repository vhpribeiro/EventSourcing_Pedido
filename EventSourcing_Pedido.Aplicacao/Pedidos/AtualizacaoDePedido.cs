using System.Threading.Tasks;
using EasyNetQ;
using EventSourcing_Pedido.Aplicacao.Dtos;
using EventSourcing_Pedido.Aplicacao.InterfacesDeRepositorio;
using EventSourcing_Pedido.Aplicacao.Mapeadores;
using EventSourcing_Pedido.Dominio.CartoesDeCredito;
using EventSourcing_Pedido.Dominio.Pedidos;
using EventSourcingPedidoPagamento.Contratos.Eventos;

namespace EventSourcing_Pedido.Aplicacao.Pedidos
{
    public class AtualizacaoDePedido : IAtualizacaoDePedido
    {
        private readonly IPedidoRepositorio _pedidoRepositorio;
        private readonly IEventoRepositorio _eventoRepositorio;
        private readonly IBus _mensageria;

        public AtualizacaoDePedido(IPedidoRepositorio pedidoRepositorio, IEventoRepositorio eventoRepositorio,
            IBus mensageria)
        {
            _pedidoRepositorio = pedidoRepositorio;
            _eventoRepositorio = eventoRepositorio;
            _mensageria = mensageria;
        }

        public async Task AtualizarCartaoDeCredito(int idDoPedido, CartaoDeCreditoDto cartaoDeCreditoDto)
        {
            var novoCartaoDeCredito = MapeadorDeCartaoDeCredito.Mapear(cartaoDeCreditoDto);
            var pedido = _pedidoRepositorio.ObterPedidoPeloId(idDoPedido);
            
            pedido.AtualizarCartaoDeCredito(novoCartaoDeCredito);
            await _pedidoRepositorio.AtualizarPedido(pedido);

            await NotificarAlteracaoDeCartaoDeCreditoAoServicoDePagamento(pedido, novoCartaoDeCredito);
        }

        public async Task AprovarPagamento(PagamentoAprovadoEvento pagamentoAprovadoEvento)
        {
            var pedido = _pedidoRepositorio.ObterPedidoPeloId(pagamentoAprovadoEvento.IdDoPedido);
            
            pedido.AprovarPagamento();
            await _pedidoRepositorio.AtualizarPedido(pedido);
            await _eventoRepositorio.Salvar(pagamentoAprovadoEvento);
        }

        public async Task NegarPagamento(PagamentoRecusadoEvento pagamentoRecusadoEvento)
        {
            var pedido = _pedidoRepositorio.ObterPedidoPeloId(pagamentoRecusadoEvento.IdDoPedido);
            
            pedido.NegarPagamento();
            await _pedidoRepositorio.AtualizarPedido(pedido);
            await _eventoRepositorio.Salvar(pagamentoRecusadoEvento);
        }

        private async Task NotificarAlteracaoDeCartaoDeCreditoAoServicoDePagamento(Pedido pedido,
            CartaoDeCredito novoCartaoDeCredito)
        {
            var eventoDeAlteracaoDeCartaoDeCreditoDoPedido = 
                new AlterouCartaoDeCreditoDoPedidoEvento(pedido.Id, novoCartaoDeCredito.Nome,
                    novoCartaoDeCredito.Numero, pedido.Produto, pedido.Valor);
            await _eventoRepositorio.Salvar(eventoDeAlteracaoDeCartaoDeCreditoDoPedido);

            await _mensageria.PublishAsync(eventoDeAlteracaoDeCartaoDeCreditoDoPedido);
        }
    }
}