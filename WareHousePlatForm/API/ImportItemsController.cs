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
    public class ImportItemsController : ControllerBase
    {
        private readonly warehouseContext _context;

        public ImportItemsController(warehouseContext context)
        {
            _context = context;
        }

        // GET: api/ImportItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImportItem>>> GetImportItem()
        {
            return await _context.ImportItem.ToListAsync();
        }

        [HttpGet("getImport")]
        public async Task<ActionResult<ImportItem>> getImport()
        {
            var items = await _context.ImportItem.ToListAsync();
            return Ok(items);
        }


        // GET: api/ImportItems/5
        [HttpGet("ImportDatas")]
        public async Task<ActionResult<ImportItem>> GetImportItem(DateTime id)
        {
            var importItem = await _context.ImportItem.ToListAsync();

            if (importItem == null)
            {
                return NotFound();
            }

            return Ok( importItem);
        }

        [HttpPost]
        public async Task<ActionResult<ImportItem>> AddItemToTable(ImportItem item)
        {
            return NoContent();
        }

        // PUT: api/ImportItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("/Edit/{item}")]
        public async Task<IActionResult> PutImportItem(DateTime id, ImportItem importItem)
        {
            if (id != importItem.DateTime)
            {
                return BadRequest();
            }

            _context.Entry(importItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImportItemExists(id))
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

        // POST: api/ImportItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Add")]
        public async Task<ActionResult<ImportItem>> PostImportItem(ImportItem importItem)
        {
            _context.ImportItem.Add(importItem);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ImportItemExists(importItem.DateTime))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetImportItem", new { id = importItem.DateTime }, importItem);
        }

        // DELETE: api/ImportItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImportItem(DateTime id)
        {
            var importItem = await _context.ImportItem.FindAsync(id);
            if (importItem == null)
            {
                return NotFound();
            }

            _context.ImportItem.Remove(importItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImportItemExists(DateTime id)
        {
            return _context.ImportItem.Any(e => e.DateTime == id);
        }
    }
}
