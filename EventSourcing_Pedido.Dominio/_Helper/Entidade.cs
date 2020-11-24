using System.ComponentModel.DataAnnotations.Schema;

namespace EventSourcing_Pedido.Dominio._Helper
{
    public abstract class Entidade
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}