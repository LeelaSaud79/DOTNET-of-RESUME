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
    public class ExperiencesController : ControllerBase
    {
        private readonly ResumeContext _context;

        public ExperiencesController(ResumeContext context)
        {
            _context = context;
        }

        // GET: api/Experiences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Experience>>> GetExperience(int id)

        {
          if (_context.Experience == null)
          {
              return NotFound();
          }
            var experience = await _context.Experience.Where(r => r.info_id == id).ToListAsync();

            if (_context.Experience == null)
            {
                return NotFound();
            }

            return experience;
        }

    }
}
