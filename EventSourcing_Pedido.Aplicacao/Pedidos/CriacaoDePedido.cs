using System.Threading.Tasks;
using EasyNetQ;
using EventSourcing_Pedido.Aplicacao.Dtos;
using EventSourcing_Pedido.Aplicacao.InterfacesDeRepositorio;
using EventSourcing_Pedido.Aplicacao.Mapeadores;
using EventSourcing_Pedido.Dominio.Eventos;
using EventSourcing_Pedido.Dominio.Pedidos;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace EventSourcing_Pedido.Aplicacao.Pedidos
{
    public class CriacaoDePedido : ICriacaoDePedido
    {
        private readonly IPedidoRepositorio _pedidoRepositorio;
        private readonly IEventoRepositorio _eventoRepositorio;
        private readonly IBus _mensageria;
        private readonly IConfiguration _configuration;

        public CriacaoDePedido(IPedidoRepositorio pedidoRepositorio, IEventoRepositorio eventoRepositorio, IBus mensageria, IConfiguration configuration)
        {
            _pedidoRepositorio = pedidoRepositorio;
            _eventoRepositorio = eventoRepositorio;
            _mensageria = mensageria;
            _configuration = configuration;
        }

        public async Task Criar(PedidoDto pedidoDto)
        {
            var pedido = MapeadorDePedido.Mapear(pedidoDto);
            await _pedidoRepositorio.Salvar(pedido);

            await NotificarServicoDePagamento(pedido);
        }

        private async Task NotificarServicoDePagamento(Pedido pedido)
        {
            var eventoDePedidoCriado = new PedidoCriadoEvento(pedido);
            await _eventoRepositorio.Salvar(eventoDePedidoCriado);

            var nomeDaQueue = _configuration.GetValue<string>("RabbitQueue");
            var mensagemEmString = JsonConvert.SerializeObject(eventoDePedidoCriado);
            await _mensageria.SendAsync(nomeDaQueue, mensagemEmString);
        }
    }
}