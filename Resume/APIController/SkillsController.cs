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
using Resume.Repositories;
using Resume.Helpers;
using Resume.DTOs.ExpereincesDTOs;

namespace Resume.APIController
{
    [APIAuthKey]
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ResumeContext _context;
        private readonly IMapper _mapper;
        private readonly IGenericRepos _iGenericRepos;

        public SkillsController(ResumeContext context, IMapper mapper, IGenericRepos iGenericRepos)
        {
            _mapper = mapper;
            _context = context;
            _iGenericRepos = iGenericRepos;
        }

        // GET: api/Skills
        [HttpGet]
        public async Task<ActionResult<List<SkillsReadDTOs>>> GetSkills()
        {
            var info = await _iGenericRepos.GetAll<Skills>();
          
          var skillReadDTO = _mapper.Map<List<SkillsReadDTOs>>(info);
            return Ok(skillReadDTO);
        }


        // GET: api/Skills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkillsReadDTOs>> GetSkills(int id)
        {
            var info = await _iGenericRepos.GetByUserId<Skills>(userData => userData.info_id == id);
            if (info == null)
            {
                return NotFound();
            }

            var returnRead = _mapper.Map<List<SkillsReadDTOs>>(info);

            return Ok(returnRead);
        }


        // PUT: api/Skills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkills(int id, SkillsUpdateDTOs skillsUpdateDTOs)
        {
            var info = await _iGenericRepos.GetById<Skills>(id);
            if (id != skillsUpdateDTOs.skill_id)
            {
                return BadRequest();
            }

            if (info == null)
            {
                throw new Exception($"Experience{id}is not found");

            }
            _mapper.Map(skillsUpdateDTOs, info);
            _context.Skills.Update(info);
            info = await _iGenericRepos.Update<Skills>(id, info);

            var expReadDTO = _mapper.Map<SkillsUpdateDTOs>(info);
            return Ok(expReadDTO);



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
            await _iGenericRepos.Create<Skills>(skillEntity);
            return CreatedAtAction("GetSkills", new { id = skillEntity.skill_id }, skillEntity);
        }

        // DELETE: api/Skills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkills(int id)
        {
            var inform = await _iGenericRepos.Delete<Skills>(id);
            if (inform == null)
            {
                return NotFound();
            }
            return BadRequest();
        }

        private bool SkillsExists(int id)
        {
            return (_context.Skills?.Any(e => e.skill_id == id)).GetValueOrDefault();
        }
    }
}
