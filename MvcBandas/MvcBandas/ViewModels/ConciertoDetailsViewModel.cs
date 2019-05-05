using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcBandas.ViewModels
{
    public class ConciertoDetailsViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Ubicación del concierto")]
        
        public string Lugar { get; set; }
        [Display(Name = "La banda es requerida")]
        
        public int BandaId { get; set; }

        [Display(Name = "Fecha y Hora")]

        public DateTime Fecha { get; set; }

        public string Banda { get; set; }
    }
}
