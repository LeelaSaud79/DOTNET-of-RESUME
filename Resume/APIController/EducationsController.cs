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
using Resume.Repositories;
using Resume.DTOs.InfoesDTOs;

namespace Resume.APIController
{
    [APIAuthKey]
    [Route("api/[controller]")]
    [ApiController]
    public class EducationsController : ControllerBase
    {
        private readonly IMapper _mapper;
        //private readonly ResumeContext _context;
        private readonly IGenericRepos _iGenericRepos;

        public EducationsController(IMapper mapper, IGenericRepos iGenericRepos)
        {
            _mapper = mapper;
            //_context = context;
            _iGenericRepos = iGenericRepos;
        }

        // GET: api/Educations
        [HttpGet]
        public async Task<ActionResult<List<EducationsReadDTOs>>> GetEducation()
        {
            //  var info = await _context.Education.ToListAsync();
            //  if (info == null)
            //{
            //    return NotFound();
            //}
            //  var records = _mapper.Map<List<EducationsReadDTOs>>(info);
            //  return records;
            var educations = await _iGenericRepos.GetAll<Educations>();
            var returnAll = _mapper.Map<List<EducationsReadDTOs>>(educations);
            return Ok(returnAll);
        }

        // GET: api/Educations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<EducationsReadDTOs>>> GetEducations(int id)
        {
        
            var info = await _iGenericRepos.GetByUserId<Educations>(userData => userData.info_id == id);

            if (info == null)
            {
                return NotFound();
            }
            var returnuser = _mapper.Map<List<EducationsReadDTOs>>(info);
            return Ok(returnuser);
        }


        // PUT: api/Educations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEducations(int id, EducationsUpdateDTOs educationsUpdateDTOs)
        {
            var info = await _iGenericRepos.GetById<Educations>(id);
            if (id != educationsUpdateDTOs.eid)
            {
                return BadRequest();
            }
            if (info == null)
            {
                throw new Exception($"Information {id} is not found");
            }
            _mapper.Map(educationsUpdateDTOs, info);
            //_context.Education.Update(info);
            //_context.Educations.Update(info);
            info = await _iGenericRepos.Update<Educations>(id, info);

            var infoReadDTO = _mapper.Map<EducationsUpdateDTOs>(info);
            return Ok(infoReadDTO);
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

            await _iGenericRepos.Create<Educations>(eduCreateDTO);
            //_context.Education.Add(eduCreateDTO);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetEducations", new { id = eduCreateDTO.eid }, eduCreateDTO);
            return Ok("Created");
        }

        // DELETE: api/Educations/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteEducations(int id)
        //{
        //    var inform = await _iGenericRepos.Delete<Info>(id);
        //    if (inform == null)
        //    {
        //        return NotFound();
        //    }
        //    return BadRequest();
        //}

        //private bool EducationsExists(int id)
        //{
        //    return (_context.Education?.Any(e => e.eid == id)).GetValueOrDefault();
        //}


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEducations(int id)
        {
            var education = await _iGenericRepos.Delete<Educations>(id);
            if (education == null)
            {
                return NotFound();
            }
            return Ok("Deleted");
        }

        //private bool EducationsExists(int id)
        //{
        //    return (_context.Education?.Any(e => e.eid == id)).GetValueOrDefault();
        //}
    }
}
