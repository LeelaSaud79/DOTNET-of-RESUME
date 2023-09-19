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
using Resume.DTOs.InfoesDTOs;
using Resume.Repositories;
using Resume.DTOs.EducationsDTOs;
using Resume.DTOs.ProjectsDTOs;

namespace Resume.APIController
{
    [APIAuthKey]
    [Route("api/[controller]")]
    [ApiController]
    public class CertificationsController : ControllerBase
    {
        private readonly ResumeContext _context;
        private readonly IMapper _mapper;
        private readonly IGenericRepos _genericRepos;

        public CertificationsController(ResumeContext context, IMapper mapper, IGenericRepos genericRepos)
        {
            _context = context;
            _mapper = mapper;
            _genericRepos = genericRepos;
        }

        // GET: api/Certifications
        [HttpGet]
        public async Task<ActionResult<List<CertificationsReadDTOs>>> GetCertifications()
        {
            var certification = await _genericRepos.GetAll<Certification>();
            
            var records = _mapper.Map<List<CertificationsReadDTOs>>(certification);
            return Ok(records);
        }

       


        // GET: api/Certifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<CertificationsReadDTOs>>> GetCertification(int id)
        {
            var info = await _genericRepos.GetByUserId<Certification>(userData => userData.info_id == id);
            

            if (info == null)
            {
                return NotFound();
            }
            var returnCet = _mapper.Map<List<CertificationsReadDTOs>>(info);

            return Ok(returnCet);
        }
        



        // PUT: api/Certifications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCertification(int id, CertificationsUpdateDTOs certificationsUpdateDTOs)
        {
            var info = await _genericRepos.GetById<Certification>(id);

            if (info == null)
            {
                return NotFound($"Certification with ID {id} not found.");
            }


            _mapper.Map(certificationsUpdateDTOs, info);
            _context.Certifications.Update(info);
            info = await _genericRepos.Update<Certification>(id, info);

            var certificationReadDTO = _mapper.Map<CertificationsReadDTOs>(info);
            return Ok(certificationReadDTO);
        }


        // POST: api/Certifications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Certification>> PostCertificationsCreateDTOs(CertificationsCreateDTOs cert)
        {
          if (cert == null)
          {
              return BadRequest("Entity set 'ResumeContext.Certifications'  is null.");
          }
          var cerEntity = _mapper.Map<Certification>(cert);
          await _genericRepos.Create<Certification>(cerEntity);
            _context.Certifications.Add(cerEntity);
       

            return CreatedAtAction("GetCertification", new { id = cerEntity.cid }, cerEntity);
        }


        // DELETE: api/Certifications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCertification(int id)
        {
            var certificate = await _genericRepos.Delete<Certification>(id);
            if (certificate == null)
            {
                return NotFound();
            }
            return Ok("Deleted");
        }



        private bool CertificationExists(int id)
        {
            return (_context.Certifications?.Any(e => e.cid == id)).GetValueOrDefault();
        }
    }
}
