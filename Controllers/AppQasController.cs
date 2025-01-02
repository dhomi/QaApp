using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QaApp.Data;
using Microsoft.AspNetCore.Authorization;

namespace QaApp.Controllers
{
    public class AppQasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppQasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AppQas
        public async Task<IActionResult> Index()
        {
            return View(await _context.AppQa.ToListAsync());
        }

        // GET: AppQas/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // GET: AppQas/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index", await _context.AppQa.Where(j => j.AppVraag.Contains(SearchPhrase)).ToListAsync());
        }

        // GET: AppQas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appQa = await _context.AppQa
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appQa == null)
            {
                return NotFound();
            }

            return View(appQa);
        }

        // GET: AppQas/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: AppQas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AppVraag,AppAntwoord")] AppQa appQa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appQa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appQa);
        }

        // GET: AppQas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appQa = await _context.AppQa.FindAsync(id);
            if (appQa == null)
            {
                return NotFound();
            }
            return View(appQa);
        }

        // POST: AppQas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AppVraag,AppAntwoord")] AppQa appQa)
        {
            if (id != appQa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appQa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppQaExists(appQa.Id))
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
            return View(appQa);
        }

        // GET: AppQas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appQa = await _context.AppQa
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appQa == null)
            {
                return NotFound();
            }

            return View(appQa);
        }

        // POST: AppQas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appQa = await _context.AppQa.FindAsync(id);
            if (appQa != null)
            {
                _context.AppQa.Remove(appQa);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppQaExists(int id)
        {
            return _context.AppQa.Any(e => e.Id == id);
        }
    }
}
