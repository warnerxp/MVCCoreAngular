using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcBandas.Models;
using MvcBandas.Servicios;
using MvcBandas.ViewModels;
using X.PagedList;

namespace MvcBandas.Controllers
{
    [Authorize]
    public class ConciertosController : Controller
    {
        private readonly MvcBandasContext _context;
        private readonly ServicioConciertos _servicioConciertos;

        public ConciertosController(MvcBandasContext context,Servicios.ServicioConciertos servicioConciertos)
        {
            _context = context;
            _servicioConciertos = servicioConciertos;
        }

        // GET: Conciertos
        public async Task<IActionResult> Index(ListadoViewModel<Concierto> modelo)
        {

            var conciertos = _servicioConciertos.ObtenerConciertos(modelo.TerminoBusqueda);

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

            var concierto = await _servicioConciertos.ObtenerIncluyendoBanda(id.Value);
            if (concierto == null)
            {
                return NotFound();
            }

            ConciertoDetailsViewModel vm = _servicioConciertos.CrearConciertoDetailsViewModel(concierto);
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
                Concierto concierto = _servicioConciertos.CrearConcierto(vm);
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

            var concierto = await _servicioConciertos.ObtenerIncluyendoBanda(id.Value);
            if (concierto == null)
            {
                return NotFound();
            }

            ConciertoCreateViewModel vm = _servicioConciertos.CrearConcierto(concierto);
            vm.Bandas = ObtenerListaBandas();
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
                    var conciertoBd = await _servicioConciertos.CrearConcierto(vm,id);
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var concierto = await _servicioConciertos.ObtenerIncluyendoBanda(id.Value);
            if (concierto == null)
            {
                return NotFound();
            }
            ConciertoDeleteViewModel vm = _servicioConciertos.CrearConciertoDeleteViewModel(concierto);
            return View(vm);
        }

        // POST: Conciertos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var concierto = await _servicioConciertos.ObtenerPorId(id);
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
