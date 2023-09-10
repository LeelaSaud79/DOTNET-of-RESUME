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
    public class ScholarshipsController : Controller
    {
        private readonly ResumeContext _context;

        public ScholarshipsController(ResumeContext context)
        {
            _context = context;
        }

        // GET: Scholarships
        public async Task<IActionResult> Index()
        {
              return _context.Scholarship != null ? 
                          View(await _context.Scholarship.ToListAsync()) :
                          Problem("Entity set 'ResumeContext.Scholarship'  is null.");
        }

        // GET: Scholarships/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Scholarship == null)
            {
                return NotFound();
            }

            var scholarship = await _context.Scholarship
                .FirstOrDefaultAsync(m => m.scholar_id == id);
            if (scholarship == null)
            {
                return NotFound();
            }

            return View(scholarship);
        }

        // GET: Scholarships/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Scholarships/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("scholar_id,info_id,scholarship_name,issue,issue_by,scholar_name")] Scholarships scholarship)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scholarship);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(scholarship);
        }

        // GET: Scholarships/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Scholarship == null)
            {
                return NotFound();
            }

            var scholarship = await _context.Scholarship.FindAsync(id);
            if (scholarship == null)
            {
                return NotFound();
            }
            return View(scholarship);
        }

        // POST: Scholarships/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("scholar_id,info_id,scholarship_name,issue,issue_by,scholar_name")] Scholarships scholarship)
        {
            if (id != scholarship.scholar_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scholarship);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScholarshipExists(scholarship.scholar_id))
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
            return View(scholarship);
        }

        // GET: Scholarships/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Scholarship == null)
            {
                return NotFound();
            }

            var scholarship = await _context.Scholarship
                .FirstOrDefaultAsync(m => m.scholar_id == id);
            if (scholarship == null)
            {
                return NotFound();
            }

            return View(scholarship);
        }

        // POST: Scholarships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Scholarship == null)
            {
                return Problem("Entity set 'ResumeContext.Scholarship'  is null.");
            }
            var scholarship = await _context.Scholarship.FindAsync(id);
            if (scholarship != null)
            {
                _context.Scholarship.Remove(scholarship);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScholarshipExists(int id)
        {
          return (_context.Scholarship?.Any(e => e.scholar_id == id)).GetValueOrDefault();
        }
    }
}
