using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using checking_v2.Data;
using checking_v2.Models;

namespace checking_v2.Controllers
{
    public class RespuestasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RespuestasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Respuestas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Respuestas.Include(r => r.Solicitud);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Respuestas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var respuesta = await _context.Respuestas
                .Include(r => r.Solicitud)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (respuesta == null)
            {
                return NotFound();
            }

            return View(respuesta);
        }

        // GET: Respuestas/Create
        public IActionResult Create()
        {
            ViewData["SolicitudId"] = new SelectList(_context.Solicitudes, "Id", "Contenido");
            return View();
        }

        // POST: Respuestas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Contenido,FechaHora,SolicitudId")] Respuesta respuesta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(respuesta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SolicitudId"] = new SelectList(_context.Solicitudes, "Id", "Contenido", respuesta.SolicitudId);
            return View(respuesta);
        }

        // GET: Respuestas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var respuesta = await _context.Respuestas.FindAsync(id);
            if (respuesta == null)
            {
                return NotFound();
            }
            ViewData["SolicitudId"] = new SelectList(_context.Solicitudes, "Id", "Contenido", respuesta.SolicitudId);
            return View(respuesta);
        }

        // POST: Respuestas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Contenido,FechaHora,SolicitudId")] Respuesta respuesta)
        {
            if (id != respuesta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(respuesta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RespuestaExists(respuesta.Id))
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
            ViewData["SolicitudId"] = new SelectList(_context.Solicitudes, "Id", "Contenido", respuesta.SolicitudId);
            return View(respuesta);
        }

        // GET: Respuestas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var respuesta = await _context.Respuestas
                .Include(r => r.Solicitud)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (respuesta == null)
            {
                return NotFound();
            }

            return View(respuesta);
        }

        // POST: Respuestas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var respuesta = await _context.Respuestas.FindAsync(id);
            _context.Respuestas.Remove(respuesta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RespuestaExists(int id)
        {
            return _context.Respuestas.Any(e => e.Id == id);
        }
    }
}
