using System.Collections.Generic;
using BangazonWeb.Models;
using BangazonWeb.Data;

namespace BangazonWeb.ViewModels
{
  public class AllProductsViewModel : BaseViewModel
  {
    public IEnumerable<Product> Products { get; set; }
    public AllProductsViewModel(BangazonWebContext ctx) : base(ctx) { }
  }
}