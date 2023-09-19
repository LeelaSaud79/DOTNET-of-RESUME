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
using Resume.Repositories;
namespace Resume.APIController
{
    [APIAuthKey]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ResumeContext _context;
        private readonly IMapper _mapper;
        private readonly IGenericRepos _iGenericRepos;

        public ProjectsController(ResumeContext context, IMapper mapper, IGenericRepos iGenericRepos)
        {
            _context = context;
            _mapper = mapper;
            _iGenericRepos = iGenericRepos;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<List<ProjectsReadDTOs>>> GetProjects()
        {
            var proj = await _iGenericRepos.GetAll<Project>();
          
          
            var records = _mapper.Map<List<ProjectsReadDTOs>>(proj);
            return records;
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectsReadDTOs>> GetProject(int id)
        {
            var info = await _iGenericRepos.GetByUserId<Project>(userData => userData.info_id == id);
            if (info == null)
          {
              return NotFound();
          }
            var returnRead = _mapper.Map<List<ProjectsReadDTOs>>(info);

            return Ok(returnRead);
        }
        




        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, ProjectsUpdateDTOs projectsUpdateDTOs)
        {
            var info = await _iGenericRepos.GetById<Project>(id);


            if (id != projectsUpdateDTOs.pid)

            {
                return BadRequest();
            }
            if (info == null)
            {
                throw new Exception($"Information {id} is not found");
            }
            _mapper.Map(projectsUpdateDTOs, info);
            _context.Projects.Update(info);
            //_context.Educations.Update(info);
            info = await _iGenericRepos.Update<Project>(id, info);

            var infoReadDTO = _mapper.Map<ProjectsUpdateDTOs>(info);
            return Ok(infoReadDTO);
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
            var projCreateDTO = _mapper.Map<Project>(project);

         
            await _iGenericRepos.Create<Project>(projCreateDTO);
            _context.Projects.Add(projCreateDTO);

            return CreatedAtAction("GetProject", new { id = projCreateDTO.pid }, projCreateDTO);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _iGenericRepos.Delete<Project>(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok("Deleted");
        }

        private bool ProjectExists(int id)
        {
            return (_context.Projects?.Any(e => e.pid == id)).GetValueOrDefault();
        }
    }
}
