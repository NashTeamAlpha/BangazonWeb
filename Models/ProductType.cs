using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Bangazom.Models
{
  public class ProductType
  {
    [Key]
    public int ProductTypeId {get; set;}
    [Required]
    public string Name {get; set;}
    [Required]
    [StringLength(255)]
    public string Description {get; set;}

    public ICollection<Product> Products; 
    }
}