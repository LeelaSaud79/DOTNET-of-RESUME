using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resume.Data;
using Resume.DTOs.InfoesDTOs;
using Resume.Helpers;
using Resume.Models;
namespace Resume.APIController
{
    [APIAuthKey]
    [Route("api/[controller]")]
    [ApiController]
    public class InfoesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ResumeContext _context;

        public InfoesController(IMapper mapper, ResumeContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        // GET: api/Infoes
        [HttpGet]
        public async Task<ActionResult<List<InfoesReadDTOs>>> GetInfo()
        {
            var info = await _context.Info.ToListAsync();

            if (info == null || info.Count == 0)
            {
                return NotFound();
            }

            var records = _mapper.Map<List<InfoesReadDTOs>>(info);
            return records;
        }



        // GET: api/Infoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InfoesReadDTOs>> GetInfo(int id)
        {
            if (_context.Info == null)
            {
                return NotFound();
            }
            var info = await _context.Info.FindAsync(id);

            if (info == null)
            {
                return NotFound();
            }

            var returnuser = _mapper.Map<InfoesReadDTOs>(info);

            return returnuser;
        }



        // PUT: api/Infoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInfo(int id, InfoesUpdateDTOs infoesUpdateDTOs)
        {
            var info = await _context.Info.FindAsync(id);
            if (id != infoesUpdateDTOs.info_id)
            {
                return BadRequest();
            }
            if (info == null)
            {
                throw new Exception($"Information {id} is not found");
            }
            _mapper.Map(infoesUpdateDTOs, info);
            _context.Info.Update(info);
            await _context.SaveChangesAsync();

            var infoReadDTO = _mapper.Map<InfoesUpdateDTOs>(info);
            return Ok(infoReadDTO);
        }
     
    
            

            
        // POST: api/Infoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Info>> PostInfoesCreateDTOs(InfoesCreateDTOs info)
        {
          if (info == null)
          {
                return BadRequest("Entity set 'ResumeContext.Info' is not null.");
          }
            var infoEntity = _mapper.Map<Info>(info);
            _context.Info.Add(infoEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInfo", new { id = infoEntity.info_id }, infoEntity);
        }

        // DELETE: api/Infoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInfo(int id)
        {
            if (_context.Info == null)
            {
                return NotFound();
            }
            var info = await _context.Info.FindAsync(id);
            if (info == null)
            {
                return NotFound();
            }

            _context.Info.Remove(info);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InfoExists(int id)
        {
            return (_context.Info?.Any(e => e.info_id == id)).GetValueOrDefault();
        }
    }
}
