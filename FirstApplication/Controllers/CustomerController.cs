using FirstApplication.Commands;
using FirstApplication.Data;
using FirstApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
namespace FirstApplication.Controllers
{
    public class CustomerController : Controller
    {
        // Customer Controller Constructure Start
        private readonly TestingDbContext _db;
        public CustomerController(TestingDbContext db)
        {
            _db = db;
        }
        // Customer Controller Constructure End

        // Index page 
        public IActionResult Index()
        {
            var all_customers = _db.Customers.ToList();
            return View(all_customers);
        }


        // Create  Start, (Implement Input validation)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]                                  // We have created two Actions one for Get to view the page, and one for POST to get the data from the user page 
        public IActionResult Create(CreateCustomerCommand obj)
        {
            if (ModelState.IsValid)
            {
                var NewCustomer = new Customer();
                NewCustomer.Address = obj.Address;
                NewCustomer.Phone = obj.Phone;
                NewCustomer.FirstName = obj.FirstName;
                NewCustomer.LastName = obj.LastName;
                _db.Customers.Add(NewCustomer);
                _db.SaveChanges();
                return RedirectToAction("Index", "Customer");

            }
            return View(obj); // will add the errors to the obj so when returning it, it will display the errors, and it will populate the input fields 

        }

        // Create End

        // Edit Start (Implement Input validation)
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var customer = _db.Customers.FirstOrDefault(c => c.Id == id); // using FirstOrDefault is better perforamnce (Select TOP 1 * From Customers) something like that 
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }



        [HttpPost]
        public IActionResult Edit(UpdateCustomerCommand obj)

        {



            var OldCustomer = _db.Customers.Find(obj.Id);
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (obj.Phone != OldCustomer.Phone)
            {
                if (_db.Customers.Any(c => c.Phone == obj.Phone))
                {
                    ModelState.AddModelError("Phone", "The phone number is already in use.");
                    return View();

                }
            }


            var NewCustomer = new Customer();
            NewCustomer.Address = obj.Address;
            NewCustomer.Phone = obj.Phone;
            NewCustomer.FirstName = obj.FirstName;
            NewCustomer.LastName = obj.LastName;
            _db.Update(NewCustomer);
            _db.SaveChanges();
            return RedirectToAction("Index", "Customer");

        }

        // Edit End

        // Delete Start
        [HttpGet]
        public IActionResult Delete(int id)
        {

            var customer = _db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        public IActionResult Delete(Customer obj) // The obj attributes is null except the id is populated 
        {

            _db.Remove(obj);
            _db.SaveChanges();

            return RedirectToAction("Index", "Customer");
        }

        // Delete End 
    }
}
