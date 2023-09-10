using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resume.Data;
using AutoMapper;
using Resume.DTOs.SkillsDTOs;
using Resume.Models;
using Resume.Helpers;

namespace Resume.APIController
{
    [APIAuthKey]
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ResumeContext _context;
        private readonly IMapper _mapper;

        public SkillsController(ResumeContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        // GET: api/Skills
        [HttpGet]
        public async Task<ActionResult<List<SkillsReadDTOs>>> GetSkills()
        {
            var info = await _context.Skills.ToListAsync();
          if ( info == null || info.Count == 0)
          {
              return NotFound();
          }
          var skillReadDTO = _mapper.Map<List<SkillsReadDTOs>>(info);
            return skillReadDTO;
        }

        // GET: api/Skills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkillsReadDTOs>> GetSkills(int id)
        {
          if (_context.Skills == null)
          {
              return NotFound();
          }
            var skills = await _context.Skills.FindAsync(id);

            if (skills == null)
            {
                return NotFound();
            }
            var returnuser = _mapper.Map<SkillsReadDTOs>(skills);

            return returnuser;
        }

        // PUT: api/Skills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkills(int id, SkillsUpdateDTOs skillsUpdateDTOs)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (id != skillsUpdateDTOs.skill_id)
            {
                return BadRequest();
            }
            if (skill == null)
            {
                throw new Exception($"Skills{id} is not found");
            }
            _mapper.Map(skillsUpdateDTOs, skill);
            _context.Skills.Update(skill);
            await _context.SaveChangesAsync();
            var skillUpdate = _mapper.Map<SkillsUpdateDTOs, Skills>(skillsUpdateDTOs, skill);
                return Ok(skillUpdate);

            

        }



        // POST: api/Skills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Skills>> PostSkillsCreateDTOs(SkillsCreateDTOs skill)
        {
          if (skill == null)
          {
              return BadRequest("Entity set 'ResumeContext.Skills'  is null.");
          }
            var skillEntity = _mapper.Map<Skills>(skill);
            _context.Skills.Add(skillEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkills", new { id = skillEntity.skill_id }, skillEntity);
        }

        // DELETE: api/Skills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkills(int id)
        {
            if (_context.Skills == null)
            {
                return NotFound();
            }
            var skills = await _context.Skills.FindAsync(id);
            if (skills == null)
            {
                return NotFound();
            }

            _context.Skills.Remove(skills);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SkillsExists(int id)
        {
            return (_context.Skills?.Any(e => e.skill_id == id)).GetValueOrDefault();
        }
    }
}
