
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApp.Services;
using LibraryApp.Data;
using LibraryApp.Models;
using LibraryApp.Dto.Rent;

namespace LibraryApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RentTbController : ControllerBase
    {

        private readonly ILogger<RentTbController> _logger;
        private readonly LibraryAppContext _context;
        private readonly RentServices rentService;

        public RentTbController(ILogger<RentTbController> logger, LibraryAppContext context)
        {
            _logger = logger;
            _context = context;
            rentService = new RentServices(_context);
        }
       

        [HttpGet("GetAllRents")]
        public async Task<ActionResult> GetAll([FromQuery] int pgNumber, [FromQuery] int pgSize)
        {
            _logger.LogInformation("GetAllRents");
            try
            {
                var rents = rentService.GetAllRents()
                    .Skip((pgNumber - 1) * pgSize)
                    .Take(pgSize)
                    .ToList();
                if (rents == null)
                {
                    _logger.LogWarning("No Content");
                    return NoContent();
                }
                return Ok(rents);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }
        [HttpGet("GetAllRentsStatus/{status}")]
        public async Task<ActionResult> GetAllRentsStatus([FromRoute] string status, [FromQuery] int pgNumber, [FromQuery] int pgSize)
        {
            _logger.LogInformation("GetAllRentsStatus");
            try
            {
                var rents = rentService.GetAllRentsStatus(status)
                    .Skip((pgNumber - 1) * pgSize)
                    .Take(pgSize)
                    .ToList();
                if (rents == null)
                {
                    _logger.LogWarning("No Content");
                    return NoContent();
                }
                return Ok(rents);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }
        [HttpGet("GetRentsFromRenterCpf/{cpf}")]
        public async Task<ActionResult> GetRentsFromRenterCpf([FromRoute] long cpf, [FromQuery] int pgNumber, [FromQuery] int pgSize)
        {
            _logger.LogInformation("GetRentsFromName");
            try
            {
                var rents = rentService.GetRentsFromRenterCpf(cpf)
                    .Skip((pgNumber - 1) * pgSize)
                    .Take(pgSize)
                    .ToList();
                if (rents == null)
                {
                    _logger.LogWarning("No Content");
                    return NotFound();
                }
                return Ok(rents);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [HttpGet("GetRentsFromBookId/{id}")]
        public async Task<ActionResult> GetRentsFromBookId([FromRoute] int id, [FromQuery] int pgNumber, [FromQuery] int pgSize)
        {
            _logger.LogInformation("GetRentsFromBookId");

            try
            {
                var rents = rentService.GetRentsFromBookId(id)
                    .Skip((pgNumber - 1) * pgSize)
                    .Take(pgSize)
                    .ToList(); 
                if (rents == null)
                {
                    _logger.LogWarning("No Content");
                    return NotFound();
                }
                return Ok(rents);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message + ex.StackTrace);
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [HttpGet("GetRentsFromId/{id}")]
        public async Task<ActionResult> GetRentsFromId([FromRoute] int id)
        {
            _logger.LogInformation("GetRentsFromId");

            try
            {
                var rents = rentService.GetRentsFromId(id);
                if (rents == null)
                {
                    _logger.LogWarning("No Content");
                    return NotFound();
                }
                return Ok(rents);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message + ex.StackTrace);
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [HttpPost]
        public async Task<ActionResult> RentBook([FromBody] RentTbDto rent)
        {
            _logger.LogInformation("RentBook");

            try
            {
                var rentReturn = await rentService.RentBook(rent);
                if (rentReturn == null)
                {
                    _logger.LogWarning("No Content");
                    return NoContent();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [HttpPut("ReturnBook")]
        public async Task<ActionResult> ReturnBook([FromBody] RentTb rent)
        {
            _logger.LogInformation("ReturnBook");

            try
            {
                var rentReturn = await rentService.ReturnBook(rent);
                if (rentReturn == null)
                {
                    _logger.LogWarning("No Content");
                    return NoContent();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRent([FromRoute] int id, [FromBody] RentTb rent)
        {
            _logger.LogInformation("UpdateRent");

            if (id != rent.IdRent)
            {
                _logger.LogWarning("No Content");
                return BadRequest();
            }
            try
            {
                var rentReturn = await rentService.UpdateRent(rent);
                if (rentReturn == null)
                {
                    return NotFound();
                }
                return Ok(rentReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRent([FromRoute] int id)
        {
            _logger.LogInformation("DeleteRent");

            try
            {
                var rentReturn = await rentService.DeleteRent(id);
                if (rentReturn == null)
                {
                    _logger.LogWarning("No Content");
                    return NotFound();
                }
                return Ok(rentReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }
    }
}
