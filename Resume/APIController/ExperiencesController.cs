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
using Resume.Repositories;
using Resume.DTOs.InfoesDTOs;
using Resume.DTOs.CertificationsDTOs;

namespace Resume.APIController
{
    [APIAuthKey]
    [Route("api/[controller]")]
    [ApiController]
    public class ExperiencesController : ControllerBase
    {
        private readonly ResumeContext _context;
        private readonly IMapper _mapper;
        private readonly IGenericRepos _genericRepos;
        public ExperiencesController(ResumeContext context, IMapper mapper, IGenericRepos genericRepos)
        {
            _context = context;
            _mapper = mapper;
            _genericRepos = genericRepos;
        }

        // GET: api/Experiences
        [HttpGet]
        public async Task<ActionResult<List<ExperiencesReadDTOs>>> GetExperience()
        {

            var infos = await _genericRepos.GetAll<Experience>();
            var retunInfos = _mapper.Map<List<ExperiencesReadDTOs>>(infos);
            return Ok(retunInfos);
        }
        


        // GET: api/Experiences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExperiencesReadDTOs>> GetExperience(int id)
        {
            var info = await _genericRepos.GetByUserId<Experience>(userData => userData.info_id == id);
            if (info == null) 
          {
              return NotFound();
          }
           
            var returnRead = _mapper.Map<List<ExperiencesReadDTOs>>(info);

            return Ok( returnRead);
        }

       


        // PUT: api/Experiences/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult>PutExperience(int id, ExperiencesUpdateDTOs experiencesUpdateDTOs)
        {
            var info = await _genericRepos.GetById<Experience>(id);
            if (id != experiencesUpdateDTOs.id)
            {
                return BadRequest();
            }

            if (info == null)
            {
                throw new Exception($"Experience{id}is not found");

            }
            _mapper.Map(experiencesUpdateDTOs, info);
            _context.Experience.Update(info);
            info = await _genericRepos.Update<Experience>(id, info);

            var expReadDTO = _mapper.Map<ExperiencesUpdateDTOs>(info);
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


            await _genericRepos.Create<Experience>(expCreateDTO);

            return CreatedAtAction("GetExperience", new { id = expCreateDTO.id }, expCreateDTO);
        }
        
        

        // DELETE: api/Experiences/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExperiences(int id)
        {
            var exp = await _genericRepos.Delete<Experience>(id);
            if (exp == null)
            {
                return NotFound();
            }
            return Ok("Deleted");
        }

        private bool ExperienceExists(int id)
        {
            return (_context.Experience?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
