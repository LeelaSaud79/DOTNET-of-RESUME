using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Resume.DTOs.ReferencesDTOs;
using Resume.Data;
using Resume.Models;
using Resume.Helpers;
using Resume.DTOs.ProjectsDTOs;
using Resume.Repositories;
using Resume.DTOs.EducationsDTOs;

namespace Resume.APIController
{
    [APIAuthKey]
    [Route("api/[controller]")]
    [ApiController]
    public class ReferencesController : ControllerBase
    {
        private readonly ResumeContext _context;
        private readonly IMapper _mapper;
        private readonly IGenericRepos _iGenericRepos;

        public ReferencesController(ResumeContext context, IMapper mapper, IGenericRepos iGenericRepos)
        {
            _context = context;
            _mapper = mapper;
            _iGenericRepos = iGenericRepos;
        }

        // GET: api/References
        [HttpGet]
        public async Task<ActionResult<List<ReferencesReadDTOs>>> GetReference()
        {
            var references = await _iGenericRepos.GetAll<References>();

            var records = _mapper.Map<List<ReferencesReadDTOs>>(references);

            return records;
        }


        // GET: api/References/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReferencesReadDTOs>> GetReferences(int id)
        {
            var info = await _iGenericRepos.GetByUserId<References>(userData => userData.info_id == id);
            if (info == null)
            {
                return NotFound();
            }
            
            var returnRef = _mapper.Map<List<ReferencesReadDTOs>>(info);

            return Ok(returnRef);
        }
       



        // PUT: api/References/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReferences(int id, ReferencesUpdateDTOs referencesUpdateDTOs)
        {
            var info = await _iGenericRepos.GetById<References>(id);

            if (id != referencesUpdateDTOs.ref_id)
                {
                return BadRequest();
            }
            if(info  == null)
            {
                throw new Exception($"Reference {id} is not found");
            }

            _mapper.Map(referencesUpdateDTOs, info);
            _context.Reference.Update(info);

            info = await _iGenericRepos.Update<References>(id, info);
            var refReadDTO = _mapper.Map<ReferencesUpdateDTOs>(info);
            return Ok(refReadDTO);
        }
       
        

            

        // POST: api/References
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<References>> PostReferencesCreateDTOs(ReferencesCreateDTOs references)
        {
          if (references == null)
          {
              return BadRequest("Entity set 'ResumeContext.Reference'  is null.");
          }
      
            var refEntity = _mapper.Map<References>(references);
            await _iGenericRepos.Create<References>(refEntity);
            _context.Reference.Add(refEntity);
           

            return CreatedAtAction("GetReferences", new { id = refEntity.ref_id }, refEntity);
        }

        // DELETE: api/References/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReferences(int id)
        {
            var inform = await _iGenericRepos.Delete<References>(id);
            if (inform == null)
            {
                return NotFound();
            }
          
            return BadRequest();
        }

        private bool ReferencesExists(int id)
        {
            return (_context.Reference?.Any(e => e.ref_id == id)).GetValueOrDefault();
        }
    }
}
