using FirstApplication.Commands;
using FirstApplication.Data;
using FirstApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace FirstApplication.Controllers
{
    public class ProductController : Controller
    {

        private readonly TestingDbContext _db;
        public ProductController(TestingDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _db.Products.ToListAsync();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand command)
        {
            if (ModelState.IsValid)
            {
                var product = new Product();
                product.Name = command.Name;
                product.Price = command.Price;
                _db.Products.Add(product); // using await can't only be on database or i/o 
                await _db.SaveChangesAsync();
                return RedirectToAction("Index", "Product");

            }
            return View(command);

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var updateProduct = new UpdateProductCommand();
            updateProduct.Name = product.Name;
            updateProduct.Price = product.Price;
            return View(updateProduct);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProductCommand command)
        {
            if (ModelState.IsValid)
            {
                var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == command.Id);
                if (product == null)
                {
                    return NotFound();

                }
                product.Name = command.Name;
                product.Price = command.Price;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index", "Product");

            }
            return View(command);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p=> p.Id== id);
            if(product == null)
            {
                return NotFound();
            }

            return View(product); 
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Product product)
        {
            _db.Remove(product);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index", "Product");
        }


    }
}
