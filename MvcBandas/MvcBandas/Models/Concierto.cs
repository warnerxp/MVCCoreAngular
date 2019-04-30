using System;
using System.ComponentModel.DataAnnotations;

namespace MvcBandas.Models
{
    public class Concierto
    {
        public int Id { get; set; }
        [Display(Name ="Ubicación del concierto")]
        [Required(ErrorMessage="El lugar es requerido")]
        public string Lugar { get; set; }
        [Display(Name = "La banda es requerida")]
        [Required(ErrorMessage = "La banda es requerida")]
        public int BandaId { get; set; }
        public virtual Banda Banda {get;set;}
        [Display(Name = "Fecha y Hora")] 
        [Required(ErrorMessage = "La fecha es requerida")]
        public DateTime Fecha { get; set; }
    }
}
