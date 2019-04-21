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
        [Required(ErrorMessage = "El nombre de la banda es requerido.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El nombre del vocalista es requerido.")]
        public string Vocalista { get; set; }
        [Display(Name ="Número de Integrantes")]
        [Required(ErrorMessage ="El número de integrantes es requerido.")]
        public int NumeroIntegrantes { get; set; }
        [Display(Name = "Año de formación")]
        [Required(ErrorMessage = "El año de formación es requerido.")]
        public int anioFormacion { get; set; }
        [Display(Name = "Fecha de registro")]
        [Required(ErrorMessage = "La fecha de registro es requerida.")]
        [DataType(DataType.Date)]
        public DateTime FechaRegistro { get; set; }
        [Display(Name = "Género Musical")]
        public string Genero { get; set; }
    }
}
