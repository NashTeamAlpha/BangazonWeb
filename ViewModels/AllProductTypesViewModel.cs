using System.Collections.Generic;
using BangazonWeb.Models;
using BangazonWeb.Data;

namespace BangazonWeb.ViewModels
{
    public class AllProductTypesViewModel : BaseViewModel
    {
        public IEnumerable<ProductType> ProductType { get; set; }
        public AllProductTypesViewModel(BangazonWebContext ctx) : base(ctx) { } 
    }
}