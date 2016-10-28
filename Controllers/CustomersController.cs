using System;
using System.Linq;
using System.Threading.Tasks;
using BangazonWeb.Models;
using BangazonWeb.Data;
using BangazonWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BangazonWeb.Controllers
{
    // Class Name: CustomersController
    // Authors: Chris Smalley, Zack Repass
    // Purpose of the class: The purpose of this class is to manage the methods that will produce the data and functionality needed for all of the views in the user interface related to customers.
    // Methods in Class: New(), Overloaded New(Customer customer) ShoppingCart(), Payment(), OrderCompleted().
    public class CustomersController : Controller
    {
        //Bringing in the context from our DB and storing it in a local variable named BangazonWebContext.
        private BangazonWebContext context;
        public CustomersController(BangazonWebContext ctx)
        {
            context = ctx;
        }

        //Storing our ActiveCustomer singleton in an private instance.
        private ActiveCustomer singleton = ActiveCustomer.Instance;

        //Method Name: New
        //Purpose of the Method: Loads new customer form to the view, view wil be static.
        //Arguments in Method: None.
        [HttpGet]
        public IActionResult New()
        {

            BaseViewModel model = new BaseViewModel(context);
            return View(model);
        }

        //Method Name: Overloaded New
        //Purpose of the Method: Will take an optional parameter of the type "customer." Takes form data then checks its validity. Post to customer table in database then redirects to home page.
        //Arguments in Method: A new customer object taken from the form of Customer/New.cshtml.
        [HttpPost]
        [ValidateAntiForgeryTokenAttribute]
        public async Task<IActionResult> New(Customer customer)
        {
            if (ModelState.IsValid)
            {
                context.Add(customer);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return BadRequest();
        }

        //Method Name: ShoppingCart
        //Purpose of the Method: 
            //Gets all LineItems on active order and give data to the returned View. 
            //Gets all PaymentTypes of selected Customer and give data to the returned View.
            //This method returns the Customer/ShoppingCart view.
        //Arguments in Method: None.
        [HttpGet]
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

        //Method Name: Payment
        //Purpose of the Method: Method should take you to the Payment view with form to add Payment.
        //Arguments in Method: None.
         [HttpGet]
         public IActionResult Payment()
        {

            BaseViewModel model = new BaseViewModel(context);

            return View(model);
        }

        //Method Name: Overloaded Payment
        //Purpose of the Method: This is the Overloaded method that actually adds the payments to the Db.
        //Arguments in Method: Takes a new PaymentType object from the form provided and posts it to the database.
        [HttpPost]
        public async Task<IActionResult> Payment(PaymentType payment)
        {
            if (ModelState.IsValid)
            {
                context.Add(payment);
                await context.SaveChangesAsync();
                return RedirectToAction("ShoppingCart");
            }
            return BadRequest();
        }

        //Method Name: OrderCompleted
        //Purpose of the Method: To change the isCompleted bool from false to true on the active order for this customer and direct the user to the OrderCompleted view.
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

        //Method Name: Activate
        //Purpose of the Method: To change the current ActiveCustomer singleton to whatever is selected in the top right dropdown select option.
        //Arguments in Method: Takes a CustomerId from the drop down select option in the navigation bar on change of selected option. 
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
