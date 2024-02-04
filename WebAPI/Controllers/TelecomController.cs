using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebAPI.Models;
using System.Text.Json;
using System.Diagnostics.Metrics;
using Microsoft.VisualBasic;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelecomController : ControllerBase
    {
        private readonly telecomdatabaseContext _context;

        public TelecomController(telecomdatabaseContext context)
        {
            _context = context;
        }

        // GET: api/PhoneNumbers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhoneNumber>>> GetPhoneNumbers()
        {
            if (_context.PhoneNumbers == null)
            {
                return NotFound();
            }
            return _context.PhoneNumbers.ToList();
        }

        // GET: api/PhoneNumber/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<PhoneNumber>>> GetPhoneNumbersByCustomerID(int id)
        {
            if (_context.PhoneNumbers == null)
            {
                return NotFound();
            }
            var phoneNumbers = await _context.PhoneNumbers.Where(a => a.Id == id).ToListAsync();

            if (phoneNumbers == null)
            {
                return NotFound();
            }

            return phoneNumbers;
        }

        // POST: api/PhoneNumber
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Tmp values)
        {
            if (_context.PhoneNumbers == null)
            {
                return Problem("Entity set '_context.PhoneNumbers' is null.");
            }

            //Tmp MyFinalObject = JsonConvert.DeserializeObject < Tmp>(values);
            PhoneNumber newPhoneNumber = new PhoneNumber { PhoneNo = values.PhoneNo, IsActive = values.IsActive, CustomerName = values.CustomerName, CreatedBy = "Admin", CreatedDate = DateTime.Now };
            _context.PhoneNumbers.Add(newPhoneNumber);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // PUT: api/PhoneNumber/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("api/Telecom/{id}")]
        [HttpPut]
        public async Task<ActionResult<PhoneNumber>> Put(int id, bool isActive)
        {
            var phoneNumber = await _context.PhoneNumbers.FindAsync(id);

            _context.Entry(phoneNumber).State = EntityState.Modified;

            try
            {
                phoneNumber.IsActive = isActive;
                phoneNumber.UpdatedBy = "Admin";
                phoneNumber.UpdatedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhoneNumberExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool PhoneNumberExists(int phoneNumberID)
        {
            return (_context.PhoneNumbers?.Any(e => e.Id == phoneNumberID)).GetValueOrDefault();
        }
    }
}
