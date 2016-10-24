using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonWeb.Models;
using BangazonWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BangazonWeb.Controllers
{
    public class CustomerController : Controller
    {
    // Class Name: Customers
    // Author: Chris Smalley
    // Purpose of the class: The purpose of this class is to manage the methods that will produce the data and functionality needed for all of the views in the user interface related to customers.
    // Methods in Class: New(), ShoppingCart(), Payment(), OrderCompleted() 
        private BangazonWebContext context;
        public CustomerController(BangazonWebContext ctx)
        {
            context = ctx;
        }
        public IActionResult New()
        {
            //Method Name: New
            //Purpose of the Method: Loads new customer form to the view, view wil be static.
            //Arguments in Method: Need more clarification here
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryTokenAttribute]
        public async Task<IActionResult> Create(Customer customer)
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
            return View(customer);
        }
        [HttpGet]
        public IActionResult ShoppingCart()
        {
            //Method Name: ShoppingCart
            //Purpose of the Method: 
            //Gets all LineItems on active order and give data to the returned View. 
            //Gets all PaymentTypes of selected Customer and give data to the returned View.
            //This method returns the Customer/ShoppingCart view.
            
        }


    }
}