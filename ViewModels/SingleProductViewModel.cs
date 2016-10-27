using System.Linq;
using BangazonWeb.Data;
using BangazonWeb.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BangazonWeb.ViewModels
{   //Class Name: SingleProductViewModel
    //Author: Zack Repass
    //Purpose of the Class: This ViewModel is for giving data to the Single.cshtml view, allowing customers to see each product individually.
    //Methods in Class: SingleProductViewModel Constructor Method
    
    public class SingleProductViewModel : BaseViewModel
    {
        public Product Product {get;set;} // This property gives the ViewModel access to the Product.cs model and its properties.

        public SingleProductViewModel(BangazonWebContext ctx) : base(ctx) {} 
    }
}