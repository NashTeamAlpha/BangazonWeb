using System.Linq;
using BangazonWeb.Data;
using BangazonWeb.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BangazonWeb.ViewModels
{
  public class BaseViewModel
  {
    public List<SelectListItem> ListOfCustomers { get; set; }
    private BangazonWebContext context;
    private ActiveCustomer singleton = ActiveCustomer.Instance;
    public Customer CustomerForm {get; set; }
    public Customer ChosenCustomer 
    {
      get
      {
        // Get the current value of the customer property of our singleton
        Customer customer = singleton.Customer;

        // If no customer has been chosen yet, it's value will be null
        if (customer == null)
        {
          // Return fake customer for now
          return new Customer () {
            FirstName = "Create",
            LastName = "Account"
          };
        }
        // If there is a customer chosen, return it
        return customer;
      }
      set
      {
        if (value != null)
        {
          singleton.Customer = value;
        }
      }
    }
    public BaseViewModel(BangazonWebContext ctx)
    {
        context = ctx;
        this.ListOfCustomers = context.Customer
            .OrderBy(l => l.LastName)
            .AsEnumerable()
            .Select(li => new SelectListItem { 
                Text = $"{li.FirstName} {li.LastName}",
                Value = li.CustomerId.ToString()
            }).ToList();
        
        this.ListOfCustomers.Insert(0, new SelectListItem {
          Text="Choose Customer"
        });
    }
    public BaseViewModel() { }
  }
}

