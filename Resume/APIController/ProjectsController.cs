using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resume.Data;
using AutoMapper;
using Resume.DTOs.ProjectsDTOs;
using Resume.Models;
using Resume.Helpers;
using Resume.DTOs.ReferencesDTOs;
using Resume.DTOs.ExpereincesDTOs;
using Resume.DTOs.EducationsDTOs;
using Resume.DTOs.CertificationsDTOs;

namespace Resume.APIController
{
    [APIAuthKey]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ResumeContext _context;
        private readonly IMapper _mapper;

        public ProjectsController(ResumeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<List<ProjectsReadDTOs>>> GetProjects()
        {
            var proj = await _context.Projects.ToListAsync();
          if (proj == null || proj.Count == 0)
          {
              return NotFound();
          }
            var records = _mapper.Map<List<ProjectsReadDTOs>>(proj);
            return records;
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectsReadDTOs>> GetProject(int id)
        {
          if (_context.Projects == null)
          {
              return NotFound();
          }
            var project = await _context.Projects.Where(c => c.info_id == id).ToListAsync();

            if (project == null)
            {
                return NotFound();
            }
            var returnRead = _mapper.Map<List<ProjectsReadDTOs>>(project);

            return Ok(returnRead);
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, ProjectsUpdateDTOs projectsUpdateDTOs)
        {
            var proj = await _context.Projects.Where(c => c.info_id == id && c.pid == projectsUpdateDTOs.pid).FirstOrDefaultAsync();
            
           
            if (proj == null)
            {
                throw new Exception($"Projects{id} is not found");
            }
            _mapper.Map(projectsUpdateDTOs, proj);
            _context.Projects.Update(proj);
            var projReadDTO = _mapper.Map<ProjectsUpdateDTOs>(proj);
            return Ok(projReadDTO);

        }

        // POST: api/Projects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Project>> PostProjectsCreateDTOs(ProjectsCreateDTOs project)
        {
          if (project == null)
          {
              return BadRequest("Entity set 'ResumeContext.Projects'  is null.");
          }
          var proj = _mapper.Map<Project>(project);
            _context.Projects.Add(proj);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProject", new { id = proj.pid }, proj);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            if (_context.Projects == null)
            {
                return NotFound();
            }
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectExists(int id)
        {
            return (_context.Projects?.Any(e => e.pid == id)).GetValueOrDefault();
        }
    }
}
