using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WareHousePlatForm.Data;
using WareHousePlatForm.Models;

namespace WareHousePlatForm.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoundDatasController : ControllerBase
    {
        private readonly warehouseContext _context;

        public BoundDatasController(warehouseContext context)
        {
            _context = context;
        }

        // GET: api/BoundDatas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BoundData>>> GetBoundData()
        {
            return await _context.BoundData.ToListAsync();
        }

        [HttpGet("getboundByName/{name}")]
        public async Task<ActionResult<List<BoundList>>> getboundByName(string name)
        {
            var data = await _context.BoundData.Where(x => x.UserName == name && x.Status == "Borrow").Select(x => new BoundList
            {
                Name = x.Name,
                Unit = x.Unit,
                DateBorrow = x.DateBorrow
            }).ToListAsync();

            return Ok(data);
        }

        [HttpGet("getBound")]
        public async Task<ActionResult<BoundData>> getBound()
        {
            var boundData = await _context.BoundData.ToListAsync();
            return Ok(boundData);
        }


        // GET: api/BoundDatas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BoundData>> GetBoundData(string id)
        {
            var boundData = await _context.BoundData.FindAsync(id);

            if (boundData == null)
            {
                return NotFound();
            }

            return boundData;
        }

        [HttpGet("getrebound/{user}/{name}")]
        public async Task<ActionResult<BoundData>> boundReturn(string user, string name)
        {
            var data = _context.BoundData.FirstOrDefault(x => x.UserName == user && x.Name == name && x.Status == "borrow");
            return Ok(data);
        }

        // PUT: api/BoundDatas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoundData(string id, BoundData boundData)
        {
            if (id != boundData.UserName)
            {
                return BadRequest();
            }

            _context.Entry(boundData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoundDataExists(id))
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

        [HttpPost("Reboud")]
        public async Task<IActionResult> Rebound(BoundData bound)
        {
            _context.Entry(bound).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/BoundDatas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("boud")]
        public async Task<ActionResult> bound(BoundData boundData)
        {
            _context.BoundData.Add(boundData);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BoundDataExists(boundData.UserName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/BoundDatas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoundData(string id)
        {
            var boundData = await _context.BoundData.FindAsync(id);
            if (boundData == null)
            {
                return NotFound();
            }

            _context.BoundData.Remove(boundData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BoundDataExists(string id)
        {
            return _context.BoundData.Any(e => e.UserName == id);
        }
    }
}
