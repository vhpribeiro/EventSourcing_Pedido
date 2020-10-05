using System.Threading.Tasks;
using EasyNetQ;
using EventSourcing_Pedido.Aplicacao.Dtos;
using EventSourcing_Pedido.Aplicacao.InterfacesDeRepositorio;
using EventSourcing_Pedido.Aplicacao.Mapeadores;
using EventSourcing_Pedido.Dominio.CartoesDeCredito;
using EventSourcing_Pedido.Dominio.Eventos;
using EventSourcing_Pedido.Dominio.Pedidos;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace EventSourcing_Pedido.Aplicacao.Pedidos
{
    public class AtualizacaoDePedido : IAtualizacaoDePedido
    {
        private readonly IPedidoRepositorio _pedidoRepositorio;
        private readonly IEventoRepositorio _eventoRepositorio;
        private readonly IConfiguration _configuration;
        private readonly IBus _mensageria;

        public AtualizacaoDePedido(IPedidoRepositorio pedidoRepositorio, IEventoRepositorio eventoRepositorio,
            IBus mensageria, IConfiguration configuration)
        {
            _pedidoRepositorio = pedidoRepositorio;
            _eventoRepositorio = eventoRepositorio;
            _configuration = configuration;
            _mensageria = mensageria;
        }

        public async Task AtualizarCartaoDeCredito(int idDoPedido, CartaoDeCreditoDto cartaoDeCreditoDto)
        {
            var novoCartaoDeCredito = MapeadorDeCartaoDeCredito.Mapear(cartaoDeCreditoDto);
            var pedido = _pedidoRepositorio.ObterPedidoPeloId(idDoPedido);
            
            pedido.AtualizarCartaoDeCredito(novoCartaoDeCredito);
            _pedidoRepositorio.AtualizarPedido(pedido);

            await NotificarAlteracaoDeCartaoDeCreditoAoServicoDePagamento(pedido, novoCartaoDeCredito);
        }

        private async Task NotificarAlteracaoDeCartaoDeCreditoAoServicoDePagamento(Pedido pedido,
            CartaoDeCredito novoCartaoDeCredito)
        {
            var eventoDeAlteracaoDeCartaoDeCreditoDoPedido = 
                new AlterouCartaoDeCreditoDoPedidoEvento(pedido, novoCartaoDeCredito);
            await _eventoRepositorio.Salvar(eventoDeAlteracaoDeCartaoDeCreditoDoPedido);

            var nomeDaQueue = _configuration.GetValue<string>("RabbitQueue");
            var mensagemEmString = JsonConvert.SerializeObject(eventoDeAlteracaoDeCartaoDeCreditoDoPedido);
            await _mensageria.SendAsync(nomeDaQueue, mensagemEmString);
        }
    }
}