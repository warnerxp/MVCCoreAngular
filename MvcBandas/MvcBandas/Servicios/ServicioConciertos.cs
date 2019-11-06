using Microsoft.EntityFrameworkCore;
using MvcBandas.Models;
using MvcBandas.ViewModels;
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

        public ConciertoCreateViewModel CrearConcierto(Concierto concierto)
        {
            ConciertoCreateViewModel vm = new ConciertoCreateViewModel
            {
                Id = concierto.Id,
                Fecha = concierto.Fecha,
                BandaId = concierto.BandaId,
                Lugar = concierto.Lugar,
                Banda = concierto.Banda.Nombre
            };

            return vm;
        }

        public Concierto CrearConcierto(ConciertoCreateViewModel vm)
        {
            return new Concierto
            {
                BandaId = vm.BandaId,
                Fecha = vm.Fecha,
                Lugar = vm.Lugar
            };

        }

        public async Task<Concierto>  CrearConcierto(ConciertoCreateViewModel vm, int id)
        {
            Concierto concierto =  await ObtenerPorId(id);

            concierto.BandaId = vm.BandaId;
            concierto.Fecha = vm.Fecha;
            concierto.Lugar = vm.Lugar;
            return concierto;
        }

        public ConciertoDetailsViewModel CrearConciertoDetailsViewModel(Concierto concierto)
        {
            ConciertoDetailsViewModel vm = new ConciertoDetailsViewModel
            {
                Id = concierto.Id,
                Banda = concierto.Banda.Nombre,
                Fecha = concierto.Fecha,
                Lugar = concierto.Lugar
            };

            return vm;
        }

        public ConciertoDeleteViewModel CrearConciertoDeleteViewModel(Concierto concierto)
        {
            ConciertoDeleteViewModel vm = new ConciertoDeleteViewModel
            {
                Id = concierto.Id,
                Banda = concierto.Banda.Nombre,
                Fecha = concierto.Fecha.ToString(),
                Lugar = concierto.Lugar

            };

            return vm;
        }
    }
}
