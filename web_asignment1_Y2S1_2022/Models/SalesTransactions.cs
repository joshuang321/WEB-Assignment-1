using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web_asignment1_Y2S1_2022.Models
{
    public class SalesTransactions
    {
        public int TransactionID { get; set; }
        public string StoreID { get; set; }
        public string? MemberID { get; set; }
        [Display(Name = "Subtotal Amount")]
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        [Display(Name ="Discount(In Percentage)")]
        public double DiscountPercent { get; set; }
        [Display(Name ="Discount Amount")]
        public decimal DiscountAmt { get; set; }
        [Display(Name = "Total Amount")]
        public decimal Total { get; set; }
        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime DateCreated { get; set; }


    }
}
