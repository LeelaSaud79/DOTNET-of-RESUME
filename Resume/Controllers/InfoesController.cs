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
    public class InfoesController : Controller
    {
        private readonly ResumeContext _context;

        public InfoesController(ResumeContext context)
        {
            _context = context;
        }

        // GET: Infoes
        public async Task<IActionResult> Index()
        {
            return _context.Info != null ?
                        View(await _context.Info.ToListAsync()) :
                        Problem("Entity set 'ResumeContext.Info'  is null.");
        }

        // GET: Infoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Info == null)
            {
                return NotFound();
            }

            var info = await _context.Info
                .FirstOrDefaultAsync(m => m.info_id == id);
            if (info == null)
            {
                return NotFound();
            }

            return View(info);
        }

        // GET: Infoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Infoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("info_id,name,password,github_link,address,email,phone,social_media_link,summary,designation")] Info info)
        {
            if (ModelState.IsValid)
            {
                _context.Add(info);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(info);
        }

        // GET: Infoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Info == null)
            {
                return NotFound();
            }

            var info = await _context.Info.FindAsync(id);
            if (info == null)
            {
                return NotFound();
            }
            return View(info);
        }

        // POST: Infoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("info_id,name,password,github_link,address,email,phone,social_media_link,summary,designation")] Info info)
        {
            if (id != info.info_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(info);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InfoExists(info.info_id))
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
            return View(info);
        }

        // GET: Infoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Info == null)
            {
                return NotFound();
            }

            var info = await _context.Info
                .FirstOrDefaultAsync(m => m.info_id == id);
            if (info == null)
            {
                return NotFound();
            }

            return View(info);
        }

        // POST: Infoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Info == null)
            {
                return Problem("Entity set 'ResumeContext.Info'  is null.");
            }
            var info = await _context.Info.FindAsync(id);
            if (info != null)
            {
                _context.Info.Remove(info);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InfoExists(int id)
        {
            return (_context.Info?.Any(e => e.info_id == id)).GetValueOrDefault();
        }
    }
}
