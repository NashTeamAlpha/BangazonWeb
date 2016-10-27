using System.Collections.Generic;
using BangazonWeb.Models;
using BangazonWeb.Data;

namespace BangazonWeb.ViewModels
{
  public class ShoppingCartViewModel : BaseViewModel
  {
    public IEnumerable<Product> Products { get; set; }
    public IEnumerable<PaymentType> PaymentTypes { get; set; }
    public ShoppingCartViewModel(BangazonWebContext ctx) : base(ctx) { }
  }
}