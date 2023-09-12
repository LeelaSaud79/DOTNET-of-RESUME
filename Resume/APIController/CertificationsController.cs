using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resume.Data;
using AutoMapper;
using Resume.DTOs.CertificationsDTOs;
using Resume.Models;
using Resume.Helpers;

namespace Resume.APIController
{
    [APIAuthKey]
    [Route("api/[controller]")]
    [ApiController]
    public class CertificationsController : ControllerBase
    {
        private readonly ResumeContext _context;
        private readonly IMapper _mapper;   

        public CertificationsController(ResumeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Certifications
        [HttpGet]
        [HttpGet]
        public async Task<ActionResult<List<CertificationsReadDTOs>>> GetCertifications()
        {
            var certification = await _context.Certifications.ToListAsync();
            if (certification == null || certification.Count == 0)
            {
                return NotFound();
            }
            var records = _mapper.Map<List<CertificationsReadDTOs>>(certification);
            return records;
        }


        // GET: api/Certifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<CertificationsReadDTOs>>> GetCertification(int id)
        {
            if (_context.Certifications == null)
            {
                return NotFound();
            }
            var certification = await _context.Certifications.Where(c => c.info_id == id).ToListAsync();

            if (certification == null)
            {
                return NotFound();
            }
            var returnCet = _mapper.Map<List<CertificationsReadDTOs>>(certification);

            return Ok(returnCet);
        }

        // PUT: api/Certifications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCertification(int id, CertificationsUpdateDTOs certificationsUpdateDTOs)
        {
            var certification = await _context.Certifications.Where(c => c.info_id == id && c.cid == certificationsUpdateDTOs.cid).FirstOrDefaultAsync();


            if (certification == null)
            {
                return NotFound($"Certification with ID {id} not found.");
            }


            _mapper.Map(certificationsUpdateDTOs, certification);
            _context.Certifications.Update(certification);
            await _context.SaveChangesAsync();

            var certificationReadDTO = _mapper.Map<CertificationsReadDTOs>(certification);
            return Ok(certificationReadDTO);
        }


        // POST: api/Certifications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Certification>> PostCertificationsCreateDTOs(CertificationsCreateDTOs certification)
        {
          if (certification == null)
          {
              return BadRequest("Entity set 'ResumeContext.Certifications'  is null.");
          }
          var cerEntity = _mapper.Map<Certification>(certification);
            _context.Certifications.Add(cerEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCertification", new { id = cerEntity.cid }, cerEntity);
        }

        // DELETE: api/Certifications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCertification(int id)
        {
            if (_context.Certifications == null)
            {
                return NotFound();
            }
            var certification = await _context.Certifications.FindAsync(id);
            if (certification == null)
            {
                return NotFound();
            }

            _context.Certifications.Remove(certification);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CertificationExists(int id)
        {
            return (_context.Certifications?.Any(e => e.cid == id)).GetValueOrDefault();
        }
    }
}
