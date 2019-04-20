using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcBandas.Models
{
    public class Banda
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Vocalista { get; set; }
        [Display(Name ="Número de Integrantes")]
        public int NumeroIntegrantes { get; set; }
        [Display(Name = "Año de formación")]
        public int anioFormacion { get; set; }
        [Display(Name = "Fecha de registro")]
        public DateTime FechaRegistro { get; set; }
        [Display(Name = "Género Musical")]
        public string Genero { get; set; }
    }
}
