using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BangazonWeb.Models
{
    public class PaymentType
    {
        [Key]
        public int PaymentTypeId {get;set;}
        
        [Required]
        public int CustomerId {get;set;}

        [Required]
        public Customer Customer {get;set;}

        [Required]
        public int CardNumber {get;set;}

        [Required]
        public string Processor {get;set;}

        [Required]
        [DataType(DataType.Date)]
        public DateTime ExpirationDate {get;set;}

        [Required]
        public string StreetAddress {get;set;}

        [Required]
        public string City {get;set;}

        [Required]
        public string State {get;set;}

        [Required]
        public int ZipCode {get;set;}

        [Required]
        public string FirstName {get;set;}

        [Required]
        public string LastName {get;set;}
        public ICollection<Order> Orders;

    }
}