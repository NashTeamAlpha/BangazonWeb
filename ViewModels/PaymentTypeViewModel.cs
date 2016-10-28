using BangazonWeb.Models;
using BangazonWeb.Data;

namespace BangazonWeb.ViewModels
{
    //Class Name: PAymentTypeViewModel
    //Author: Grant Regnier
    //Purpose of the class: The purpose of this class is to pass the type of PaymentType to the view for our payment form to model from.
    //Constructor in Class: PAymentTypeViewModel()
    public class PaymentTypeViewModel : BaseViewModel
    {
        public PaymentType PaymentType { get; set; }

        public PaymentTypeViewModel(BangazonWebContext ctx) : base(ctx) { } 
    }
}