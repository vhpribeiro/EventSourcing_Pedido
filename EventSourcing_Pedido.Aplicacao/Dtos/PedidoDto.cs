namespace EventSourcing_Pedido.Aplicacao.Dtos
{
    public class PedidoDto
    {
        public string Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public CartaoDeCreditoDto CartaoDeCreditoDto { get; set; }
    }
}