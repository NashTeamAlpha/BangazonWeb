using System.Collections.Generic;
using BangazonWeb.Models;
using BangazonWeb.Data;

namespace BangazonWeb.ViewModels
{
    //Class Name: AllSubProductTypesViewModel
    //Author: Jamie Duke
    //Purpose of the class: The purpose of this class is to manage the property and method that will produce the data and functionality needed for the view of all of the product types
    //Constructor in Class: AllProductTypesViewModel() 
    public class AllSubProductTypesViewModel : BaseViewModel
    {
        public IEnumerable<SubProductType> SubProductTypes { get; set; }

        public AllSubProductTypesViewModel(BangazonWebContext ctx) : base(ctx) { } 
    }
}