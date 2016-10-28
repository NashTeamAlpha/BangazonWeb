using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonWeb.Data;
using BangazonWeb.Models;
using BangazonWeb.ViewModels;
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
        private ActiveCustomer singleton = ActiveCustomer.Instance;
        public ProductsController (BangazonWebContext ctx)
        {
            context = ctx;
        }

        //Method Name: Index
        //Purpose of the Method: This method is the first method that is loaded when a user visits Bangazon. This method will get all of the products in the database and show them on the homepage.
        //Arguments in Method: there are no arguments being taken in to this method
        public async Task<IActionResult> Index()
        {
            AllProductsViewModel model = new AllProductsViewModel(context);

            model.Products = await context.Product.ToListAsync();
 
            return View(model);
        }
        //Method Name: Types
        //Purpose of the Method: When the customer clicks on the product types link in the navbar it activates this method. this will return the product types to the view
        //Arguments in Method: there are no arguments being taken in to this method
        public async Task<IActionResult> Types()
        {
            AllProductTypesViewModel model = new AllProductTypesViewModel(context);
           
            model.ProductTypes = await context.ProductType.ToListAsync();

            return View(model);
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
            
            var ProductsInSubType = await context.Product.Where(p => p.SubProductTypeId == id).ToListAsync();
            //var ProductsInType = await context.Product.ToListAsync(p => p.ProductTypeId == id);    weren't sure if this would work
            
            if (ProductsInSubType == null)
            {
                return NotFound();
            }
            return View(ProductsInSubType);
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
            
            SingleProductViewModel model = new SingleProductViewModel(context);

            model.Product = await context.Product.SingleOrDefaultAsync(p => p.ProductId == id);

            if (model.Product == null)
            {
                return NotFound();
            }
            
            return View(model);
        }
        //Method Name: New
        //Purpose of the Method: This method returns the New.cshtml file in the Products folder, which will contain a form to add a new product
        //Arguments in Method: No arguments are passed into this method
        [HttpGet]
        public IActionResult New()
        {
            ProductTypesListViewModel model = new ProductTypesListViewModel(context);
            return View(model);
        }

        // //Method Name: New
        // //Purpose of the Method: This method takes information from the add product form and posts that information to the database, if it is valid. If the information is invalid, the user will be returned back to the form view. 
        // //Arguments in Method: This method takes in an argument of type Product from the form 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                context.Add(product);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //Make sure error messages are present in the view if the view is returned to the customer
            return NotFound();
        }
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> AddToCart([FromRoute] int? id)
         {
             //Get the active customer's order
             var activeOrder = await context.Order.Where(o => o.IsCompleted == false && o.CustomerId==singleton.Customer.CustomerId).SingleOrDefaultAsync(); 

             if (activeOrder == null)
             {
                 var order = new Order();
                 order.IsCompleted = false;
                 order.CustomerId = Convert.ToInt32(singleton.Customer.CustomerId);
                 context.Add(order);
                 await context.SaveChangesAsync();
                 var newOrder = await context.Order.Where(o => o.IsCompleted == false && o.CustomerId==singleton.Customer.CustomerId).SingleOrDefaultAsync();
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

        //Method Name: GetSubTypes
        //Purpose of the Method: This method is called when the user changes the Product Type dropdown list to select a larger product category. The method grabs all subTypes within that Product Type and returns them in a Json format. 
        //Arguments in Method: Takes in an integer, which is equal to the ProductTypeId of the selected larger Product Category. 
        [HttpPost]
        public IActionResult GetSubTypes([FromRoute]int id)
        {
            //get sub categories with that product type on them
            var subTypes = context.SubProductType.Where(p => p.ProductTypeId == id).ToList();
            // ProductTypesListViewModel model = new ProductTypesListViewModel(context, id);
            // return View("~/Views/Products/New.cshtml", model);
            return Json(subTypes);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
