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
    public class ScholarshipsController : ControllerBase
    {
        private readonly ResumeContext _context;

        public ScholarshipsController(ResumeContext context)
        {
            _context = context;
        }


        // GET: api/Scholarships/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Scholarships>>> GetScholarships(int id)
        {
          if (_context.Scholarship == null)
          {
             return NotFound();
          }
            var scholarships = await _context.Scholarship.Where(r=>r.info_id == id).ToListAsync();
            if (_context.Scholarship == null)
                {
                return NotFound();
                }
            return scholarships;
        }
    }
}

