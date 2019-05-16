using Microsoft.EntityFrameworkCore;
using MvcBandas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcBandas.Servicios
{
    public class ServicioConciertos
    {
        private MvcBandasContext _context;

        public ServicioConciertos(MvcBandasContext context)
        {
            _context = context;
        }

        public async Task<Concierto>  ObtenerPorId(int id)
        {
            return await _context.Conciertos.FindAsync(id);
        }

        public async Task<Concierto> ObtenerIncluyendoBanda(int id)
        {
            return await _context.Conciertos
                .Include(c => c.Banda)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public IQueryable<Concierto> ObtenerConciertos(string terminoBusqueda = null)
        {
            var conciertos = _context.Conciertos.Include(u => u.Banda).Select(c => c);

            if (!string.IsNullOrEmpty(terminoBusqueda))
            {
                conciertos = conciertos.Where(u => u.Lugar.Contains(terminoBusqueda) || u.Banda.Nombre.Contains(terminoBusqueda));
            }

            return conciertos;
        }
    }
}
