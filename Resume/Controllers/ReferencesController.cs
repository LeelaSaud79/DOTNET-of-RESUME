using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Resume.Data;
using Resume.Models;

namespace Resume.Controllers
{
    public class ReferencesController : Controller
    {
        private readonly ResumeContext _context;

        public ReferencesController(ResumeContext context)
        {
            _context = context;
        }

        // GET: References
        public async Task<IActionResult> Index()
        {
              return _context.Reference != null ? 
                          View(await _context.Reference.ToListAsync()) :
                          Problem("Entity set 'ResumeContext.Reference'  is null.");
        }

        // GET: References/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reference == null)
            {
                return NotFound();
            }

            var references = await _context.Reference
                .FirstOrDefaultAsync(m => m.ref_id == id);
            if (references == null)
            {
                return NotFound();
            }

            return View(references);
        }

        // GET: References/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: References/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ref_id,info_id,reference_by,company,email,phone_number,designation")] References references)
        {
            if (ModelState.IsValid)
            {
                _context.Add(references);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(references);
        }

        // GET: References/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reference == null)
            {
                return NotFound();
            }

            var references = await _context.Reference.FindAsync(id);
            if (references == null)
            {
                return NotFound();
            }
            return View(references);
        }

        // POST: References/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ref_id,info_id,reference_by,company,email,phone_number,designation")] References references)
        {
            if (id != references.ref_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(references);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReferencesExists(references.ref_id))
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
            return View(references);
        }

        // GET: References/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reference == null)
            {
                return NotFound();
            }

            var references = await _context.Reference
                .FirstOrDefaultAsync(m => m.ref_id == id);
            if (references == null)
            {
                return NotFound();
            }

            return View(references);
        }

        // POST: References/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reference == null)
            {
                return Problem("Entity set 'ResumeContext.Reference'  is null.");
            }
            var references = await _context.Reference.FindAsync(id);
            if (references != null)
            {
                _context.Reference.Remove(references);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReferencesExists(int id)
        {
          return (_context.Reference?.Any(e => e.ref_id == id)).GetValueOrDefault();
        }
    }
}
