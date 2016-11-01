using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BangazonWeb.Models
{
    //Class Name: PaymentType
    //Author: Zack Repass and Grant Regnier
    //Purpose of the class: This class serves as a model for our PaymentType objects being transfered, manipulated, and saved between our Database, context, and Views.
    //Methods in Class: None.
    public class PaymentType
    {
        [Key]
        public int PaymentTypeId {get;set;}
        
        [Required]
        public int CustomerId {get;set;}

        [Required]
        public Customer Customer {get;set;}

        [Required]
        [CreditCard]
        public int CardNumber {get;set;}

        [Required]
        [StringLength(25)]
        public string Processor {get;set;}

        [Required]
        [DataType(DataType.Date)]
        public DateTime ExpirationDate {get;set;}

        [Required]
        [StringLength(80)]
        public string StreetAddress {get;set;}

        [Required]
        [StringLength(50)]
        public string City {get;set;}

        [Required]
        [StringLength(2)]
        public string State {get;set;}

        [Required]
        [Range(5, 5)]
        public int ZipCode {get;set;}

        [Required]
        [StringLength(50)]
        public string FirstName {get;set;}

        [Required]
        [StringLength(50)]
        public string LastName {get;set;}
        public ICollection<Order> Orders;

    }
}