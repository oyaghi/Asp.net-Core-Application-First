using FirstApplication.Commands;
using FirstApplication.Data;
using FirstApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Drawing.Printing;

namespace FirstApplication.Controllers
{
    public class ProductsController(TestingDbContext db) : Controller
    {

        private readonly TestingDbContext _db = db;

        public async Task<IActionResult> Index(int? pageNumber, int pageSize = 5)
        {

            int currentPage = pageNumber ?? 1;
            int totalItems = await _db.Products.CountAsync();

            // Get only the products for the current page using Skip and Take
            var items = await _db.Products.Skip((currentPage - 1) * pageSize)
                                      .Take(pageSize).ToListAsync();

            ViewBag.CurrentPage = currentPage;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems;

            return View(items);
        }

        public async Task<IActionResult> GetProducts(int? pageNumber, int pageSize = 5)
        {
            int currentPage = pageNumber ?? 1;
            var items = await _db.Products.Skip((currentPage - 1) * pageSize)
                                          .Take(pageSize).ToListAsync();

            return PartialView("_ProductTablePartial", items);
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
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
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
