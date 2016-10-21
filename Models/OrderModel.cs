using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bangazon.Models
{
    public class Customer
    {
        [key]
        public int OrderId { get; set}

        [Required]
        public bool isCompleted {get; set}
        public int? PaymentTypeId {get; set}
        public PaymentType PaymentType {get; set}

        [Required]
        public int CustomerId {get; set}
        public Customer Customer {get; set}
        public ICollection<LineItem> LineItems; 

        [Required]
        [DataTypeAttribute(DataType.Date)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Computed)]
        public DateTime DateCreated {get;set;}

        [Required]
        [DataTypeAttribute(DataType.Date)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Computed)]
        public DateTime DateCompleted {get;set;}
    }
}