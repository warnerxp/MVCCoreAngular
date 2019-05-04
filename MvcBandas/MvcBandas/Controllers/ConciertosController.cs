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
        public async Task<IActionResult> Index()
        {
            var mvcBandasContext = _context.Conciertos.Include(c => c.Banda);
            return View(await mvcBandasContext.ToListAsync());
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

            return View(concierto);
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
            ViewData["BandaId"] = new SelectList(_context.Bandas, "Id", "Nombre", concierto.BandaId);
            return View(concierto);
        }

        // POST: Conciertos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Lugar,BandaId,Fecha")] Concierto concierto)
        {
            if (id != concierto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(concierto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConciertoExists(concierto.Id))
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
            ViewData["BandaId"] = new SelectList(_context.Bandas, "Id", "Nombre", concierto.BandaId);
            return View(concierto);
        }

        // GET: Conciertos/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

            return View(concierto);
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
