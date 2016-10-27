using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonWeb.Models;
using BangazonWeb.Data;
using BangazonWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BangazonWeb.Controllers
{
    public class CustomerController : Controller
    {
    // Class Name: Customers
    // Authors: Chris Smalley, Zack Repass
    // Purpose of the class: The purpose of this class is to manage the methods that will produce the data and functionality needed for all of the views in the user interface related to customers.
    // Methods in Class: New(), ShoppingCart(), Payment(), OrderCompleted() 
        private BangazonWebContext context;
        public CustomerController(BangazonWebContext ctx)
        {
            context = ctx;
        }
        private ActiveCustomer singleton = ActiveCustomer.Instance;
        public IActionResult New()
        {
            //Method Name: New
            //Purpose of the Method: Loads new customer form to the view, view wil be static.
            //Arguments in Method: Need more clarification here

            BaseViewModel model = new BaseViewModel(context);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryTokenAttribute]
        public async Task<IActionResult> New(Customer customer)
        {
            //Method Name: OverloadedNew
            //Purpose of the Method: Will take an optional parameter of the type "customer." Takes form data then checks its validity. Post to customer table in database then redirects to home page.
            //Arguments in Method: Need more clarification here
            if (ModelState.IsValid)
            {
                context.Add(customer);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return BadRequest();
        }
        [HttpGet]
            //Method Name: ShoppingCart
            //Purpose of the Method: 
            //Gets all LineItems on active order and give data to the returned View. 
            //Gets all PaymentTypes of selected Customer and give data to the returned View.
            //This method returns the Customer/ShoppingCart view.
        public async Task<IActionResult> ShoppingCart()
        {
            var activeOrder = await context.Order.Where(o => o.IsCompleted == false && o.CustomerId==singleton.Customer.CustomerId).SingleOrDefaultAsync(); 
            if (activeOrder == null)
            {
                var order = new Order();
                order.IsCompleted = false;
                order.CustomerId = Convert.ToInt32(singleton.Customer.CustomerId);
                context.Add(order);
                await context.SaveChangesAsync();
            }
            ShoppingCartViewModel model = new ShoppingCartViewModel(context);

            model.LineItems = activeOrder.LineItems;

            return View(model);
            
        }

         [HttpGet]
         public IActionResult Payment()
        {
            //Method Name: Payment
            //Purpose of the Method: Method should take you to the Payment view with form to add Payment.
            //Arguments in Method: None

            BaseViewModel model = new BaseViewModel(context);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Payment(PaymentType payment)
        {
            //Method Name: Payment - Overloaded
            //Purpose of the Method: This is the Overloaded method that actually adds the payments to the Db.
            //Arguments in Method: Is the PaymentType that was just created in the previous method (Payment).
            if (ModelState.IsValid)
            {
                context.Add(payment);
                await context.SaveChangesAsync();
                return RedirectToAction("ShoppingCart");
            }
            return BadRequest();
        }

        //Method Name: OrderCompleted
        //Purpose of the Method: To change the isCompleted bool from false to true and direct the user to the OrderCompleted view.
        //Arguments in Method: None.

        [HttpPut]
        public async Task<IActionResult> OrderCompleted()
        {
            var activeOrder = await context.Order.Where(o => o.IsCompleted == false && o.CustomerId==singleton.Customer.CustomerId)
            .SingleOrDefaultAsync(); 
            activeOrder.IsCompleted = true;
            context.Update(activeOrder);
            await context.SaveChangesAsync();

            BaseViewModel model = new BaseViewModel(context);

            return View(model);
        }
        [HttpPost]
        public IActionResult Activate([FromRoute]int id)
        {
            // Find the corresponding customer in the DB
            var customer = context.Customer.SingleOrDefault(c => c.CustomerId == id);

            // Return 404 if not found
            if (customer == null)
            {
                return NotFound();
            }

            // Set the active customer to the selected on
            ActiveCustomer.Instance.Customer = customer;

            return Json(customer);
        }
    }
}
