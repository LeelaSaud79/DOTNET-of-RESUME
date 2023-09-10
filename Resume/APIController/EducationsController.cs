using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Resume.DTOs.EducationsDTOs;
using Resume.Data;
using Resume.Models;
using Resume.Helpers;

namespace Resume.APIController
{
    [APIAuthKey]
    [Route("api/[controller]")]
    [ApiController]
    public class EducationsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ResumeContext _context;

        public EducationsController(ResumeContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        // GET: api/Educations
        [HttpGet]
        public async Task<ActionResult<List<EducationsReadDTOs>>> GetEducation()
        {
            var info = await _context.Education.ToListAsync();
            if (info == null)
          {
              return NotFound();
          }
            var records = _mapper.Map<List<EducationsReadDTOs>>(info);
            return records;
        }

        // GET: api/Educations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EducationsReadDTOs>> GetEducations(int id)
        {
            if (!_context.Education.Any())
                //if (_context.Education == null)
          {
              return NotFound();
          }
            var info = await _context.Education.FindAsync(id);

            if (info == null)
            {
                return NotFound();
            }
            var returnuser = _mapper.Map<EducationsReadDTOs>(info);
            return returnuser;
        }

        // PUT: api/Educations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEducations(int id, EducationsUpdateDTOs educationsUpdateDTOs)
        {
            var edu = await _context.Education.FindAsync(id);
            if (id != educationsUpdateDTOs.eid)
            {
                return BadRequest();
            }
            if (edu == null)
            {
                throw new Exception("$Education{id} is not found.");
            }
            _mapper.Map(educationsUpdateDTOs, edu);
            _context.Education.Update(edu);
            await _context.SaveChangesAsync();
            var eduReadDTO = _mapper.Map<EducationsUpdateDTOs>(edu);
            return Ok(eduReadDTO);
        }

            

        // POST: api/Educations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Educations>>PostEducationsCreateDTOs(EducationsCreateDTOs edu)
        {
          if (edu == null)
          {
                return BadRequest("Entity set 'ResumeContext.Educations' is not null.");
          }
            var eduCreateDTO = _mapper.Map<Educations>(edu);


            _context.Education.Add(eduCreateDTO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEducations", new { id = eduCreateDTO.eid }, eduCreateDTO);
        }

        // DELETE: api/Educations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEducations(int id)
        {
            if (_context.Education == null)
            {
                return NotFound();
            }
            var educations = await _context.Education.FindAsync(id);
            if (educations == null)
            {
                return NotFound();
            }

            _context.Education.Remove(educations);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EducationsExists(int id)
        {
            return (_context.Education?.Any(e => e.eid == id)).GetValueOrDefault();
        }
    }
}
