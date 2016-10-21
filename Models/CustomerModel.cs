using System;
using System.Collections.Generic;


namespace Bangazon.Models
{
    public class Customer
    {
        [Key]

        public int CustomerId {get;set;}

        [Required]

        public string FirstName {get;set;}

        [Required]

        public string LastName {get;set;}

        public ICollection<PaymentType> PaymentTypes;

        public ICollection<Order> Orders;
    }
}
