using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resume.Data;
using Resume.Helpers;
using Resume.Models;

namespace Resume.APIController
{
    [APIAuthKey]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ResumeContext _context;

        public ProjectsController(ResumeContext context)
        {
            _context = context;
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Project>>> GetProject(int id)
        {
          if (_context.Projects == null)
          {
              return NotFound();
          }
            var project = await _context.Projects.Where(r => r.info_id == id).ToListAsync();

            if(_context.Projects == null)
          {
                return NotFound();
            }

            return project;
        }

        
    }
}
