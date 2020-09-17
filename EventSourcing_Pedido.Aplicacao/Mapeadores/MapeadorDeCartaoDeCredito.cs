using System;
using EventSourcing_Pedido.Aplicacao.Dtos;
using EventSourcing_Pedido.Dominio.CartoesDeCredito;

namespace EventSourcing_Pedido.Aplicacao.Mapeadores
{
    public class MapeadorDeCartaoDeCredito
    {
        public static CartaoDeCredito Mapear(CartaoDeCreditoDto cartaoDeCreditoDto)
        {
            return cartaoDeCreditoDto == null ? 
                null : 
                new CartaoDeCredito(cartaoDeCreditoDto.Numero, cartaoDeCreditoDto.Nome, cartaoDeCreditoDto.CVV, cartaoDeCreditoDto.Expiracao);
        }
    }
}