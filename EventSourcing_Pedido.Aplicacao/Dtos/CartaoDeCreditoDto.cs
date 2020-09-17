namespace EventSourcing_Pedido.Aplicacao.Dtos
{
    public class CartaoDeCreditoDto
    {
        public string Numero { get; set; }
        public string Nome { get; set; }
        public string CVV { get; set; }
        public string Expiracao { get; set; }
    }
}