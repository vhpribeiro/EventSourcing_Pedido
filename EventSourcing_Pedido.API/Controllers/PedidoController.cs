using EventSourcing_Pedido.Aplicacao.Dtos;
using EventSourcing_Pedido.Aplicacao.Pedidos;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcing_Pedido.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly ICriacaoDePedido _criacaoDePedido;

        public PedidoController(ICriacaoDePedido criacaoDePedido)
        {
            _criacaoDePedido = criacaoDePedido;
        }
        
        [HttpPost]
        public ActionResult Post([FromBody] PedidoDto pedidoDto)
        {
            _criacaoDePedido.Criar(pedidoDto);
            return Ok(true);
        }
    }
}