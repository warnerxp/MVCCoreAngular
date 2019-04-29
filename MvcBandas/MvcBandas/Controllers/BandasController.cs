using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcBandas.Models;

namespace MvcBandas.Controllers
{
    public class BandasController : Controller
    {
        private readonly MvcBandasContext _context;

        public BandasController(MvcBandasContext context)
        {
            _context = context;
        }

        // GET: Bandas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bandas.ToListAsync());
        }

        // GET: Bandas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banda = await _context.Bandas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (banda == null)
            {
                return NotFound();
            }

            return View(banda);
        }

        // GET: Bandas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bandas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Vocalista,NumeroIntegrantes,anioFormacion,FechaRegistro,Genero")] Banda banda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(banda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(banda);
        }

        // GET: Bandas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banda = await _context.Bandas.FindAsync(id);
            if (banda == null)
            {
                return NotFound();
            }
            return View(banda);
        }

        // POST: Bandas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Vocalista,NumeroIntegrantes,anioFormacion,FechaRegistro,Genero")] Banda banda)
        {
            if (id != banda.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(banda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BandaExists(banda.Id))
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
            return View(banda);
        }

        // GET: Bandas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banda = await _context.Bandas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (banda == null)
            {
                return NotFound();
            }

            return View(banda);
        }

        // POST: Bandas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var banda = await _context.Bandas.FindAsync(id);
            _context.Bandas.Remove(banda);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BandaExists(int id)
        {
            return _context.Bandas.Any(e => e.Id == id);
        }
    }
}
