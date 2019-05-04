using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcBandas.ViewModels
{
    public class ConciertoCreateViewModel
    {

        public int Id { get; set; }
        [Display(Name = "Ubicación del concierto")]
        [Required(ErrorMessage = "El lugar es requerido")]
        public string Lugar { get; set; }
        [Display(Name = "La banda es requerida")]
        [Required(ErrorMessage = "La banda es requerida")]
        public int BandaId { get; set; }
        
        [Display(Name = "Fecha y Hora")]
        [Required(ErrorMessage = "La fecha es requerida")]
        public DateTime Fecha { get; set; }

        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Bandas { get; set; }
    }
}
