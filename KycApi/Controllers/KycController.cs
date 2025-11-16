using KycApi.Data;
using KycApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace KycApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KycController : ControllerBase
    {
        private readonly AppDbContext _context;

        public KycController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/kyc
        [HttpPost("submit")]
        public async Task<IActionResult> SubmitKyc([FromBody] KycApplication kyc)
        {
            if (kyc == null)
                return BadRequest("KYC data is null.");

            try
            {
                await _context.KycApplications.AddAsync(kyc);
                await _context.SaveChangesAsync();
                return Ok(new { Message = "KYC submitted successfully", KycId = kyc.Id });
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                Console.WriteLine("Error saving KYC: " + ex.Message);

                return StatusCode(500, new { Message = "Error saving KYC", Error = ex.Message });
            }
        }

        // GET: api/kyc/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var kyc = await _context.KycApplications.FindAsync(id);
            if (kyc == null)
                return NotFound(new { Message = "KYC record not found" });

            return Ok(kyc);
        }
    }
}
