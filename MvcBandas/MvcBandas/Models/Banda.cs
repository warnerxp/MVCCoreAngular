using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcBandas.Models
{
    public class Banda
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Vocalista { get; set; }
        public int NumeroIntegrantes { get; set; }
        public int anioFormacion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Genero { get; set; }
    }
}
