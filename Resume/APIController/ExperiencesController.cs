using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Resume.DTOs.ExpereincesDTOs;
using Resume.Data;
using Resume.Models;
using Resume.Helpers;


namespace Resume.APIController
{
    [APIAuthKey]
    [Route("api/[controller]")]
    [ApiController]
    public class ExperiencesController : ControllerBase
    {
        private readonly ResumeContext _context;
        private readonly IMapper _mapper;

        public ExperiencesController(ResumeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Experiences
        [HttpGet]
        public async Task<ActionResult<List<ExperiencesReadDTOs>>> GetExperience()
        {
            var Exp = await _context.Experience.ToListAsync();
          if (Exp == null)
          {
              return NotFound();
          }
            var records = _mapper.Map<List<ExperiencesReadDTOs>>(Exp);
            return records;
        }

        // GET: api/Experiences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExperiencesReadDTOs>> GetExperience(int id)
        {
            if (_context.Experience == null) 
          {
              return NotFound();
          }
            var experience = await _context.Experience.Where(c => c.info_id == id).ToListAsync();

            if (experience == null)
            {
                return NotFound();
            }
            var returnRead = _mapper.Map<List<ExperiencesReadDTOs>>(experience);

            return Ok( returnRead);
        }

        // PUT: api/Experiences/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult>PutExperience(int id, ExperiencesUpdateDTOs experiencesUpdateDTOs)
        {
            var exp = await _context.Experience.Where(c => c.info_id == id && c.id == experiencesUpdateDTOs.id).FirstOrDefaultAsync();
           
            if (exp == null)
            {
                throw new Exception($"Experience{id}is not found");

            }
            _mapper.Map(experiencesUpdateDTOs, exp);
            _context.Experience.Update(exp);
            await _context.SaveChangesAsync();
            var expReadDTO = _mapper.Map<ExperiencesUpdateDTOs>(exp);
            return Ok(expReadDTO);
        }


        // POST: api/Experiences
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Experience>> PostExperiencesCreateDTOs(ExperiencesCreateDTOs exp)
        {
          if (exp == null)
          {
              return BadRequest("Entity set 'ResumeContext.Experience'  is null.");
            }
            var expCreateDTO = _mapper.Map<Experience>(exp);

            _context.Experience.Add(expCreateDTO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExperience", new { id = expCreateDTO.id }, expCreateDTO);
        }

        // DELETE: api/Experiences/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExperience(int id)
        {
            if (_context.Experience == null)
            {
                return NotFound();
            }
            var experience = await _context.Experience.FindAsync(id);
            if (experience == null)
            {
                return NotFound();
            }

            _context.Experience.Remove(experience);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExperienceExists(int id)
        {
            return (_context.Experience?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
