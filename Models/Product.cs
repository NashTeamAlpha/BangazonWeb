using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bangazon.Models
{
  public class Product
  {
    [Key]
    public int ProductId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    [StringLength(255)]
    public string Description { get; set; }
    [Required]
    public double Price { get; set; }
    [Required]
    [DataType(DataType.Date)]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime DateAdded { get; set; }
    public int ProductTypeId { get; set; }
    public ProductType productType { get; set; }
    public ICollection<LineItem> LineItems; 
  }
}
