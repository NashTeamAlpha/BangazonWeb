using System.Collections.Generic;
using BangazonWeb.Models;
using BangazonWeb.Data;

namespace BangazonWeb.ViewModels
{
    //Class Name: AllProductTypesViewModel
    //Author: Debbie Bourne
    //Purpose of the class: The purpose of this class is to manage the property and method that will produce the data and functionality needed for the view of all of the product types
    //Constructor in Class: AllProductTypesViewModel() 
    public class AllProductTypesViewModel : BaseViewModel
    {
        public IEnumerable<ProductType> ProductTypes { get; set; }

          public AllProductTypesViewModel(BangazonWebContext ctx) : base(ctx) { } 
    }
}