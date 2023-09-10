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

namespace Resume.APIController
{
    [APIAuthKey]
    [Route("api/[controller]")]
    [ApiController]
    public class ReferencesController : ControllerBase
    {
        private readonly ResumeContext _context;
        private readonly IMapper _mapper;

        public ReferencesController(ResumeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/References
        [HttpGet]
        public async Task<ActionResult<List<ReferencesReadDTOs>>> GetReference()
        {
            var references = await _context.Reference.ToListAsync();

            if (references == null || references.Count == 0)
            {
                return NotFound();
            }

            var records = _mapper.Map<List<ReferencesReadDTOs>>(references);

            return records;
        }


        // GET: api/References/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReferencesReadDTOs>> GetReferences(int id)
        {
            if (_context.Reference == null)
            {
                return NotFound();
            }
            var references = await _context.Reference.FindAsync(id);

            if (references == null)
            {
                return NotFound();
            }
            var returnRef = _mapper.Map<ReferencesReadDTOs>(references);

            return returnRef;
        }

        // PUT: api/References/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReferences(int id, ReferencesUpdateDTOs referencesUpdateDTOs)
        {
            var Ref = await _context.Reference.FindAsync(id);
            if (id != referencesUpdateDTOs.ref_id)
            {
                return BadRequest();
            }
            if (Ref == null)
            {
                throw new Exception($"Reference {id} is not found");
            }

            _mapper.Map(referencesUpdateDTOs, Ref);
            _context.Reference.Update(Ref);
            await _context.SaveChangesAsync();

            var refReadDTO = _mapper.Map<ReferencesUpdateDTOs>(Ref);
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
            _context.Reference.Add(refEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReferences", new { id = refEntity.ref_id }, refEntity);
        }

        // DELETE: api/References/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReferences(int id)
        {
            if (_context.Reference == null)
            {
                return NotFound();
            }
            var references = await _context.Reference.FindAsync(id);
            if (references == null)
            {
                return NotFound();
            }

            _context.Reference.Remove(references);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReferencesExists(int id)
        {
            return (_context.Reference?.Any(e => e.ref_id == id)).GetValueOrDefault();
        }
    }
}
