using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resume.Data;
using Resume.Helpers;
using Resume.Models;

namespace Resume.APIController
{
    [APIAuthKey]
    [Route("api/[controller]")]
    [ApiController]
    public class CertificationsController : ControllerBase
    {
        private readonly ResumeContext _context;

        public CertificationsController(ResumeContext context)
        {
            _context = context;
        }

        // GET: api/Certifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Certification>>> GetCertification(int id)
        {
          if (_context.Certifications == null)
          {
              return NotFound();
          }
            var certification = await _context.Certifications.Where(r => r.info_id == id).ToListAsync();


            if (_context.Certifications == null)
            {
                return NotFound();
            }

            return certification;
        }

        
    }
}
