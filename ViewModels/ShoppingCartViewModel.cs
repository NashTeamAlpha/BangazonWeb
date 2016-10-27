using System.Collections.Generic;
using BangazonWeb.Models;
using BangazonWeb.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BangazonWeb.ViewModels
{
    //Class Name: ShoppingCartViewModel
    //Author: Grant Regnier
    //Purpose of the class: The purpose of this class is to pass data from the controller to the shoppingcart view with a selectlist of PaymentTypes
    //Methods in Class: ShoppingCartViewModel(ctx)
  public class ShoppingCartViewModel : BaseViewModel
  {
    public List<SelectListItem> ListOfPaymentTypes { get; set; }
    public IEnumerable<Product> Products { get; set; }
    private BangazonWebContext context;
    private ActiveCustomer singleton = ActiveCustomer.Instance;
    
    //Method Name: ShoppingCartViewModel
    //Purpose of the Method: Upon construction this should take the context and send a list of select items of the type PaymentType to the View. They should be the paymentTypes of the active customer.
    //Arguments in Method: BangazonWebContext
    public ShoppingCartViewModel(BangazonWebContext ctx)
    {
        context = ctx;
        this.ListOfPaymentTypes = context.PaymentType
            .Where(pt => pt.CustomerId == singleton.Customer.CustomerId)
            .AsEnumerable()
            .Select(pt => new SelectListItem { 
                Text = $"{pt.FirstName} {pt.LastName} {pt.Processor} {pt.ExpirationDate}",
                Value = pt.PaymentTypeId.ToString()
            }).ToList();
        
        this.ListOfPaymentTypes.Insert(0, new SelectListItem {
          Text="Choose Payment Type"
        });
    }
  }
}