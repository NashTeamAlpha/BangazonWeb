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
    //Class Name: ProductsController
    //Author: Debbie Bourne, Delaine Wendling
    //Purpose of the class: The purpose of this class is to manage the methods that will produce the data and functionality needed for all of the views in the user interface related to products.
    //Methods in Class: Index(), Types(), TypesList(), Single(), New(), overloaded New(), AddToCart()
    public class ProductsController : Controller
    {
        
        private BangazonWebContext context;
        public ProductsController (BangazonWebContext ctx)
        {
            context = ctx;
        }

        //Method Name: Index
        //Purpose of the Method: This method is the first method that is loaded when a user visits Bangazon. This method will get all of the products in the database and show them on the homepage.
        //Arguments in Method: there are no arguments being taken in to this method
        public async Task<IActionResult> Index()
        {
            return View(await context.Product.ToListAsync());
        }
        //Method Name: Types
        //Purpose of the Method: When the customer clicks on the product types link in the navbar it activates this method. this will return the product types to the view
        //Arguments in Method: there are no arguments being taken in to this method
        public async Task<IActionResult> Types()
        {
            return View(await context.ProductType.ToListAsync());
        }
        //Method Name: TypesList
        //Purpose of the Method: This is called when user clicks on the specific type of products they want to view. this will return all of the products of that type to the view
        //Arguments in Method: this method will take an integer from the route that aligns to the product type Id.
        public async Task<IActionResult> TypesList([FromRoute] int? id)
        {
            if (id == null) 
            {
                return NotFound();
            }
            
            var ProductsInType = await context.Product.Where(p => p.ProductTypeId == id).ToListAsync();
            //var ProductsInType = await context.Product.ToListAsync(p => p.ProductTypeId == id);    weren't sure if this would work
            
            if (ProductsInType == null)
            {
                return NotFound();
            }
            return View(ProductsInType);
        }
        //Method Name: Single
        //Purpose of the Method: This method is called when a user clicks on an inidividual product and gets the data from the database about that product to be returned to the view
        //Arguments in Method: Takes an integer from the route that matched the product selected's product id
        public async Task<IActionResult> Single([FromRoute] int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await context.Product.SingleOrDefaultAsync(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        //Method Name: New
        //Purpose of the Method: This method returns the New.cshtml file in the Products folder, which will contain a form to add a new product
        //Arguments in Method: No arguments are passed into this method
        public IActionResult New()
        {
            return View();
        }
        //Method Name: New
        //Purpose of the Method: This method takes information from the add product form and posts that information to the database, if it is valid. If the information is invalid, the user will be returned back to the form view. 
        //Arguments in Method: This method takes in an argument of type Product from the form 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(Product product)
        {
            if (ModelState.IsValid)
            {
                context.Add(product);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //Make sure error messages are present in the view if the view is returned to the customer
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart([FromRoute] int? id)
        {
            var customerId = ActiveCustomer.customerId;
            if (id == null)
            {
                return NotFound();
            }
            if (customerId == null)
            {
                //return the view with a message saying that there is not an active customer
                return View();
            }
            //Get the active customer's order
            var activeOrder = await context.Order.Where(o => o.IsCompleted == false && o.CustomerId==customerId).SingleOrDefaultAsync(); 
            if (activeOrder == null)
            {
                var order = new Order();
                order.IsCompleted = false;
                order.CustomerId = Convert.ToInt32(customerId);
                context.Add(order);
                await context.SaveChangesAsync();
                var newOrder = await context.Order.Where(o => o.IsCompleted == false && o.CustomerId==customerId).SingleOrDefaultAsync();
                var lineItem = new LineItem();
                lineItem.OrderId = newOrder.OrderId;
                lineItem.ProductId = Convert.ToInt32(id);
                context.Add(lineItem);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else 
            {
                var lineItem = new LineItem();
                lineItem.OrderId = activeOrder.OrderId;
                lineItem.ProductId = Convert.ToInt32(id);
                context.Add(lineItem);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }



        public IActionResult Error()
        {
            return View();
        }
    }
}