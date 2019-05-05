using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcBandas.Models;
using MvcBandas.ViewModels;
using X.PagedList;

namespace MvcBandas.Controllers
{
    [Authorize]
    public class ConciertosController : Controller
    {
        private readonly MvcBandasContext _context;

        public ConciertosController(MvcBandasContext context)
        {
            _context = context;
        }

        // GET: Conciertos
        public async Task<IActionResult> Index(ListadoViewModel<Concierto> modelo)
        {

            var conciertos = _context.Conciertos.Include(u => u.Banda).Select(c => c);

            if (!string.IsNullOrEmpty(modelo.TerminoBusqueda))
            {
                conciertos = conciertos.Where(u => u.Lugar.Contains(modelo.TerminoBusqueda) || u.Banda.Nombre.Contains(modelo.TerminoBusqueda));
            }

            var numeroPagina = modelo.Pagina ?? 1;
            var registros = await conciertos.ToPagedListAsync(numeroPagina, 5);
            modelo.Registros = registros;
         
            return View(modelo);
        }

        // GET: Conciertos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var concierto = await _context.Conciertos
                .Include(c => c.Banda)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (concierto == null)
            {
                return NotFound();
            }

            ConciertoDetailsViewModel vm = new ConciertoDetailsViewModel
            {
                Id =concierto.Id,
                Banda = concierto.Banda.Nombre,
                Fecha = concierto.Fecha,
                Lugar = concierto.Lugar
            };
            return View(vm);
        }

        // GET: Conciertos/Create
        public IActionResult Create()
        {
            ConciertoCreateViewModel vm = new ConciertoCreateViewModel();
            vm.Fecha = DateTime.Now;
            vm.Bandas = ObtenerListaBandas();
           // ViewData["BandaId"] = new SelectList(_context.Bandas, "Id", "Nombre");
            return View(vm);
        }

        // POST: Conciertos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Lugar,BandaId,Fecha")] ConciertoCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Concierto concierto = new Concierto {

                    BandaId = vm.BandaId,
                    Fecha = vm.Fecha,
                    Lugar = vm.Lugar
                };
                _context.Add(concierto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.Bandas = ObtenerListaBandas();
            return View(vm);
        }

        // GET: Conciertos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concierto = await _context.Conciertos
                .Include(c => c.Banda)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (concierto == null)
            {
                return NotFound();
            }

            ConciertoCreateViewModel vm = new ConciertoCreateViewModel();
            vm.Id = concierto.Id;
            vm.Fecha = concierto.Fecha;
            vm.BandaId = concierto.BandaId;
            vm.Lugar = concierto.Lugar;
            vm.Bandas = ObtenerListaBandas();
            vm.Banda = concierto.Banda.Nombre;
            return View(vm);

        }

        // POST: Conciertos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Lugar,BandaId,Fecha")] ConciertoCreateViewModel vm)
        {
            if (id != vm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var conciertoBd = await _context.Conciertos.FindAsync(vm.Id);
                    conciertoBd.BandaId = vm.BandaId;
                    conciertoBd.Fecha = vm.Fecha;
                    conciertoBd.Lugar = vm.Lugar;

                    _context.Update(conciertoBd);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConciertoExists(vm.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            vm.Bandas = ObtenerListaBandas();
            return View(vm);
        }

        // GET: Conciertos/Delete/5
        public async Task<IActionResult>    Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concierto = await _context.Conciertos
                .Include(c => c.Banda)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (concierto == null)
            {
                return NotFound();
            }

            ConciertoDeleteViewModel vm = new ConciertoDeleteViewModel
            {
                Id = concierto.Id,
                Banda = concierto.Banda.Nombre,
                Fecha = concierto.Fecha.ToString(),
                Lugar = concierto.Lugar

            };

            return View(vm);
        }

        // POST: Conciertos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var concierto = await _context.Conciertos.FindAsync(id);
            _context.Conciertos.Remove(concierto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConciertoExists(int id)
        {
            return _context.Conciertos.Any(e => e.Id == id);
        }

        private List<SelectListItem> ObtenerListaBandas()
        {
            ConciertoCreateViewModel vm = new ConciertoCreateViewModel();
            vm.Fecha = DateTime.Now;
            return vm.Bandas = _context.Bandas.OrderBy(u => u.Nombre)
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.Nombre
                }).ToList();
        }
    }
}
