using FirstApplication.Commands;
using FirstApplication.Data;
using FirstApplication.Models;
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

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(LoginCustomerCommand obj)
        {
            if (ModelState.IsValid)
            {
                var customer = _db.Customers
                .FirstOrDefault(c => c.Phone == obj.Phone && c.FirstName == obj.FirstName);

                if (customer != null)
                {
                    HttpContext.Session.SetInt32("UserId", customer.Id);
                    return RedirectToAction("Index", "Order");

                }
                else
                {
                    ViewData["ErrorMessage"] = "Customer doesn't exist";

                    return View();
                }

            }

            ViewData["ErrorMessage"] = "Invalid phone number or first name.";
            return View();

        }


        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                // if the user don't have a userId in the session he should redirected to login 
                return RedirectToAction("Login", "Order");
            }

            // Fetch orders associated with the user
            var orders = _db.Orders
                .Include(o => o.Customer)    // Eager load the Customer
                .Include(o => o.OrderDetail) // Eager load the OrderDetail
                .Where(o => o.CustomerId == userId) // Filter orders by CustomerId
                .ToList();

            return View(orders);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateOrderCommand obj)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                if (userId.HasValue)
                {
                    var newOrder = new Order();
                    newOrder.OrderFulfilled = obj.OrderFulfilled;
                    newOrder.OrderPlaced = obj.OrderPlaced;
                    newOrder.CustomerId = userId.Value;
                    _db.Orders.Add(newOrder);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "Order");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var order= _db.Orders.Find(id);
            if (order ==null)
            {
                return NotFound();
            }

            var UpdateOrder = new UpdateOrderCommand();
            UpdateOrder.OrderFulfilled = order.OrderFulfilled;
            UpdateOrder.OrderPlaced= order.OrderPlaced;
            return View(UpdateOrder);
        }


        [HttpPost]
        public IActionResult Edit(UpdateOrderCommand obj)
        {

            if (ModelState.IsValid) {
                var order= _db.Orders.Find(obj.Id);
                
                if(order != null)
                {
                    order.OrderFulfilled = obj.OrderFulfilled;
                    order.OrderPlaced=obj.OrderPlaced;
                    _db.Update(order);
                    _db.SaveChanges();
                }
                
            }
            
            return RedirectToAction("Index", "Order");
        }

        [HttpGet]
        public IActionResult Delete(int id) {
            var order = _db.Orders.Find(id);
            if (order != null)
            {
                return View(order);
            }

            return NotFound();
            
        }
       
        [HttpPost]

        public IActionResult Delete(Order obj)
        {

            _db.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index", "Order");
        }
     

    }
}


  