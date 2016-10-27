using System.Collections.Generic;
using BangazonWeb.Models;
using BangazonWeb.Data;

namespace BangazonWeb.ViewModels
{
    public class AllProductTypesViewModel : BaseViewModel
    {
    //Class Name: Products
    //Author: Debbie Bourne
    //Purpose of the class: The purpose of this class is to manage the methods that will produce the data and functionality needed for all of the views in the user interface related to products.
    //Methods in Class: AllProductTypesViewModel() 
        public IEnumerable<ProductType> ProductType { get; set; }
        public AllProductTypesViewModel(BangazonWebContext ctx) : base(ctx) { } 
        //Method Name: Index
        //Purpose of the Method: This method will return a list of all of the product types to the productsTypes view
        //Arguments in Method: the BangazonWebContext is being passed into this argument
    }
}