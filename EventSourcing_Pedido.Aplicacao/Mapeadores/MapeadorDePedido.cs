using System;
using EventSourcing_Pedido.Aplicacao.Dtos;
using EventSourcing_Pedido.Aplicacao.Pedidos;
using EventSourcing_Pedido.Dominio.Pedidos;

namespace EventSourcing_Pedido.Aplicacao.Mapeadores
{
    public class MapeadorDePedido
    {
        public static Pedido Mapear(PedidoDto pedidoDto)
        {
            if (pedidoDto == null) throw new Exception();

            var cartaoDeCredito = MapeadorDeCartaoDeCredito.Mapear(pedidoDto.CartaoDeCreditoDto);
            return new Pedido(pedidoDto.Produto, pedidoDto.Quantidade, pedidoDto.Valor, cartaoDeCredito);
        }
    }
}