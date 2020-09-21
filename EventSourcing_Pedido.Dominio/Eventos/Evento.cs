using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace EventSourcing_Pedido.Dominio.Eventos
{
    public abstract class Evento<T> where T: class 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string MetaDado { get; set; }
        public DateTime Data { get; set; }
        public int IdDoObjeto { get; set; }
        
        public Evento(int identificadorDoObjeto, T objeto)
        {
            IdDoObjeto = identificadorDoObjeto;
            Data = DateTime.Now;
            MetaDado = JsonConvert.SerializeObject(objeto);
        }
    }
}