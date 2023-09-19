using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Resume.Data;
using Resume.DTOs.InfoesDTOs;
using Resume.Helpers;
using Resume.Models;
using Resume.Repositories;

namespace Resume.APIController
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepos _genericRepos;
        private const string ApiUsername = "UserID";
        private const string ApiPassword = "Password";

        public InfoesController(IMapper mapper, IGenericRepos genericRepos)
        {
            _mapper = mapper;
            _genericRepos = genericRepos;
            
    }

        [HttpGet]
        [Route("~/api/login")]
        public async Task<ActionResult<Info>> GetLogin()
        {
            if ((!HttpContext.Request.Headers.TryGetValue(ApiUsername, out var Email)) ||
                 (!HttpContext.Request.Headers.TryGetValue(ApiPassword, out var Password)))
            {
                return BadRequest("Both Email and Password are required");
            }
            else
            {
                var userId = await _genericRepos.Login<Info>(user => user.email == Email.ToString());

                if (userId == null)
                {
                    return NotFound("Invalid Email or Password");
                }
                if (userId.password == Password.ToString())
                {
                    var record = _mapper.Map<InfoesReadDTOs>(userId);
                    return Ok(record);
                }
                else
                {
                    return NotFound("Incorrect Password");
                }
            }
        }
           
        // GET: api/Infoes
        [HttpGet]
        public async Task<ActionResult<List<InfoesReadDTOs>>> GetInfo()
        {
            var infos = await _genericRepos.GetAll<Info>();
            var retunInfos = _mapper.Map<List<InfoesReadDTOs>>(infos);
            return Ok(retunInfos);
        }

        // GET: api/Infoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InfoesReadDTOs>> GetInfo(int id)
        {
            
            var info = await _genericRepos.GetByUserId<Info>(userData => userData.info_id == id);

            if (info == null)
            {
                return NotFound();
            }

            var returnInfo = _mapper.Map<List<InfoesReadDTOs>>(info);

            return Ok(returnInfo);
        }



        // PUT: api/Infoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInfo(int id, InfoesUpdateDTOs infoesUpdateDTOs)
        {
            var info = await _genericRepos.GetById<Info>(id);
            if (id != infoesUpdateDTOs.info_id)
            {
                return BadRequest();
            }
            if (info == null)
            {
                throw new Exception($"Information {id} is not found");
            }
            _mapper.Map(infoesUpdateDTOs, info);
           
            info = await _genericRepos.Update<Info>(id, info);

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
            //_context.Info.Add(infoEntity);
            await _genericRepos.Create<Info>(infoEntity);
            //await _context.SaveChangesAsync();

            return Ok("Created");
        }

        // DELETE: api/Infoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInfo(int id)
        {
            var inform = await _genericRepos.Delete<Info>(id);
            if (inform == null)
            {
                return NoContent();
            }
            
            return BadRequest();
        }
       
    }
}
