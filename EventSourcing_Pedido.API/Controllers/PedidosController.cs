using System.Threading.Tasks;
using EventSourcing_Pedido.Aplicacao.Dtos;
using EventSourcing_Pedido.Aplicacao.Pedidos;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcing_Pedido.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly ICriacaoDePedido _criacaoDePedido;
        private readonly IAtualizacaoDePedido _atualizacaoDePedido;

        public PedidosController(ICriacaoDePedido criacaoDePedido, IAtualizacaoDePedido atualizacaoDePedido)
        {
            _criacaoDePedido = criacaoDePedido;
            _atualizacaoDePedido = atualizacaoDePedido;
        }
        
        [HttpPost]
        public async Task<ActionResult> CriarPedido([FromBody] PedidoDto pedidoDto)
        {
            await _criacaoDePedido.Criar(pedidoDto);
            return Ok(true);
        }

        [HttpPut]
        [Route("{idDoPedido}")]
        public async Task<ActionResult> AtualizarCartaoDeCredito(int idDoPedido,
            [FromBody] CartaoDeCreditoDto novoCartaoDeCreditoDto)
        {
            await _atualizacaoDePedido.AtualizarCartaoDeCredito(idDoPedido, novoCartaoDeCreditoDto);
            return Ok(true);
        }
    }
}