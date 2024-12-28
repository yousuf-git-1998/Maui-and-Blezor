using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Data;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly ProductsAPIContext _context;

        public ProductCategoriesController(ProductsAPIContext context)
        {
            _context = context;
        }

        // GET: ProductCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetProductCategory()
        {
            return await _context.ProductCategory.Include(pc => pc.Products).ToListAsync();
        }

        // GET: ProductCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategory>> GetProductCategory(int id)
        {
            var productCategory = await _context.ProductCategory.Include(pc => pc.Products).SingleAsync(pc => pc.ProductCategoryID == id);

            if (productCategory == null)
            {
                return NotFound();
            }

            return productCategory;
        }

        // PUT: ProductCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductCategory(int id, ProductCategory productCategory)
        {
            if (id != productCategory.ProductCategoryID)
            {
                return BadRequest();
            }
            var prevCat = await _context.ProductCategory.Include(x => x.Products).FirstOrDefaultAsync(x => x.ProductCategoryID == id);
            if (prevCat == null) { return NotFound(); }

            _context.Entry(prevCat).CurrentValues.SetValues(productCategory);

            foreach (var prevProducts in prevCat.Products.ToList())
            {


                var newProduct = productCategory.Products.FirstOrDefault(x => x.ProductID == prevProducts.ProductID);
                if (newProduct != null)
                {
                    _context.Entry(prevProducts).CurrentValues.SetValues(newProduct);
                }
                else
                {
                    _context.Remove(prevProducts);

                }



            }

            foreach (var addProducts in productCategory.Products)
            {
                if (addProducts.ProductID == 0)
                {
                    prevCat.Products.Add(addProducts);
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductCategoryExists(id))
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

        // POST: ProductCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductCategory>> PostProductCategory(ProductCategory productCategory)
        {
            _context.ProductCategory.Add(productCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductCategory", new { id = productCategory.ProductCategoryID }, productCategory);
        }

        // DELETE: ProductCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductCategory(int id)
        {
            var productCategory = await _context.ProductCategory.FindAsync(id);
            if (productCategory == null)
            {
                return NotFound();
            }

            _context.ProductCategory.Remove(productCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductCategoryExists(int id)
        {
            return _context.ProductCategory.Any(e => e.ProductCategoryID == id);
        }
    }
}
