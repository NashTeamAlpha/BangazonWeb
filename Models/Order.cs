using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BangazonWeb.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set;}

        [Required]
        public bool IsCompleted {get; set;}
        public int? PaymentTypeId {get; set;}
        public PaymentType PaymentType {get; set;}

        [Required]
        public int CustomerId {get; set;}
        public Customer Customer {get; set;}
        public ICollection<LineItem> LineItems; 

        [Required]
        [DataTypeAttribute(DataType.Date)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Computed)]
        public DateTime DateCreated {get;set;}

        [DataTypeAttribute(DataType.Date)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Computed)]
        public DateTime? DateCompleted {get;set;}
    }
}
