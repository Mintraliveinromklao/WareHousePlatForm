using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WareHousePlatForm.Data;

namespace WareHousePlatForm.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportHistoriesController : ControllerBase
    {
        private readonly warehouseContext _context;

        public ExportHistoriesController(warehouseContext context)
        {
            _context = context;
        }

        [HttpGet("getExport")]
        public async Task<ActionResult<ExportHistory>> getExport()
        {
            var dataExport = await _context.ExportHistory.ToListAsync();
            return Ok(dataExport);
        }

        // GET: api/ExportHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExportHistory>>> GetExportHistory()
        {
            return await _context.ExportHistory.ToListAsync();
        }

        // GET: api/ExportHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExportHistory>> GetExportHistory(DateTime id)
        {
            var exportHistory = await _context.ExportHistory.FindAsync(id);

            if (exportHistory == null)
            {
                return NotFound();
            }

            return exportHistory;
        }

        // PUT: api/ExportHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExportHistory(DateTime id, ExportHistory exportHistory)
        {
            if (id != exportHistory.DateTime)
            {
                return BadRequest();
            }

            _context.Entry(exportHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExportHistoryExists(id))
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

        // POST: api/ExportHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExportHistory>> PostExportHistory(ExportHistory exportHistory)
        {
            _context.ExportHistory.Add(exportHistory);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ExportHistoryExists(exportHistory.DateTime))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetExportHistory", new { id = exportHistory.DateTime }, exportHistory);
        }

        // DELETE: api/ExportHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExportHistory(DateTime id)
        {
            var exportHistory = await _context.ExportHistory.FindAsync(id);
            if (exportHistory == null)
            {
                return NotFound();
            }

            _context.ExportHistory.Remove(exportHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExportHistoryExists(DateTime id)
        {
            return _context.ExportHistory.Any(e => e.DateTime == id);
        }
    }
}
