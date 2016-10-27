using System.Collections.Generic;
using BangazonWeb.Models;
using BangazonWeb.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BangazonWeb.ViewModels
{
  public class ShoppingCartViewModel : BaseViewModel
  {
    public List<SelectListItem> ListOfPaymentTypes { get; set; }
    public IEnumerable<Product> Products { get; set; }
    private BangazonWebContext context;
    private ActiveCustomer singleton = ActiveCustomer.Instance;

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