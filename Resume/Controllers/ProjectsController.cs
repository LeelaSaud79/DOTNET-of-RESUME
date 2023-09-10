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
    public class ProjectssController : Controller
    {
        private readonly ResumeContext _context;

        public ProjectssController(ResumeContext context)
        {
            _context = context;
        }

        // GET: Projectss
        public async Task<IActionResult> Index()
        {
              return _context.Projects != null ? 
                          View(await _context.Projects.ToListAsync()) :
                          Problem("Entity set 'ResumeContext.Projects'  is null.");
        }

        // GET: Projectss/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var Projects = await _context.Projects
                .FirstOrDefaultAsync(m => m.pid == id);
            if (Projects == null)
            {
                return NotFound();
            }

            return View(Projects);
        }

        // GET: Projectss/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projectss/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("pid,info_id,Projects_name,Tech_stack,Descritpion,Link")] Project Projects)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Projects);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Projects);
        }

        // GET: Projectss/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var Projects = await _context.Projects.FindAsync(id);
            if (Projects == null)
            {
                return NotFound();
            }
            return View(Projects);
        }

        // POST: Projectss/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("pid,info_id,project_name,Tech_stack,Descritpion,Link")] Project Projects)
        {
            if (id != Projects.pid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Projects);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectsExists(Projects.pid))
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
            return View(Projects);
        }

        // GET: Projectss/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var Projects = await _context.Projects
                .FirstOrDefaultAsync(m => m.pid == id);
            if (Projects == null)
            {
                return NotFound();
            }

            return View(Projects);
        }

        // POST: Projectss/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Projects == null)
            {
                return Problem("Entity set 'ResumeContext.Projects'  is null.");
            }
            var Projects = await _context.Projects.FindAsync(id);
            if (Projects != null)
            {
                _context.Projects.Remove(Projects);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectsExists(int id)
        {
          return (_context.Projects?.Any(e => e.pid == id)).GetValueOrDefault();
        }
    }
}
