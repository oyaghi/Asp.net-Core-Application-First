using FirstApplication.Commands;
using FirstApplication.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstApplication.Controllers
{
    public class OrderController : Controller
    {
        private readonly TestingDbContext _db;
        public OrderController(TestingDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var orders = _db.Orders
                    .Include(o => o.Customer) // Eager load the Customer and the Order Detail, by this the Customer object will get retrieved
                    .Include(o => o.OrderDetail) 
                    .ToList();

            return View(orders);
        }
        [HttpGet]
        public IActionResult Create() { 
        
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateOrderCommand obj)
        {

            return RedirectToAction("Index", "Order");
        }
    }
}
