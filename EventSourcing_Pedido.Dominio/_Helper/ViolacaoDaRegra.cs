using System.Linq.Expressions;

namespace EventSourcing_Pedido.Dominio._Helper
{
    public class ViolacaoDeRegra
    {
        public LambdaExpression Propriedade { get; internal set; }
        public string Mensagem { get; internal set; }
    }
}