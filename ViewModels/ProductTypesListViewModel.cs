using System.Linq;
using BangazonWeb.Data;
using BangazonWeb.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BangazonWeb.ViewModels
{
  //Class Name: Products
  //Author: Delaine Wendling
  //Purpose of the class: The purpose of this class is to manage the methods that will produce the data and functionality needed for all of the views in the user interface related to products.
  //Methods in Class: Index(), Types(), Single(), Add() 
  public class ProductTypesListViewModel : BaseViewModel
  {
    public List<SelectListItem> ProductTypesList { get; set; }
    public List<SelectListItem> SubProductTypesList { get; set; }
    public Product Product {get; set;}
    private BangazonWebContext context;
    public ProductTypesListViewModel(BangazonWebContext ctx) : base(ctx)
    { 
        context = ctx;
        this.ProductTypesList = context.ProductType
          .OrderBy(type => type.Name)
          .AsEnumerable()
          .Select(li => new SelectListItem{
            Text = $"{li.Name}",
            Value = li.ProductTypeId.ToString()
          }).ToList();
        this.ProductTypesList.Insert(0, new SelectListItem{
          Text = "Choose Product Category"
        });

        this.SubProductTypesList = new List<SelectListItem>();

        this.SubProductTypesList.Insert(0, new SelectListItem{
          Text = "Choose Sub-Category"
        });
    }

    // public ProductTypesListViewModel(BangazonWebContext ctx, int id) : base(ctx)
    // { 
    //     context = ctx;
    //     this.ProductTypesList = context.ProductType
    //       .OrderBy(type => type.Name)
    //       .AsEnumerable()
    //       .Select(li => new SelectListItem{
    //         Text = $"{li.Name}",
    //         Value = li.ProductTypeId.ToString()
    //       }).ToList();
    //     this.ProductTypesList.Insert(0, new SelectListItem{
    //       Text = "Choose Product Category"
    //     });

    //     this.SubProductTypesList = context.SubProductType
    //         .Where(sub => sub.ProductTypeId == id)
    //         .OrderBy(n => n.Name)
    //         .AsEnumerable()
    //         .Select(li => new SelectListItem {
    //             Text = $"{li.Name}",
    //             Value = li.SubProductTypeId.ToString()
    //         }).ToList();
    //     this.SubProductTypesList.Insert(0, new SelectListItem{
    //       Text = "Choose Sub-Category"
    //     });
    // }
  }
}