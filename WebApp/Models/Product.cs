using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Can not be null")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Can not be null")]
        public decimal Cost { get; set; }
        public DateTime InsertDate { get; set; }
    }
}
