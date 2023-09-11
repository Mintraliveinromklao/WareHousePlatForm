using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Utils;
using WareHousePlatForm.Data;
using WareHousePlatForm.Models;

namespace WareHousePlatForm.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductInformationsController : ControllerBase
    {
        private readonly warehouseContext _context;

        public ProductInformationsController(warehouseContext context)
        {
            _context = context;
        }

        // GET: api/ProductInformations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductInformation>>> GetProductInformation()
        {
            return await _context.ProductInformation.ToListAsync();
        }

        [HttpGet("productInRemovepage")]
        public async Task<ActionResult<List<string>>> productInRemovepage()
        {
            var productInformation = await _context.ProductInformation.Select(x => new ProductRemovePage
            {
                name = x.Name,
                stock = (int)x.Stock
            }).ToListAsync();

            return Ok(productInformation);
        }

        [HttpGet("productBoundPage")]
        public async Task<ActionResult<selectProductBoundPage>> productBoundPage()
        {
            var productBoundPage = await _context.ProductInformation.Select(x => new selectProductBoundPage
            {
                Name = x.Name,
                Category = x.Category,
                Code = x.Code,
                UnitOfUsable = x.UnitOfUsable
            }).ToListAsync();

            return Ok(productBoundPage);
        }

        [HttpGet("getProdut")]
        public async Task<ActionResult> getProdut()
        {
            var products = await _context.ProductInformation.ToListAsync();
            return Ok(products);
        }

        [HttpGet("getForEdit/{name}")]
        public async Task<ActionResult<ProductInformation>> getForEdit(string name)
        {
            var data = _context.ProductInformation.FirstOrDefault(x => x.Name == name);

            return Ok(data);
        }

        [HttpPost("Edit")]
        public async Task<ActionResult> PostImportItem(ProductInformation product)
        {
            _context.Entry(product).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();

        }

        // GET: api/ProductInformations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductInformation>> GetProductInformation(string id)
        {
            var productInformation = await _context.ProductInformation.FindAsync(id);

            if (productInformation == null)
            {
                return NotFound();
            }

            return productInformation;
        }

        // PUT: api/ProductInformations/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductInformation>> Edit(string id, ProductInformation productInformation)
        {
            if (id != productInformation.Name)
            {
                return BadRequest();
            }

            _context.Entry(productInformation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductInformationExists(id))
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

        // POST: api/ProductInformations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductInformation>> PostProductInformation(ProductInformation productInformation)
        {
            _context.ProductInformation.Add(productInformation);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductInformationExists(productInformation.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProductInformation", new { id = productInformation.Name }, productInformation);
        }

        // DELETE: api/ProductInformations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductInformation(string id)
        {
            var productInformation = await _context.ProductInformation.FindAsync(id);
            if (productInformation == null)
            {
                return NotFound();
            }

            _context.ProductInformation.Remove(productInformation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductInformationExists(string id)
        {
            return _context.ProductInformation.Any(e => e.Name == id);
        }
    }
}
