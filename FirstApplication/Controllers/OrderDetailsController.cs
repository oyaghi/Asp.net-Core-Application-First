using FirstApplication.Commands;
using FirstApplication.Data;
using FirstApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstApplication.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly TestingDbContext _db;
        public OrderDetailsController(TestingDbContext db)
        {
            _db = db;
        }

        // There is no need for the Index for the OrderDetail, just add an option for the Order Index to add Order detail, and also showing the attributes of the OrderDetail will be from the Order index page 
        // The OrderDetail Controller will only have, Add(Create), Update, Delete, Since index should be the job of the OrderController, anyway the OrderDetail will be specifc Order/5 then display the order detail for it, 
        // !!Thinking!! The Edit, should be availabe from the Order specific Page, and the there shouldn't be a Delete function 
        // The order should be updated, FORGOT





        [HttpGet]
        public IActionResult Create(int id)
        {
            HttpContext.Session.SetInt32("OrderId", id);

            return View();
        }


        [HttpPost]
        public IActionResult Create(CreateOrderDetailCommand obj)
        {

            if (ModelState.IsValid)
            {
                var orderId = HttpContext.Session.GetInt32("OrderId");


                if (orderId.HasValue)
                {
                    var order = _db.Orders
                   .Include(o => o.OrderDetail)
                   .FirstOrDefault(order => order.Id == orderId);

                    if (order == null)
                    {

                        return View();
                    }

                    if (!order.OrderDetail.Any())
                    {
                        // creating the OrderDetail, get the Order, set the Order.OrderDetail to new Created OrderDetail
                        var orderDetail = new OrderDetail();
                        orderDetail.Quantity = obj.Quantity;
                        orderDetail.OrderId = orderId.Value;
                        //orderDetail.ProductId = null;
                        order.OrderDetail.Add(orderDetail);
                        _db.SaveChanges();

                        return RedirectToAction("Index", "Order");

                    }

                }

            }
            return View();
        }





        [HttpGet]
        public IActionResult Edit(int id)
        {
            var orderDetail = _db.OrderDetails.FirstOrDefault(o => o.Id == id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            var updateOrderDetail = new UpdateOrderDetailCommand();
            updateOrderDetail.Quantity= orderDetail.Quantity;

            return View(updateOrderDetail);
        }
        [HttpPost]
        public IActionResult Edit(UpdateOrderDetailCommand obj)
        {
            if (ModelState.IsValid)
            {
                var orderDetail = _db.OrderDetails.FirstOrDefault(o => o.Id == obj.Id);
                if (orderDetail == null) {
                    return View(obj);
                
                }
                orderDetail.Quantity = obj.Quantity;
                _db.Update(orderDetail);
                _db.SaveChanges();
                return RedirectToAction("Index", "Order");

            }
            return View(obj);

        }

        [HttpGet]
        public IActionResult Delete(int id) {



            var orderDetail = _db.OrderDetails.FirstOrDefault(o => o.Id == id);
            if (orderDetail == null)
            {
                return NotFound();
            }
        
            return View(orderDetail);
        
        }


        [HttpPost]
        public IActionResult Delete(OrderDetail obj)
        {
            _db.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index", "Order");
        }






    }
}
