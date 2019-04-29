using System;
using System.ComponentModel.DataAnnotations;

namespace MvcBandas.Models
{
    public class Concierto
    {
        public int Id { get; set; }
        [Required(ErrorMessage="El lugar es requerido")]
        public string Lugar { get; set; }
        [Required(ErrorMessage = "La banda es requerida")]
        public int BandaId { get; set; }
        public virtual Banda Banda {get;set;}
        [Required(ErrorMessage = "La fecha es requerida")]
        public DateTime Fecha { get; set; }
    }
}
